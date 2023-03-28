namespace Basket.Host.Models.Responses;

public class BasketResponse<T>
{
    public int BasketId { get; set; }
    public string? UserId { get; set; }
    public decimal TotalCost { get; set; }
    public List<T> BasketList { get; set; } = new List<T>();
}