using CarMarket.Core.Image.Domain;
using CarMarket.Core.User.Domain;
using CarMarket.Data.Car.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarMarket.Data.Configuration
{
    public class CarConfiguration : IEntityTypeConfiguration<CarEntity>
    {
        public void Configure(EntityTypeBuilder<CarEntity> builder)
        {
            builder.ToTable("Cars").HasKey(p => p.Id);
        }
    }

    public class PermissonConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions").HasKey(p => p.Id);
        }
    }

    public class ImageConfiguration : IEntityTypeConfiguration<ImageModel>
    {
        public void Configure(EntityTypeBuilder<ImageModel> builder)
        {
            builder.ToTable("Images").HasKey(p => p.Id);
        }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            //builder.HasKey(keys => new { keys.UserId, keys.RoleId });

            builder.HasData(
                new IdentityUserRole<string> // Admin
                {
                    RoleId = "qwe",
                    UserId = "qwe"
                },
                new IdentityUserRole<string> // User
                {
                    RoleId = "qwerty",
                    UserId = "qwerty"
                });
        }
    }

    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Guest",
                    NormalizedName = "GUEST"
                },
                new IdentityRole
                {
                    Id = "qwerty",
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Id = "qwe",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                }
            );
        }
    }
}
