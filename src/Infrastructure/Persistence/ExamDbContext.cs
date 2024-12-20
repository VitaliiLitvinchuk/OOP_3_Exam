using System.Reflection;
using Domain.Models.Roles;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ExamDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}
