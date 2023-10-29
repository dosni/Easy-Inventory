using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ConfigStock
{
    public class ProductSKUConfig : IEntityTypeConfiguration<ProductSku>
    {
        public void Configure(EntityTypeBuilder<ProductSku> modelBuilder)
        {
            modelBuilder.HasKey(b => b.SkuId);

            // jangan buat AutoNumber 
            modelBuilder.Property<int>("SkuId")
              .ValueGeneratedNever()
              .HasColumnType("int");

            modelBuilder.Property(b => b.SKU).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.HasIndex(b => b.SKU).IsUnique(true);

            modelBuilder.Property(b => b.ProductId).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.Price).IsRequired(true).HasColumnType("Decimal(18,2)");

            //one to many relation between product and Sku. Satu category bisa di banyak product 
            modelBuilder
               .HasOne(b => b.Product)
               .WithMany(b => b.ProductSkus).HasForeignKey(b => b.ProductId);
        }
    }
}
