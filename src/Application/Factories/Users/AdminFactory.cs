using Application.Factories.Users.Abstractions;
using Application.Managers;
using Domain.Models.Users;
using static Infrastructure.Persistence.DefaultDbData;

namespace Application.Factories.Users;

public class AdminFactory(RoleManager roleManager) : IUserFactory
{
    private const Roles AdminFactoryRole = Roles.Admin;
    public async Task<User> CreateUser(string name, CancellationToken cancellationToken)
    {
        var role = (await roleManager.GetRoleByName(Enum.GetName(AdminFactoryRole)!, cancellationToken))!;

        return User.New(UserId.New(), name, role.Id);
    }
}
