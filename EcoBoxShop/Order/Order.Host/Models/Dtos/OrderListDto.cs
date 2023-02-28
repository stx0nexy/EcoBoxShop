namespace Order.Host.Models.Dtos;

public class OrderListDto
{
    public int OrderListId { get; set; }
    public string? UserId { get; set; }
    public List<OrderListItemDto> OrderListItems { get; set; } = null!;
}