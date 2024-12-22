using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Injections;
using Server.Modules;

namespace Server;

public static class HostCreator
{
    public static async Task<IHost> CreateHost(string[] args, Action<IHostBuilder>? configureHost = null)
    {
        string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
        string jsonFileName = Directory.GetFiles(directoryPath, "*.json").First();

        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile(jsonFileName, optional: false, reloadOnChange: true);
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
            })
            .ConfigureServices((context, services) =>
            {
                var configuration = context.Configuration;

                services.AddPersistence(configuration);

                services.AddInjections();
            });

        configureHost?.Invoke(builder);

        var host = builder.Build();

        await host.InitializeDb();

        return host;
    }
}