using Application.Loggers;
using Application.Loggers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Injections.Loggers;

public static class InjectLoggers
{
    public static IServiceCollection AddLoggers(this IServiceCollection services)
    {
        services.AddSingleton<ILogger>(provider =>
        {
            return new ConsoleLogger();
        });

        return services;
    }
}
