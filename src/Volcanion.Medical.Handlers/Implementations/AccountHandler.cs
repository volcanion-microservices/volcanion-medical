using Microsoft.Extensions.Logging;
using Volcanion.Core.Handlers.Implementations;
using Volcanion.Medical.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Models.Request;
using Volcanion.Medical.Models.Response;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Handlers.Implementations;

/// <inheritdoc/>
internal class AccountHandler : BaseHandler<Account, IAccountService, AccountFilter>, IAccountHandler
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="service"></param>
    /// <param name="logger"></param>
    public AccountHandler(IAccountService service, ILogger<BaseHandler<Account, IAccountService, AccountFilter>> logger) : base(service, logger)
    {
    }

    /// <inheritdoc/>
    public async Task<AccountResponse?> Login(AccountLogin account)
    {
        return await _service.Login(account);
    }

    /// <inheritdoc/>
    public async Task<AccountResponse?> RefreshToken(TokenRequest request)
    {
        return await _service.RefreshToken(request);
    }

    /// <inheritdoc/>
    public async Task<AccountResponse?> Register(AccountRegister account)
    {
        return await _service.Register(account);
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAccountAsync(Account account)
    {
        return await _service.UpdateAccountAsync(account);
    }
}
