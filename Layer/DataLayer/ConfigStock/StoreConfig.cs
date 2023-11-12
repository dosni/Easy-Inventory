using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class StoreConfig : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> modelBuilder)
        {
            modelBuilder.HasKey(b => b.StoreId);
            // jangan buat AutoNumber 
            modelBuilder.Property<int>("StoreId")
              .ValueGeneratedNever()
              .HasColumnType("int");
            modelBuilder.Property(b => b.StoreName).IsRequired().HasColumnType("varchar(50)");



        }
    }
}
