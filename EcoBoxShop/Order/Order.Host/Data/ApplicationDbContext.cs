using Order.Host.Data.Entities;
using Order.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Order.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<OrderListEntity> OrderLists { get; set; } = null!;
    public DbSet<OrderListItemEntity> OrderListItems { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new OrderListEntityTypeConfiguration());
        builder.ApplyConfiguration(new OrderListItemEntityTypeConfiguration());
    }
}