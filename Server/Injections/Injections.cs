using Microsoft.Extensions.DependencyInjection;
using Server.Injections.Repositories;

namespace Server.Injections;

public static class Injections
{
    public static IServiceCollection AddInjections(this IServiceCollection services)
    {
        services.AddRepositories();

        return services;
    }
}
