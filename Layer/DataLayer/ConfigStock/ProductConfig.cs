using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {


        public void Configure(EntityTypeBuilder<Product> modelBuilder)
        {
            modelBuilder.HasKey(b => b.ProductId);
            // jangan buat AutoNumber 

            // jangan buat AutoNumber 
            modelBuilder.Property<int>("ProductId")
              .ValueGeneratedNever()
              .HasColumnType("int");

            modelBuilder.Property(b => b.ProductName).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.Property(b => b.ProductImage).IsRequired(false).HasColumnType("varchar(200)");
            modelBuilder.Property(b => b.ProductDescription).IsRequired(false).HasColumnType("varchar(200)");

            //one to many relation between product and category. Satu category bisa di banyak product 
            modelBuilder
               .HasOne(b => b.ProductCategory)
               .WithMany(b => b.Products).HasForeignKey(b => b.CategoryId);

          //  one to many relation between product and ProductSKU. Satu Produk banyak product SKU
            modelBuilder
               .HasOne(b => b.ProductUnit)
               .WithMany(b => b.Products).HasForeignKey(b => b.UnitId);
        }
    }
}
