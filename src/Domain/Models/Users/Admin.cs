using Domain.Models.Roles;

namespace Domain.Models.Users;

public class Admin(UserId id, string name, RoleId roleId) : User(id, name, roleId);
