namespace Order.Host.Models.Requests;

public class UpdateOrderListItemRequest
{
    public int ItemId { get; set; }
    public int CatalogItemId { get; set; }
    public string Title { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }
    public int OrderListId { get; set; }
}