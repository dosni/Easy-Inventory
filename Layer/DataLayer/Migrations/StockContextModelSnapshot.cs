﻿// <auto-generated />
using DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataLayer.Migrations
{
    [DbContext(typeof(StockContext))]
    partial class StockContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataLayer.EntityStock.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ProductImage")
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DataLayer.EntityStock.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("CategoryId");

                    b.HasIndex("Category")
                        .IsUnique();

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("DataLayer.EntityStock.ProductProperty", b =>
                {
                    b.Property<int>("PropetyId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("PropetyId");

                    b.ToTable("ProductProperties");
                });

            modelBuilder.Entity("DataLayer.EntityStock.ProductSku", b =>
                {
                    b.Property<string>("SKU")
                        .HasColumnType("varchar(50)");

                    b.Property<decimal>("Price")
                        .HasColumnType("Decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("SKU");

                    b.ToTable("ProductSkus");
                });

            modelBuilder.Entity("DataLayer.EntityStock.ProductValue", b =>
                {
                    b.Property<string>("SKU")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<string>("PropertyValue")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("SKU", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("ProductValues");
                });

            modelBuilder.Entity("DataLayer.EntityStock.Stock", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<decimal>("qty")
                        .HasColumnType("Decimal(18,2)");

                    b.HasKey("ProductId", "StoreId");

                    b.HasIndex("StoreId");

                    b.ToTable("stocks");
                });

            modelBuilder.Entity("DataLayer.EntityStock.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnType("int");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("StoreId");

                    b.ToTable("Stores");
                });

            modelBuilder.Entity("DataLayer.EntityStock.Product", b =>
                {
                    b.HasOne("DataLayer.EntityStock.ProductCategory", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("DataLayer.EntityStock.ProductValue", b =>
                {
                    b.HasOne("DataLayer.EntityStock.ProductProperty", "ProductProperty")
                        .WithMany("ProductValues")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.EntityStock.ProductSku", "ProductSku")
                        .WithMany("ProductValues")
                        .HasForeignKey("SKU")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductProperty");

                    b.Navigation("ProductSku");
                });

            modelBuilder.Entity("DataLayer.EntityStock.Stock", b =>
                {
                    b.HasOne("DataLayer.EntityStock.Product", "Product")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.EntityStock.Store", "Store")
                        .WithMany("Stocks")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("DataLayer.EntityStock.Product", b =>
                {
                    b.Navigation("Stocks");
                });

            modelBuilder.Entity("DataLayer.EntityStock.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DataLayer.EntityStock.ProductProperty", b =>
                {
                    b.Navigation("ProductValues");
                });

            modelBuilder.Entity("DataLayer.EntityStock.ProductSku", b =>
                {
                    b.Navigation("ProductValues");
                });

            modelBuilder.Entity("DataLayer.EntityStock.Store", b =>
                {
                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
