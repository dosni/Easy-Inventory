using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DataLayer.ConfigStock
{
    public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(b => b.Name).HasColumnType("varchar(256)");
            builder.Property(b => b.Id).HasColumnType("varchar(256)");
            builder.Property(b => b.UserName).HasColumnType("varchar(256)");
            builder.Property(b => b.NormalizedUserName).HasColumnType("varchar(256)");
            builder.Property(b => b.Email).HasColumnType("varchar(256)");
            builder.Property(b => b.NormalizedEmail).HasColumnType("varchar(256)");
            builder.Property(b => b.PasswordHash).HasColumnType("varchar(256)");
            builder.Property(b => b.SecurityStamp).HasColumnType("varchar(256)");
            builder.Property(b => b.ConcurrencyStamp).HasColumnType("varchar(256)");
            builder.Property(b => b.PhoneNumber).HasColumnType("varchar(16)");
          
        }
    }
}
