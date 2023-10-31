//using DataLayer.EntityStock;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace DataLayer.ConfigStock
//{
//    public class ProductTransactionConfig : IEntityTypeConfiguration<ProductTransaction>
//    {


//        public void Configure(EntityTypeBuilder<ProductTransaction> modelBuilder)
//        {
//            modelBuilder.HasKey(b => b.TransactionId);
//            // jangan buat AutoNumber 

//            // jangan buat AutoNumber 
//            modelBuilder.Property<int>("TransactionId")
//              .ValueGeneratedNever()
//              .HasColumnType("int");

//            modelBuilder.Property(b => b.TransactionType).IsRequired().HasColumnType("char(1)");
//            modelBuilder.Property(b => b.TransactionDate).IsRequired().HasColumnType("DateTime");
//            modelBuilder.Property(b => b.SkuId).IsRequired().HasColumnType("int");
//            modelBuilder.Property(b => b.StoreId).IsRequired().HasColumnType("int");
//            modelBuilder.Property(b => b.ContactId).IsRequired(false).HasColumnType("int");
//            modelBuilder.Property(b => b.Qty).IsRequired().HasColumnType("Decimal(18,2)");
//            modelBuilder.Property(b => b.Price).IsRequired().HasColumnType("Decimal(18,2)");
//            modelBuilder.Property(b => b.Description).IsRequired().HasColumnType("varchar(2000)");
//            modelBuilder.Property(b => b.CreatedBy).IsRequired().HasColumnType("varchar(100)");
//            modelBuilder.Property(b => b.CreatedAt).IsRequired().HasColumnType("DateTime");

//            //one to many relation between productSku and product Transaction. Satu produk bisa banyak transaksi
//            modelBuilder
//               .HasOne(b => b.ProductSku)
//               .WithMany(b => b.ProductTransactions).HasForeignKey(b => b.SkuId);

//            //one to many relation between product and product Transaction. Satu produk bisa banyak transaksi
//            modelBuilder
//               .HasOne(b => b.Store)
//               .WithMany(b => b.ProductTransactions).HasForeignKey(b => b.StoreId);



//        }
//    }
//}
