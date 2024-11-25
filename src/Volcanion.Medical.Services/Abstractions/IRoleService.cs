using Volcanion.Core.Services.Abstractions;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Services.Abstractions;

/// <inheritdoc />
public interface IRoleService : IBaseService<Role, RoleFilter>
{
}
