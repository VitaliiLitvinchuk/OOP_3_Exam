using Microsoft.Extensions.DependencyInjection;
using Server.Injections.Factories;
using Server.Injections.Loggers;
using Server.Injections.Managers;
using Server.Injections.Repositories;

namespace Server.Injections;

public static class Injections
{
    public static IServiceCollection AddInjections(this IServiceCollection services)
    {
        services.AddRepositories();

        services.AddFactories();

        services.AddLoggers();

        services.AddManagers();

        return services;
    }
}
