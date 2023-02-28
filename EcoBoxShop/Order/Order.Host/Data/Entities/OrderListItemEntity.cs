namespace Order.Host.Data.Entities;

public class OrderListItemEntity
{
    public int ItemId { get; set; }
    public int CatalogItemId { get; set; }

    public int OrderListId { get; set; }
    public OrderListEntity? OrderList { get; set; } = null!;
}