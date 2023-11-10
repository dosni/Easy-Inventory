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
    public class ApplicationActivityConfig : IEntityTypeConfiguration<ApplicationActivity>
    {
        public void Configure(EntityTypeBuilder<ApplicationActivity> modelBuilder)
        {
            modelBuilder.HasKey(b => b.Id);
            modelBuilder.Property(b => b.UserId).IsRequired().HasColumnType("varchar(256)");
            modelBuilder.Property(b => b.message).IsRequired().HasColumnType("varchar").HasMaxLength(100);
        }
    }
}
