using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations;

public class OrderListItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderListItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderListItemEntity> builder)
    {
        builder.ToTable("OrderListItem");

        builder.HasKey(ci => ci.ItemId);

        builder.Property(ci => ci.ItemId)
            .UseHiLo("order_list_item_hilo")
            .IsRequired();

        builder.Property(cb => cb.ItemId)
            .IsRequired();

        builder.Property(ci => ci.CatalogItemId)
            .IsRequired();

        builder.HasOne(ci => ci.OrderList)
            .WithMany(w => w.OrderListItems)
            .HasForeignKey(ci => ci.OrderListId);
    }
}