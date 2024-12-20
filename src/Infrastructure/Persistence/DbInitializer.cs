using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class DbInitializer(ExamDbContext context)
{
    public async Task InitializeAsync()
    {
        await context.Database.MigrateAsync();
    }
}
