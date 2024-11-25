using Microsoft.Extensions.DependencyInjection;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Infrastructure.Implementations.Identity;

namespace Volcanion.Medical.Infrastructure;

/// <summary>
/// IdentityInfrastructureRegister
/// </summary>
public static class IdentityInfrastructureRegister
{
    /// <summary>
    /// RegisterIdentityInfrastructure
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterIdentityInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IAccountRepository, AccountRepository>();
        services.AddTransient<IGrantPermissionRepository, GrantPermissionRepository>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IPermissionRepository, PermissionRepository>();
        services.AddTransient<IRolePermissionRepository, RolePermissionRepository>();
        services.AddTransient<IRoleRepository, RoleRepository>();
    }
}
