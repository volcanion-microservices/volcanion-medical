using Volcanion.Core.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Models.Request;
using Volcanion.Medical.Models.Response;

namespace Volcanion.Medical.Handlers.Abstractions;

/// <summary>
/// IAccountHandler
/// </summary>
public interface IAccountHandler : IBaseHandler<Account, AccountFilter>
{
    /// <summary>
    /// Register
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<AccountResponse?> Register(AccountRegister account);

    /// <summary>
    /// Login
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<AccountResponse?> Login(AccountLogin account);

    /// <summary>
    /// RefreshToken
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<AccountResponse?> RefreshToken(TokenRequest request);

    /// <summary>
    /// UpdateAccountAsync
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    Task<bool> UpdateAccountAsync(Account account);
}
