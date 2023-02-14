using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogItemEntityTypeConfiguration : IEntityTypeConfiguration<CatalogItemEntity>
{
    public void Configure(EntityTypeBuilder<CatalogItemEntity> builder)
    {
        builder.ToTable("Catalog");

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_hilo")
            .IsRequired();

        builder.Property(ci => ci.Title)
            .IsRequired(true)
            .HasMaxLength(100);

        builder.Property(ci => ci.SubTitle)
            .IsRequired(true)
            .HasMaxLength(200);

        builder.Property(ci => ci.Description)
            .IsRequired(true);

        builder.Property(ci => ci.PictureFileName)
            .IsRequired(false);

        builder.Property(ci => ci.Price)
            .IsRequired(true);

        builder.Property(ci => ci.AvailableStock)
            .IsRequired(true);

        builder.HasOne(ci => ci.CatalogBrand)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogBrandId);

        builder.HasOne(ci => ci.CatalogSubCategory)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogSubCategoryId);
    }
}