namespace Basket.Host.Models.Responses;

public class BasketSubCategoryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public CategoryResponse CatalogCategory { get; set; } = null!;
}