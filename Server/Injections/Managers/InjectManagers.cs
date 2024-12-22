using Application.Managers;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Injections.Managers;

public static class InjectManagers
{
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            RoleManager.Initialize(provider);

            return RoleManager.Instance;
        });

        services.AddSingleton(provider =>
        {
            UserManager.Initialize(provider);

            return UserManager.Instance;
        });

        return services;
    }
}
