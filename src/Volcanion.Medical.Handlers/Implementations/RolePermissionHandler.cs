using Microsoft.Extensions.Logging;
using Volcanion.Core.Handlers.Implementations;
using Volcanion.Medical.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Handlers.Implementations;

/// <inheritdoc/>
internal class RolePermissionHandler : BaseHandler<RolePermission, IRolePermissionService, RolePermissionFilter>, IRolePermissionHandler
{
    /// <inheritdoc/>
    public RolePermissionHandler(IRolePermissionService service, ILogger<BaseHandler<RolePermission, IRolePermissionService, RolePermissionFilter>> logger) : base(service, logger)
    {
    }
}
