using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogCategoryEntityTypeConfiguration : IEntityTypeConfiguration<CatalogCategoryEntity>
{
    public void Configure(EntityTypeBuilder<CatalogCategoryEntity> builder)
    {
        builder.ToTable("CatalogCategory");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_category_hilo")
            .IsRequired();

        builder.Property(cb => cb.Title)
            .IsRequired()
            .HasMaxLength(100);
    }
}