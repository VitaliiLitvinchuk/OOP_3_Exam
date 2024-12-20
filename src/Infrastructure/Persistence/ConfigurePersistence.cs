using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Persistence;

public static class ConfigurePersistence
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var builder = new NpgsqlDataSourceBuilder(connectionString);

        var dataSource = builder.Build();

        services.AddDbContext<ExamDbContext>(options =>
            options.UseNpgsql(dataSource,
                builder => builder.MigrationsAssembly(typeof(ExamDbContext).Assembly.FullName)
            )
            .UseSnakeCaseNamingConvention()
            .ConfigureWarnings(x => x.Ignore(RelationalEventId.MultipleCollectionIncludeWarning))
        );

        services.AddScoped<DbInitializer>();

        return services;
    }
}
