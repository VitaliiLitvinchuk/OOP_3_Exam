using Domain.Models.Roles;

namespace Infrastructure.Persistence.Repositories.Abstractions.Roles;

public interface IRoleRepository
{
    Task<Role> Create(Role role, CancellationToken cancellationToken);
    Task<Role> Update(Role role, CancellationToken cancellationToken);
    Task<Role> Delete(Role role, CancellationToken cancellationToken);
}
