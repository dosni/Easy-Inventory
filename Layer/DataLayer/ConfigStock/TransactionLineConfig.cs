using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DataLayer.ConfigStock
{
    public class TransactionLineConfig : IEntityTypeConfiguration<TransactionLine>
    {
        public void Configure(EntityTypeBuilder<TransactionLine> modelBuilder)
        {
            modelBuilder.HasKey("LineId","TransactionId") ;

            modelBuilder.Property(b => b.SkuId).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.StoreId).IsRequired().HasColumnType("int");
            modelBuilder.Property(b => b.Qty).IsRequired().HasColumnType("Decimal(18,2)");
            modelBuilder.Property(b => b.Price).IsRequired().HasColumnType("Decimal(18,2)");

            //one to many relation between productSku and product Transaction. Satu produk bisa banyak transaksi
            modelBuilder
               .HasOne(b => b.ProductSku)
               .WithMany(b => b.TransactionLines).HasForeignKey(b => b.SkuId);

            //one to many relation between product and product Transaction. Satu produk bisa banyak transaksi
            modelBuilder
               .HasOne(b => b.Store)
               .WithMany(b => b.LineTransactions).HasForeignKey(b => b.StoreId);

        }
    }
}
