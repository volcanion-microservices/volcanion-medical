using Microsoft.Extensions.Logging;
using Volcanion.Core.Services.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Services.Implementations;

/// <inheritdoc />
internal class GrantPermissionService : BaseService<GrantPermission, IGrantPermissionRepository, GrantPermissionFilter>, IGrantPermissionService
{
    public GrantPermissionService(IGrantPermissionRepository repository, ILogger<BaseService<GrantPermission, IGrantPermissionRepository, GrantPermissionFilter>> logger) : base(repository, logger)
    {
    }
}
