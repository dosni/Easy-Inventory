using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class ProductTransferConfig : IEntityTypeConfiguration<ProductTransfer>
    {
        public void Configure(EntityTypeBuilder<ProductTransfer> modelBuilder)
        {
            modelBuilder.HasKey(b => b.TransactionId);
            // jangan buat AutoNumber 

            // jangan buat AutoNumber 
            modelBuilder.Property<int>("TransactionId")
              .ValueGeneratedNever()
              .HasColumnType("int");


            modelBuilder.Property(b => b.TransactionDate).IsRequired().HasColumnType("DateTime");
            modelBuilder.Property(b => b.SkuId).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.StoreIdFrom).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.StoreIdTo).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.Qty).IsRequired().HasColumnType("Decimal(18,2)");

            modelBuilder.Property(b => b.Description).IsRequired().HasColumnType("varchar(2000)");
            modelBuilder.Property(b => b.CreatedBy).IsRequired().HasColumnType("varchar(100)");
            modelBuilder.Property(b => b.CreatedAt).IsRequired().HasColumnType("DateTime");

            //one to many relation between product and product Transfer. Satu produk bisa banyak transaksi
            modelBuilder
               .HasOne(b => b.ProductSku)
               .WithMany(b => b.ProductTransfers).HasForeignKey(b => b.SkuId);

       

        }
    }
}
