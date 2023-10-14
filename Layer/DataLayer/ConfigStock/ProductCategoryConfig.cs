using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> modelBuilder)
        {
            modelBuilder.HasKey(b => b.CategoryId);
            // jangan buat AutoNumber 
            modelBuilder.Property<int>("CategoryId")
              .ValueGeneratedNever()
              .HasColumnType("int");
            modelBuilder.HasIndex(b => b.Category).IsUnique(true);
            modelBuilder.Property(b => b.Category).IsRequired().HasColumnType("varchar(50)");

        }
    }
}
