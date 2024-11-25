using Microsoft.Extensions.Logging;
using Volcanion.Core.Handlers.Implementations;
using Volcanion.Medical.Handlers.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Handlers.Implementations;

/// <inheritdoc/>
internal class RoleHandler : BaseHandler<Role, IRoleService, RoleFilter>, IRoleHandler
{
    /// <inheritdoc/>
    public RoleHandler(IRoleService service, ILogger<BaseHandler<Role, IRoleService, RoleFilter>> logger) : base(service, logger)
    {
    }
}
