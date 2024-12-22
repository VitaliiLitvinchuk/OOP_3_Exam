using Application.Managers;
using Domain.Models.Roles;
using Domain.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Server;

class Program
{
    static async Task Main(string[] args)
    {
        IHost host = await HostCreator.CreateHost(args);

        using var scope = host.Services.CreateScope();

        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager>();

        // RoleManager usage examples
        Role newRole = Role.New(RoleId.New(), "NewRole1");
        await roleManager.CreateRole(newRole, CancellationToken.None);

        IEnumerable<Role> roles = await roleManager.GetRoles(CancellationToken.None);
        foreach (var role in roles)
        {
            Console.WriteLine($"Role: {role.Name}");
        }

        Console.WriteLine("Press 'e' to exit or any other key to delete the created role.");
        if (Console.ReadLine() != "e")
        {
            await roleManager.DeleteRole(newRole, CancellationToken.None);
        }

        roles = await roleManager.GetRoles(CancellationToken.None);
        foreach (var role in roles)
        {
            Console.WriteLine($"Role: {role.Name}");
        }

        // UserManager usage examples
        User newUser = User.New(UserId.New(), "NewUser", newRole.Id);
        await userManager.CreateUser(newUser, CancellationToken.None);

        User? retrievedUser = await userManager.GetUserById(newUser.Id, CancellationToken.None);
        if (retrievedUser != null)
        {
            Console.WriteLine($"User: {retrievedUser.Name}, Role: {retrievedUser.RoleId}");
        }

        Console.WriteLine("Press 'e' to exit or any other key to delete the created user.");
        if (Console.ReadLine() != "e")
        {
            await userManager.DeleteUser(newUser, CancellationToken.None);
        }

        IEnumerable<User> users = await userManager.GetUsers(CancellationToken.None);
        foreach (var user in users)
        {
            Console.WriteLine($"User: {user.Name}, Role: {user.RoleId}");
        }
    }
}