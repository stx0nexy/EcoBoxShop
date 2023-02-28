namespace Order.Host.Models.Requests;

public class UpdateOrderListItemRequest
{
    public int ItemId { get; set; }
    public int CatalogItemId { get; set; }
    public int OrderListId { get; set; }
}