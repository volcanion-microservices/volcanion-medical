using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Context;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Infrastructure.Implementations.Identity;

/// <inheritdoc/>
internal class PermissionRepository : BaseRepository<Permission, ApplicationDbContext, PermissionFilter>, IPermissionRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <param name="httpContextAccessor"></param>
    public PermissionRepository(ApplicationDbContext context, ILogger<BaseRepository<Permission, ApplicationDbContext, PermissionFilter>> logger, IHttpContextAccessor httpContextAccessor) : base(context, logger, httpContextAccessor)
    {
    }
}
