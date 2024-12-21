using Application.Loggers.Abstractions;
using Application.Managers;
using Infrastructure.Persistence.Repositories.Abstractions.Roles;
using Infrastructure.Persistence.Repositories.Abstractions.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Injections.Managers;

public static class InjectManagers
{
    public static IServiceCollection AddManagers(this IServiceCollection services)
    {
        services.AddSingleton(provider =>
        {
            RoleManager.Initialize(
                provider.GetRequiredService<IRoleQueries>(),
                provider.GetRequiredService<IRoleRepository>(),
                provider.GetRequiredService<ILogger>()
            );

            return RoleManager.Instance;
        });

        services.AddSingleton(provider =>
        {
            UserManager.Initialize(
                provider.GetRequiredService<IUserQueries>(),
                provider.GetRequiredService<IUserRepository>(),
                provider.GetRequiredService<ILogger>()
            );

            return UserManager.Instance;
        });

        return services;
    }
}
