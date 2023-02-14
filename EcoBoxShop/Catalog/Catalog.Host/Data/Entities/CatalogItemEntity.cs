namespace Catalog.Host.Data.Entities;

public class CatalogItemEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFileName { get; set; } = null!;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    public int CatalogBrandId { get; set; }
    public CatalogBrandEntity? CatalogBrand { get; set; } = null!;
    public int CatalogSubCategoryId { get; set; }
    public CatalogSubCategoryEntity? CatalogSubCategory { get; set; } = null!;
}