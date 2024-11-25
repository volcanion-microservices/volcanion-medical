using Volcanion.Core.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Handlers.Abstractions;

/// <summary>
/// IGrantPermissionHandler
/// </summary>
public interface IGrantPermissionHandler : IBaseHandler<GrantPermission, GrantPermissionFilter>
{
}
