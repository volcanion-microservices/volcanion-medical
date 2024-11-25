using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volcanion.Core.Common.Abstractions;
using Volcanion.Core.Models.Enums;
using Volcanion.Core.Models.Jwt;
using Volcanion.Core.Presentation.Middlewares.Exceptions;
using Volcanion.Core.Services.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Models.Request;
using Volcanion.Medical.Models.Response;
using Volcanion.Medical.Models.Setting;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Services.Implementations;

/// <inheritdoc />
internal class AccountService : BaseService<Account, IAccountRepository, AccountFilter>, IAccountService
{
    /// <summary>
    /// ICacheProvider
    /// </summary>
    private readonly IRedisCacheProvider _redisCacheProvider;

    /// <summary>
    /// IJwtProvider
    /// </summary>
    private readonly IJwtProvider _jwtProvider;

    /// <summary>
    /// IHahsProvider instance
    /// </summary>
    private readonly IHashProvider _hashProvider;

    /// <summary>
    /// IGrantPermissionRepository instance
    /// </summary>
    private readonly IGrantPermissionRepository _grantPermissionRepository;

    /// <summary>
    /// IRolePermissionRepository instance
    /// </summary>
    private readonly IRolePermissionRepository _rolePermissionRepository;

    /// <summary>
    /// IHttpContextAccessor
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// AllowedOrigin
    /// </summary>
    private string[] AllowedOrigin { get; set; }

    /// <summary>
    /// GroupAccess
    /// </summary>
    private string[] GroupAccess { get; set; }

