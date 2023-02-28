namespace Basket.Host.Models.Responses;

public class ItemResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFileName { get; set; } = null!;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    public BrandResponse CatalogBrand { get; set; } = null!;
    public BasketSubCategoryResponse CatalogSubCategory { get; set; } = null!;
}