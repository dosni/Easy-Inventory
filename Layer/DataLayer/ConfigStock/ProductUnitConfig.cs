using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class ProductUnitConfig : IEntityTypeConfiguration<ProductUnit>
    {
        public void Configure(EntityTypeBuilder<ProductUnit> modelBuilder)
        {
            modelBuilder.HasKey(b => b.UnitId);
            // jangan buat AutoNumber 

            // jangan buat AutoNumber 
            modelBuilder.Property<int>("UnitId")
              .ValueGeneratedNever()
              .HasColumnType("int");

            modelBuilder.HasIndex(b => b.Unit).IsUnique(true);
            modelBuilder.Property(b => b.Unit).IsRequired().HasColumnType("varchar(50)");

        }
    }
}
