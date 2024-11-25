using Volcanion.Core.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Handlers.Abstractions;

/// <summary>
/// IPermissionHandler
/// </summary>
public interface IPermissionHandler : IBaseHandler<Permission, PermissionFilter>
{
}
