using DataLayer.EntityStock;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.ConfigStock
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> modelBuilder)
        {
            modelBuilder.HasKey(b => b.ContactId);
            // jangan buat AutoNumber 
            modelBuilder.Property<int>("ContactId")
              .ValueGeneratedNever()
              .HasColumnType("int");

            modelBuilder.Property(b => b.type).IsRequired().HasColumnType("char(1)");
            modelBuilder.Property(b => b.Name).IsRequired().HasColumnType("varchar(50)");
            modelBuilder.HasIndex(b => b.Name).IsUnique(true);

            modelBuilder.Property(b => b.Address).IsRequired(false).HasColumnType("varchar(200)");
            modelBuilder.Property(b => b.Phone).IsRequired(false).HasColumnType("varchar(15)");
            modelBuilder.Property(b => b.Description).IsRequired(false).HasColumnType("varchar(200)");


        }
    }
}
