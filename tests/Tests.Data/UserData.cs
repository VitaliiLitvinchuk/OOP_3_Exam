using Domain.Models.Roles;
using Domain.Models.Users;

namespace Tests.Data;

public static class UserData
{
    public static User CreateUser(RoleId roleId) => User.New(new UserId(new Guid("5b2c93c8-0fcd-4760-9a5a-3a3e4e5a6bce")), "User", roleId);
}
