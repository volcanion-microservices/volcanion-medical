using Volcanion.Core.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Handlers.Abstractions;

/// <summary>
/// IRolePermissionHandler
/// </summary>
public interface IRolePermissionHandler : IBaseHandler<RolePermission, RolePermissionFilter>
{
}
