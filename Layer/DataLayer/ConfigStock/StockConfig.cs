using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many

namespace DataLayer.ConfigStock
{
    public class StockConfig : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> modelBuilder)
        {
            modelBuilder.HasKey(b => new { b.ProductId, b.StoreId });
            modelBuilder.Property(b => b.qty).IsRequired(true).HasColumnType("Decimal(18,2)");

            //one to many relation between product and Stock. Satu category bisa di banyak product 
            modelBuilder
               .HasOne(b => b.Product)
               .WithMany(b => b.Stocks).HasForeignKey(b => b.ProductId);

            //one to many relation between Store and Stock. Satu category bisa di banyak product 
            modelBuilder
               .HasOne(b => b.Store)
               .WithMany(b => b.Stocks).HasForeignKey(b => b.StoreId);

        }
    }
}
