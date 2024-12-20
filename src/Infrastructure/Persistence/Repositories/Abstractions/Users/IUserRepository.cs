using Domain.Models.Users;

namespace Infrastructure.Persistence.Repositories.Abstractions.Users;

public interface IUserRepository<T> where T : User
{
    Task<T> Create(T user, CancellationToken cancellationToken);
    Task<T> Update(T user, CancellationToken cancellationToken);
    Task<T> Delete(T user, CancellationToken cancellationToken);
}
