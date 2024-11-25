using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Context;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Infrastructure.Implementations.Identity;

/// <inheritdoc />
internal class RolePermissionRepository : BaseRepository<RolePermission, ApplicationDbContext, RolePermissionFilter>, IRolePermissionRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <param name="httpContextAccessor"></param>
    public RolePermissionRepository(ApplicationDbContext context, ILogger<BaseRepository<RolePermission, ApplicationDbContext, RolePermissionFilter>> logger, IHttpContextAccessor httpContextAccessor) : base(context, logger, httpContextAccessor)
    {
    }

    /// <inheritdoc />
    public async Task<List<RolePermission>?> GetRolePermissionByGrantPermissionId(Guid grantPermissionId)
    {
        return await _context.RolePermissions
            .Include(x => x.Role)
            .Include(x => x.Permission)
            .AsNoTracking()
            .Where(x => x.Id == grantPermissionId)
            .ToListAsync();
    }
}
