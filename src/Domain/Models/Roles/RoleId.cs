namespace Domain.Models.Roles;

public record RoleId(Guid Value)
{
    public static RoleId New() => new(Guid.NewGuid());
    public override string ToString() => Value.ToString();
}