using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Modules;

public static class DbModule
{
    public static async Task InitializeDb(this IServiceProvider service)
    {
        using var scope = service.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();

        await initializer.InitializeAsync();
        await service.SeedAsync();
    }
}
