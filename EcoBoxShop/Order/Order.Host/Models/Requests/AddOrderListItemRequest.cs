namespace Order.Host.Models.Requests;

public class AddOrderListItemRequest
{
    public int CatalogItem { get; set; }
    public int OrderListId { get; set; }
}