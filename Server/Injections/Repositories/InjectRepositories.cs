using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Abstractions.Roles;
using Infrastructure.Persistence.Repositories.Abstractions.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Injections.Repositories;

public static class InjectRepositories
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<RoleRepository>();
        services.AddScoped<IRoleRepository>(provider => provider.GetRequiredService<RoleRepository>());
        services.AddScoped<IRoleQueries>(provider => provider.GetRequiredService<RoleRepository>());

        services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
        services.AddScoped(typeof(IUserQueries<>), typeof(UserRepository<>));

        return services;
    }
}
