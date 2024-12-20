using Domain.Models.Users;

namespace Domain.Models.Roles;

public class Role(RoleId id, string name)
{
    public RoleId Id { get; } = id;
    public string Name { get; private set; } = name;

    public ICollection<User> Users { get; } = new HashSet<User>();

    public static Role New(RoleId id, string name) => new(id, name);

    public void UpdateDetails(string name)
    {
        Name = name;
    }
}
