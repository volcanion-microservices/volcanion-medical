using Volcanion.Core.Infrastructure.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Infrastructure.Abstractions.Identity;

/// <summary>
/// IRolePermissionRepository
/// </summary>
public interface IRolePermissionRepository : IGenericRepository<RolePermission, RolePermissionFilter>
{
    /// <summary>
    /// GetRolePermissionByGrantPermissionId
    /// </summary>
    /// <param name="grantPermissionId"></param>
    /// <returns></returns>
    Task<List<RolePermission>?> GetRolePermissionByGrantPermissionId(Guid grantPermissionId);
}
