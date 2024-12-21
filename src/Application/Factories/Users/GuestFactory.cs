using Application.Factories.Users.Abstractions;
using Application.Managers;
using Domain.Models.Users;
using static Infrastructure.Persistence.DefaultDbData;

namespace Application.Factories.Users;

public class GuestFactory(RoleManager roleManager) : IUserFactory
{
    private const Roles GuestFactoryRole = Roles.Guest;
    public async Task<User> CreateUser(string name, CancellationToken cancellationToken)
    {
        var role = (await roleManager.GetRoleByName(Enum.GetName(GuestFactoryRole)!, cancellationToken))!;

        return User.New(UserId.New(), name, role.Id);
    }
}
