using Volcanion.Core.Services.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Services.Abstractions;

/// <summary>
/// IGrantPermissionService
/// </summary>
public interface IGrantPermissionService : IBaseService<GrantPermission, GrantPermissionFilter>
{
}
