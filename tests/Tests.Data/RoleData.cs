using Domain.Models.Roles;

namespace Tests.Data;

public static class RoleData
{
    public static readonly Role Guest = Role.New(new RoleId(new Guid("5b2c93c8-0fcd-4760-9a5a-3a3e4e5a6bce")), "Guest");
}
