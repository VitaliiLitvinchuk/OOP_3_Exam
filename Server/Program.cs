using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Server.Injections;
using Server.Modules;

string directoryPath = AppDomain.CurrentDomain.BaseDirectory;
string jsonFileName = Directory.GetFiles(directoryPath, "*.json").First();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile(jsonFileName, optional: false, reloadOnChange: true);
    })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
    })
    .ConfigureServices(async (context, services) =>
    {
        var configuration = context.Configuration;

        services.AddPersistence(configuration);

        services.AddInjections();

        var provider = services.BuildServiceProvider();

        await provider.InitializeDb();
    })
    .Build();

// using var scope = host.Services.CreateScope();
// var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
// var adminRoleId = (await scope.ServiceProvider.GetRequiredService<IRoleQueries>().GetRoleByName("Admin", CancellationToken.None)).Id;
// var admin = User.New(UserId.New(), "AdminName", adminRoleId);

// await userRepository.Create(admin, CancellationToken.None);

await host.RunAsync();