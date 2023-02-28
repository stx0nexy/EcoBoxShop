namespace Order.Host.Models.Response;

public class OrderSubCategoryResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public OrderCategoryResponse CatalogCategory { get; set; } = null!;
}