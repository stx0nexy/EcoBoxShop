﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Order.Host.Data;

#nullable disable

namespace Order.Host.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.HasSequence("order_list_hilo")
                .IncrementsBy(10);

            modelBuilder.HasSequence("order_list_item_hilo")
                .IncrementsBy(10);

            modelBuilder.Entity("Order.Host.Data.Entities.OrderListEntity", b =>
                {
                    b.Property<int>("OrderListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("OrderListId"), "order_list_hilo");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("OrderListId");

                    b.ToTable("OrderList", (string)null);
                });

            modelBuilder.Entity("Order.Host.Data.Entities.OrderListItemEntity", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseHiLo(b.Property<int>("ItemId"), "order_list_item_hilo");

                    b.Property<int>("CatalogItemId")
                        .HasColumnType("integer");

                    b.Property<int>("OrderListId")
                        .HasColumnType("integer");

                    b.HasKey("ItemId");

                    b.HasIndex("OrderListId");

                    b.ToTable("OrderListItem", (string)null);
                });

            modelBuilder.Entity("Order.Host.Data.Entities.OrderListItemEntity", b =>
                {
                    b.HasOne("Order.Host.Data.Entities.OrderListEntity", "OrderList")
                        .WithMany("OrderListItems")
                        .HasForeignKey("OrderListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderList");
                });

            modelBuilder.Entity("Order.Host.Data.Entities.OrderListEntity", b =>
                {
                    b.Navigation("OrderListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
