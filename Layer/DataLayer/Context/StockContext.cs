using DataLayer.ConfigStock;
using DataLayer.EntityStock;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class StockContext : DbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.ApplyConfiguration(new ProductCategoryConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new StoreConfig());
            modelBuilder.ApplyConfiguration(new StockConfig());

            modelBuilder.ApplyConfiguration(new ProductPropertyConfig());
            modelBuilder.ApplyConfiguration(new ProductSKUConfig());
            modelBuilder.ApplyConfiguration(new ProductValueConfig());

            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(
           DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }


        public DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Store> Stores { get; set; } = null!;   
        public DbSet<Stock> stocks { get; set; } = null!;

        public DbSet<ProductProperty> ProductProperties { get; set; } = null!;
        public DbSet<ProductSku> ProductSkus { get; set; } = null!;
        public DbSet<ProductValue> ProductValues { get; set; } = null!;

    }
}
