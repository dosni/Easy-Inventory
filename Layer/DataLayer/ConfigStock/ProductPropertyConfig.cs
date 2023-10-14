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
