using Domain.Models.Roles;

namespace Infrastructure.Persistence;

public static class DefaultDbData
{
    public enum Roles
    {
        Guest = 1,
        User = 2,
        Admin = 3
    }

    public static IEnumerable<Role> GetRoles()
    {
        foreach (var role in Enum.GetNames<Roles>())
        {
            yield return Role.New(new RoleId(Guid.NewGuid()), role);
        }
    }
}
