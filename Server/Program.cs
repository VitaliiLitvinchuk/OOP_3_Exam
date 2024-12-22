using Application.Managers;
using Domain.Models.Roles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Server;

class Program
{
    static async Task Main(string[] args)
    {
        // IHost host = await HostCreator.CreateHost(args);

        // using var scope = host.Services.CreateScope();

        // var manager = scope.ServiceProvider.GetRequiredService<RoleManager>();

        // IEnumerable<Role> roles;

        // do
        // {
        //     roles = await manager.GetRoles(CancellationToken.None);
        //     foreach (var role in roles)
        //     {
        //         Console.WriteLine($"Role: {role.Name}");
        //     }
        //     Console.WriteLine("Press 'e' to exit or any other key to display roles again.");
        // } while (Console.ReadLine() != "e");

        IHost host = await HostCreator.CreateHost(args);

        using var scope = host.Services.CreateScope();

        var manager = scope.ServiceProvider.GetRequiredService<RoleManager>();

        Role role = Role.New(RoleId.New(), "NewRole1");

        await manager.CreateRole(role, CancellationToken.None);

        IEnumerable<Role> roles = await manager.GetRoles(CancellationToken.None);

        foreach (var r in roles)
        {
            Console.WriteLine($"Role: {r.Name}");
        }

        Console.WriteLine("Press 'e' to exit or any other key to delete the created role.");
        if (Console.ReadLine() != "e")
        {
            await manager.DeleteRole(role, CancellationToken.None);
        }

        roles = await manager.GetRoles(CancellationToken.None);

        foreach (var r in roles)
        {
            Console.WriteLine($"Role: {r.Name}");
        }
    }
}