using Domain.Models.Users;

namespace Application.Factories.Users.Abstractions;

public interface IUserFactory
{
    Task<User> CreateUser(string name, CancellationToken cancellationToken);
}
