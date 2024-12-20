using Domain.Models.Roles;

namespace Infrastructure.Persistence.Repositories.Abstractions.Roles;

public interface IRoleQueries
{
    Task<Role?> GetRoleById(RoleId id, CancellationToken cancellationToken);
    Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Role>> GetRoles(CancellationToken cancellationToken);
}
