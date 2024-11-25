using Microsoft.Extensions.DependencyInjection;
using Volcanion.Medical.Handlers.Abstractions;
using Volcanion.Medical.Handlers.Implementations;

namespace Volcanion.Medical.Handlers;

/// <summary>
/// IdentityHandlerRegister
/// </summary>
public static class IdentityHandlerRegister
{
    /// <summary>
    /// RegisterIdentityInfrastructure
    /// </summary>
    /// <param name="services"></param>
    public static void RegisterIdentityHandler(this IServiceCollection services)
    {
        services.AddTransient<IAccountHandler, AccountHandler>();
        services.AddTransient<IGrantPermissionHandler, GrantPermissionHandler>();
        services.AddTransient<IPermissionHandler, PermissionHandler>();
        services.AddTransient<IRolePermissionHandler, RolePermissionHandler>();
        services.AddTransient<IRoleHandler, RoleHandler>();
    }
}
