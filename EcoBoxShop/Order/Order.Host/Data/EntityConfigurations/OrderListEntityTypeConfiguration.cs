using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Host.Data.Entities;

namespace Order.Host.Data.EntityConfigurations;

public class OrderListEntityTypeConfiguration : IEntityTypeConfiguration<OrderListEntity>
{
    public void Configure(EntityTypeBuilder<OrderListEntity> builder)
    {
        builder.ToTable("OrderList");

        builder.HasKey(ci => ci.OrderListId);

        builder.Property(ci => ci.OrderListId)
            .UseHiLo("order_list_hilo")
            .IsRequired();

        builder.Property(cb => cb.UserId)
            .IsRequired()
            .HasMaxLength(100);
        builder.HasMany(m => m.OrderListItems)
            .WithOne(w => w.OrderList);
    }
}