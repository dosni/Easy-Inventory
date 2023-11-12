using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class ProductPropertyConfig : IEntityTypeConfiguration<ProductProperty>
    {
        public void Configure(EntityTypeBuilder<ProductProperty> modelBuilder)
        {
            modelBuilder.HasKey(b => b.PropetyId);
            // jangan buat AutoNumber 
            modelBuilder.Property<int>("PropetyId")
              .ValueGeneratedNever()
              .HasColumnType("int");
            modelBuilder.Property(b => b.ProductId).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.PropertyName).IsRequired().HasColumnType("varchar(50)");

        }
    }
}
