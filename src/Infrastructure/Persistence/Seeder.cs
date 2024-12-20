using Domain.Models.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static class Seeder
{
    public static async Task SeedAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<ExamDbContext>();

        await SeedRolesAsync(context.Roles);

        await context.SaveChangesAsync();
    }

    private static async Task SeedRolesAsync(DbSet<Role> roles)
    {
        if (!await roles.AnyAsync())
        {
            await roles.AddRangeAsync(DefaultDbData.GetRoles());
        }
    }
}
