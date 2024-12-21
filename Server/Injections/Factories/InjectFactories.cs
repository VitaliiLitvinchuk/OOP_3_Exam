using Application.Factories.Loggers;
using Application.Factories.Loggers.Abstractions;
using Application.Factories.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Injections.Factories;

public static class InjectFactories
{
    public static IServiceCollection AddFactories(this IServiceCollection services)
    {
        services.AddSingleton<ILoggerFactory, LoggerFactory>();

        services.AddScoped<AdminFactory>();
        services.AddScoped<UserFactory>();
        services.AddScoped<GuestFactory>();

        return services;
    }
}
