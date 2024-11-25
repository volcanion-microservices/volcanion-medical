using Microsoft.Extensions.Logging;
using Volcanion.Core.Services.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;
using Volcanion.Medical.Services.Abstractions;

namespace Volcanion.Medical.Services.Implementations;

/// <inheritdoc />
internal class RoleService : BaseService<Role, IRoleRepository, RoleFilter>, IRoleService
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="logger"></param>
    public RoleService(IRoleRepository repository, ILogger<BaseService<Role, IRoleRepository, RoleFilter>> logger) : base(repository, logger)
    {
    }
}
