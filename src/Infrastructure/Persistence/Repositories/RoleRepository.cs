using Domain.Models.Roles;
using Infrastructure.Persistence.Repositories.Abstractions.Roles;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class RoleRepository(ExamDbContext context) : IRoleRepository, IRoleQueries
{
    private readonly DbSet<Role> _roles = context.Roles;

    public async Task<Role> Create(Role role, CancellationToken cancellationToken)
    {
        await _roles.AddAsync(role, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return role;
    }

    public async Task<Role> Delete(Role role, CancellationToken cancellationToken)
    {
        _roles.Remove(role);

        await context.SaveChangesAsync(cancellationToken);

        return role;
    }

    public async Task<Role> Update(Role role, CancellationToken cancellationToken)
    {
        _roles.Update(role);

        await context.SaveChangesAsync(cancellationToken);

        return role;
    }

    public async Task<Role?> GetRoleById(RoleId id, CancellationToken cancellationToken)
    {
        return await _roles.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken)
    {
        return await _roles.FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
    }

    public async Task<IEnumerable<Role>> GetRoles(CancellationToken cancellationToken)
    {
        return await _roles.ToListAsync(cancellationToken);
    }
}
