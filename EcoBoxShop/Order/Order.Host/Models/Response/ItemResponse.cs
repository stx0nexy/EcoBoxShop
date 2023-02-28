namespace Order.Host.Models.Response;

public class ItemResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PictureFileName { get; set; } = null!;
    public decimal Price { get; set; }
    public int AvailableStock { get; set; }
    public OrderBrandResponse CatalogBrand { get; set; } = null!;
    public OrderSubCategoryResponse CatalogSubCategory { get; set; } = null!;
}