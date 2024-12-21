using Domain.Models.Roles;
using Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new UserId(x));

        builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(255)");

        builder.Property(x => x.RoleId).HasConversion(x => x.Value, x => new RoleId(x));

        builder.HasOne(x => x.Role)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("FK_Users_Roles_RoleId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
