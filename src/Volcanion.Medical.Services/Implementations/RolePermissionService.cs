using Microsoft.Extensions.Logging;
using Volcanion.Core.Services.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Services.Implementations;

/// <inheritdoc />
internal class RolePermissionService : BaseService<RolePermission, IRolePermissionRepository, RolePermissionFilter>, IRolePermissionService
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    public RolePermissionService(IRolePermissionRepository repository, ILogger<BaseService<RolePermission, IRolePermissionRepository, RolePermissionFilter>> logger) : base(repository, logger)
    {
    }
}
