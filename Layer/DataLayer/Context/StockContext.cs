using DataLayer.ConfigStock;
using DataLayer.EntityStock;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class StockContext : IdentityDbContext
    {
        public StockContext(DbContextOptions<StockContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationRoleConfig());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfig());
            modelBuilder.ApplyConfiguration(new ApplicationActivityConfig());

            modelBuilder.ApplyConfiguration(new ProductCategoryConfig());
            modelBuilder.ApplyConfiguration(new ProductUnitConfig());
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new StoreConfig());

            modelBuilder.Entity<Store>().HasData(
                   new Store { StoreId = 1, StoreName= "Default" }
               );

            modelBuilder.ApplyConfiguration(new StockConfig());

            modelBuilder.ApplyConfiguration(new ProductPropertyConfig());
            modelBuilder.ApplyConfiguration(new ProductSKUConfig());
            modelBuilder.ApplyConfiguration(new ProductValueConfig());

         //   modelBuilder.ApplyConfiguration(new ProductTransactionConfig());
            modelBuilder.ApplyConfiguration(new ProductTransferConfig());
            modelBuilder.ApplyConfiguration(new TransactionConfig());
            modelBuilder.ApplyConfiguration(new TransactionLineConfig());

            modelBuilder.ApplyConfiguration(new ContactConfig());

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

       // public DbSet<ProductTransaction> ProductTransactions { get; set; } = null!;
        public DbSet<ProductTransfer> ProductTransfers { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<TransactionLine> TransactionLines { get; set; } = null!;
        public DbSet<ProductUnit> ProductUnits { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;

        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationActivity> ApplicationActivities { get; set; }

    }
}
