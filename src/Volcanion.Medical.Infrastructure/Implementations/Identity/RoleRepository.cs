using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Context;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Infrastructure.Implementations.Identity;

/// <inheritdoc />
internal class RoleRepository : BaseRepository<Role, ApplicationDbContext, RoleFilter>, IRoleRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <param name="httpContextAccessor"></param>
    public RoleRepository(ApplicationDbContext context, ILogger<BaseRepository<Role, ApplicationDbContext, RoleFilter>> logger, IHttpContextAccessor httpContextAccessor) : base(context, logger, httpContextAccessor)
    {
    }
}