    /// <summary>
    /// Audience
    /// </summary>
    private string Audience { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    /// <param name="hashProvider"></param>
    public AccountService(IAccountRepository repository, ILogger<BaseService<Account, IAccountRepository, AccountFilter>> logger, IHashProvider hashProvider, IOptions<AppSettings> options, IGrantPermissionRepository grantPermissionRepository, IJwtProvider jwtProvider, IRedisCacheProvider redisCacheProvider, IRolePermissionRepository rolePermissionRepository, IHttpContextAccessor httpContextAccessor) : base(repository, logger)
    {
        _hashProvider = hashProvider;
        _grantPermissionRepository = grantPermissionRepository;
        _jwtProvider = jwtProvider;
        _redisCacheProvider = redisCacheProvider;
        AllowedOrigin = options.Value.AllowedOrigins;
        Audience = options.Value.Audience;
        GroupAccess = options.Value.GroupAccess;
        _rolePermissionRepository = rolePermissionRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public async Task<AccountResponse?> Login(AccountLogin account)
    {
        // Find account by email
        var accountFind = await _repository.GetAccountByEmail(account.LoginName);
        // If account not found, return null
        if (accountFind == null) return null;
        // Verify password
        if (!_hashProvider.VerifyPassword(accountFind.Password, account.Password)) return null;

        // Generate resource access
        var resourceAccess = await GenerateResourceAccessFromRole(accountFind.Id) ?? throw new VolcanionBusinessException("Cannot generate resource access!");

        // Generate session id and save to redis cache
        var sessionId = Guid.NewGuid().ToString();
        _ = _redisCacheProvider.SetStringAsync(sessionId, "Valid");
        // Generate account response and return
        return GenerateAccountResponse(accountFind, account.Issuer, account.RememberMe, resourceAccess, sessionId);
    }

    /// <inheritdoc />
    public async Task<AccountResponse?> RefreshToken(TokenRequest request)
    {
        // Validate refresh token
        if (!_jwtProvider.ValidateJwt(request.Token, JwtType.RefreshToken)) return null;
        // Decode refresh token and get payload
        var payload = _jwtProvider.DecodeJwt(request.Token).payload;
        // If payload is null, return null
        if (payload == null) return null;

        // Find account by email from payload
        var accountFind = await _repository.GetAccountByEmail(payload.Email);
        // If account not found, return null
        if (accountFind == null) return null;

        // Generate resource access
        var resourceAccess = await GenerateResourceAccessFromRole(accountFind.Id) ?? throw new VolcanionBusinessException("Cannot generate resource access!");

        // Generate session id and save to redis cache
        var sessionId = Guid.NewGuid().ToString();
        _ = _redisCacheProvider.SetStringAsync(sessionId, "Valid");
        // Generate account response and return
        return GenerateAccountResponse(accountFind, payload.Issuer, true, resourceAccess, sessionId);
    }

    /// <inheritdoc />
    public async Task<AccountResponse?> Register(AccountRegister account)
    {
        // Find account by email
        var accountFind = await _repository.GetAccountByEmail(account.Email);
        // If account found, throw exception
        if (accountFind != null) throw new VolcanionBusinessException("Email is exists!");

        // Create new account
        var registerAccount = new Account
        {
            Address = account.Address,
            Email = account.Email,
            Fullname = account.Fullname,
            Password = _hashProvider.HashPassword(account.Password)
        };

        // Save account to database
        var res = await _repository.CreateAsync(registerAccount);
        // If account not saved, return null
        if (res == Guid.Empty) return null;
        // Generate resource access
        var resourceAccess = await GenerateResourceAccessFromRole(res) ?? throw new VolcanionBusinessException("Cannot generate resource access!");

        // Generate session id and save to redis cache
        var sessionId = Guid.NewGuid().ToString();
        _ = _redisCacheProvider.SetStringAsync(sessionId, "Valid");
        // Generate account response and return
        return GenerateAccountResponse(registerAccount, account.Issuer, true, resourceAccess, sessionId);
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAccountAsync(Account account)
    {
        var routeData = _httpContextAccessor.HttpContext.GetRouteData().Values;
        var accountFind = await _repository.GetAsync(account.Id);
        if (accountFind == null) return false;
        account.Password = string.IsNullOrEmpty(account.Password) ? accountFind.Password : _hashProvider.HashPassword(account.Password);

        if (account.IsActived != accountFind.IsActived)
        {
            if (!account.IsActived)
            {
                account.IsDeleted = true;
                account.DeletedAt = DateTimeOffset.Now;
                account.DeletedBy = routeData["AccountId"].ToString();
            }
            else
            {
                account.IsDeleted = false;
                account.DeletedAt = null;
                account.DeletedBy = null;
            }
        }

        return await _repository.UpdateAsync(account);
    }

    /// <summary>
    /// GenerateAccountResponse
    /// </summary>
    /// <param name="account"></param>
    /// <param name="issuer"></param>
    /// <param name="rememberMe"></param>
    /// <param name="resourceAccess"></param>
    /// <returns></returns>
    private AccountResponse GenerateAccountResponse(Account account, string issuer, bool rememberMe, ResourceAccess resourceAccess, string sessionId)
    {
        // Get group access from account id
        var groupAccess = GetGroupAccess(account.Id);

        // Generate access token
        var refreshToken = "";
        var accessToken = _jwtProvider.GenerateJwt(account, Audience, issuer, [.. AllowedOrigin], groupAccess, resourceAccess, JwtType.AccessToken, sessionId);

        // If remember me is true, generate refresh token
        if (rememberMe)
        {
            refreshToken = _jwtProvider.GenerateJwt(account, Audience, issuer, [.. AllowedOrigin], groupAccess, resourceAccess, JwtType.RefreshToken, sessionId);
        }

        // Return account response
        return new AccountResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Account = account
        };
    }

    /// <summary>
    /// GenerateResourceAccessFromRole
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    private async Task<ResourceAccess?> GenerateResourceAccessFromRole(Guid accountId)
    {
        // Get grant permission by account id
        var grantPermissions = await _grantPermissionRepository.GetGrantPermissionByAccountId(accountId);
        var roles = new List<string>();

        if (grantPermissions != null && grantPermissions.Count > 0)
        {
            var roleFound = grantPermissions.Select(rp => $"{rp.RoleName}.{rp.PermissionName}").ToList();
            if (roleFound != null && roleFound.Count > 0) roles.AddRange(roleFound);
        }

        // Return resource access
        return new ResourceAccess
        {
            RoleAccess = new RoleAccess { Roles = roles }
        };
    }

    /// <summary>
    /// GetGroupAccess
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    private List<string> GetGroupAccess(Guid accountId)
    {
        // TODO: implement group access
        var groupAccess = GroupAccess.ToList();
        return groupAccess;
    }
}
