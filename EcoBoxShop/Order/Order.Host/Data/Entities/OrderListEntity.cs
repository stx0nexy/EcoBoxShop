namespace Order.Host.Data.Entities;

public class OrderListEntity
{
    public int OrderListId { get; set; }
    public string? UserId { get; set; }
    public decimal TotalCost { get; set; }
    public List<OrderListItemEntity> OrderListItems { get; set; } = null!;
}