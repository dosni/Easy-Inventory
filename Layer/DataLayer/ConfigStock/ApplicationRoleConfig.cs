using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ConfigStock
{
    public class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.Property(b => b.Id).HasColumnType("varchar(256)");
            builder.Property(b => b.Name).HasColumnType("varchar(256)");
            builder.Property(b => b.NormalizedName).HasColumnType("varchar(256)");
            builder.Property(b => b.ConcurrencyStamp).HasColumnType("varchar(256)");
            builder.Property(b => b.Description).HasColumnType("varchar(100)");
        }
    }
}
