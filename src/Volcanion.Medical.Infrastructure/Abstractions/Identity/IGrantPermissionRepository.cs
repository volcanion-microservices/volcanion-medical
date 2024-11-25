using Volcanion.Core.Infrastructure.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Models.Response.BOs;

namespace Volcanion.Medical.Infrastructure.Abstractions.Identity;

/// <summary>
/// IGrantPermissionRepository
/// </summary>
public interface IGrantPermissionRepository : IGenericRepository<GrantPermission, GrantPermissionFilter>
{
    /// <summary>
    /// GetGrantPermissionByAccountId
    /// </summary>
    /// <param name="accountId"></param>
    /// <returns></returns>
    Task<List<GrantPermissionResponseBO>?> GetGrantPermissionByAccountId(Guid accountId);
}
