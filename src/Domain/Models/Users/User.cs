using Domain.Models.Roles;

namespace Domain.Models.Users;

public class User(UserId id, string name, RoleId roleId)
{
    public UserId Id { get; } = id;
    public string Name { get; private set; } = name;

    public RoleId RoleId { get; private set; } = roleId;
    public Role? Role { get; private set; }

    public static User New(UserId id, string name, RoleId roleId) => new(id, name, roleId);

    public void UpdateDetails(string name)
    {
        Name = name;
    }

    public void UpdateRole(RoleId roleId)
    {
        RoleId = roleId;
        Role = null;
    }
}
