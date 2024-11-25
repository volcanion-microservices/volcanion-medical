using Microsoft.Extensions.Logging;
using Volcanion.Core.Services.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Services.Implementations;

/// <inheritdoc />
internal class PermissionService : BaseService<Permission, IPermissionRepository, PermissionFilter>, IPermissionService
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    public PermissionService(IPermissionRepository repository, ILogger<BaseService<Permission, IPermissionRepository, PermissionFilter>> logger) : base(repository, logger)
    {
    }
}
