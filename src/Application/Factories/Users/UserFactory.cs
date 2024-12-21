using Application.Factories.Users.Abstractions;
using Application.Managers;
using Domain.Models.Users;
using static Infrastructure.Persistence.DefaultDbData;

namespace Application.Factories.Users;

public class UserFactory(RoleManager roleManager) : IUserFactory
{
    private const Roles UserFactoryRole = Roles.User;
    public async Task<User> CreateUser(string name, CancellationToken cancellationToken)
    {
        var role = (await roleManager.GetRoleByName(Enum.GetName(UserFactoryRole)!, cancellationToken))!;

        return User.New(UserId.New(), name, role.Id);
    }
}
