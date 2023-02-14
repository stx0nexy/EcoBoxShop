namespace Catalog.Host.Models.Dtos;

public class CatalogItemDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    public CatalogBrandDto CatalogBrand { get; set; } = null!;
    public CatalogSubCategoryDto CatalogSubCategory { get; set; } = null!;
}