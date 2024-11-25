using Volcanion.Core.Infrastructure.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Infrastructure.Abstractions.Identity;

/// <summary>
/// IRoleRepository
/// </summary>
public interface IRoleRepository : IGenericRepository<Role, RoleFilter>
{
}
