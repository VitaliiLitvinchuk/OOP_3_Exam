using Domain.Models.Users;

namespace Infrastructure.Persistence.Repositories.Abstractions.Users;

public interface IUserQueries
{
    Task<User?> GetUserById(UserId id, CancellationToken cancellationToken);
    Task<IEnumerable<User>> GetUsers(CancellationToken cancellationToken);
}
