using Domain.Models.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new RoleId(x));

        builder.Property(x => x.Name).IsRequired().HasColumnType("varchar(255)");

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .HasConstraintName("FK_Role_Users_RoleId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
