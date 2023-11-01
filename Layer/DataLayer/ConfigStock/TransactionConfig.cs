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
    public class TransactionConfig : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> modelBuilder)
        {
            modelBuilder.HasKey(b => b.TransactionId);
            // jangan buat AutoNumber 

            // jangan buat AutoNumber 
            modelBuilder.Property<int>("TransactionId")
              .ValueGeneratedNever()
              .HasColumnType("int");

            modelBuilder.Property(b => b.TransactionDate).IsRequired().HasColumnType("DateTime");
            modelBuilder.Property(b => b.ContactId).IsRequired(false).HasColumnType("int");
            modelBuilder.Property(b => b.Description).IsRequired().HasColumnType("varchar(2000)");
            modelBuilder.Property(b => b.CreatedBy).IsRequired().HasColumnType("varchar(100)");
            modelBuilder.Property(b => b.CreatedAt).IsRequired().HasColumnType("DateTime");



        }
    }
}
