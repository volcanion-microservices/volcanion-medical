using Volcanion.Core.Infrastructure.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Infrastructure.Abstractions.Identity;

/// <summary>
/// IAccountRepository
/// </summary>
public interface IAccountRepository : IGenericRepository<Account, AccountFilter>
{
    /// <summary>
    /// GetAccountByEmail
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<Account?> GetAccountByEmail(string email);
}
