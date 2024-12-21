using Application.Factories.Loggers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Injections.Loggers;

public static class InjectLoggers
{
    public static IServiceCollection AddLoggers(this IServiceCollection services)
    {
        services.AddSingleton(provider => provider.GetRequiredService<ILoggerFactory>().CreateLogger());

        return services;
    }
}
