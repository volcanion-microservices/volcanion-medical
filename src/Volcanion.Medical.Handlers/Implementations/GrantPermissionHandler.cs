using Microsoft.Extensions.Logging;
using Volcanion.Core.Handlers.Implementations;
using Volcanion.Medical.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Handlers.Implementations;

/// <inheritdoc/>
internal class GrantPermissionHandler : BaseHandler<GrantPermission, IGrantPermissionService, GrantPermissionFilter>, IGrantPermissionHandler
{
    /// <inheritdoc/>
    public GrantPermissionHandler(IGrantPermissionService service, ILogger<BaseHandler<GrantPermission, IGrantPermissionService, GrantPermissionFilter>> logger) : base(service, logger)
    {
    }
}
