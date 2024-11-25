using Microsoft.Extensions.Logging;
using Volcanion.Core.Handlers.Implementations;
using Volcanion.Medical.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Handlers.Implementations;

/// <inheritdoc/>
internal class PermissionHandler : BaseHandler<Permission, IPermissionService, PermissionFilter>, IPermissionHandler
{
    /// <inheritdoc/>
    public PermissionHandler(IPermissionService service, ILogger<BaseHandler<Permission, IPermissionService, PermissionFilter>> logger) : base(service, logger)
    {
    }
}
