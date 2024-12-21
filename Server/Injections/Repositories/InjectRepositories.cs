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

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository>(provider => provider.GetRequiredService<UserRepository>());
        services.AddScoped<IUserQueries>(provider => provider.GetRequiredService<UserRepository>());

        return services;
    }
}
