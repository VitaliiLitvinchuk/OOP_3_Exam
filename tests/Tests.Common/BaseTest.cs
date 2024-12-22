using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Tests.Common;

public abstract class BaseTest : IClassFixture<TestFactory>
{
    protected readonly ExamDbContext Context;

    protected BaseTest(TestFactory factory)
    {
        var scope = factory.ServiceProvider.CreateScope();

        Context = scope.ServiceProvider.GetRequiredService<ExamDbContext>();
    }

    protected async Task<int> SaveChangesAsync()
    {
        var result = await Context.SaveChangesAsync();

        Context.ChangeTracker.Clear();

        return result;
    }
}
