using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class ApplicationActivityConfig : IEntityTypeConfiguration<ApplicationActivity>
    {
        public void Configure(EntityTypeBuilder<ApplicationActivity> modelBuilder)
        {
            modelBuilder.HasKey(b => b.Id);
            modelBuilder.Property(b => b.UserId).IsRequired().HasColumnType("varchar(256)");
            modelBuilder.Property(b => b.message).IsRequired().HasColumnType("varchar").HasMaxLength(100);
        }
    }
}
