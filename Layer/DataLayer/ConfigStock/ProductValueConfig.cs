using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class ProductValueConfig : IEntityTypeConfiguration<ProductValue>
    {
        public void Configure(EntityTypeBuilder<ProductValue> modelBuilder)
        {
            modelBuilder.HasKey(b => new { b.SkuId, b.PropertyId });
            modelBuilder.Property(b => b.PropertyValue).IsRequired().HasColumnType("varchar(50)");

            //one to many relation between productProperty and ProductValue.  
            modelBuilder
               .HasOne(b => b.ProductProperty)
               .WithMany(b => b.ProductValues).HasForeignKey(b => b.PropertyId);

            //one to many relation between productSku and ProductValue.  
            modelBuilder
               .HasOne(b => b.ProductSku)
               .WithMany(b => b.ProductValues).HasForeignKey(b => b.SkuId);



        }
    }
}
