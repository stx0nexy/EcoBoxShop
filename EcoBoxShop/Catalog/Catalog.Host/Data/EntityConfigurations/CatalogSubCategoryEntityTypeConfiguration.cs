using Catalog.Host.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Host.Data.EntityConfigurations;

public class CatalogSubCategoryEntityTypeConfiguration : IEntityTypeConfiguration<CatalogSubCategoryEntity>
{
    public void Configure(EntityTypeBuilder<CatalogSubCategoryEntity> builder)
    {
        builder.ToTable("CatalogSubCategory");

        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .UseHiLo("catalog_sub_category_hilo")
            .IsRequired();

        builder.Property(cb => cb.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(ci => ci.CatalogCategory)
            .WithMany()
            .HasForeignKey(ci => ci.CatalogCategoryId);
    }
}