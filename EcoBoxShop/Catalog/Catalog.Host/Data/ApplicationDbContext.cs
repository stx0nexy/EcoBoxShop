using Catalog.Host.Data.Entities;
using Catalog.Host.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CatalogItemEntity> CatalogItems { get; set; } = null!;
    public DbSet<CatalogBrandEntity> CatalogBrands { get; set; } = null!;
    public DbSet<CatalogCategoryEntity> CatalogCategories { get; set; } = null!;
    public DbSet<CatalogSubCategoryEntity> CatalogSubCategories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogCategoryEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
        builder.ApplyConfiguration(new CatalogSubCategoryEntityTypeConfiguration());
    }
}