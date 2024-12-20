using Domain.Models.Roles;

namespace Domain.Models.Users;

public class Guest(UserId id, string name, RoleId roleId) : User(id, name, roleId);