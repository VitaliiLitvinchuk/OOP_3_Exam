using Application.Managers;
using Domain.Models.Roles;
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
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        services.AddPersistence(configuration);

        services.AddInjections();
    })
    .Build();

await host.InitializeDb();

using var scope = host.Services.CreateScope();

var manager = scope.ServiceProvider.GetRequiredService<RoleManager>();

IEnumerable<Role> roles;

while (Console.ReadLine() != "e")
{
    roles = await manager.GetRoles(CancellationToken.None);
    foreach (var role in roles)
    {
        Console.WriteLine($"Role: {role.Name}");
    }
    Console.WriteLine("Press 'e' to exit or any other key to display roles again.");
}
