using Domain.Models.Users;

namespace Infrastructure.Persistence.Repositories.Abstractions.Users;

public interface IUserQueries<T> where T : User
{
    Task<T?> GetUserById(UserId id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetUsers(CancellationToken cancellationToken);
}
