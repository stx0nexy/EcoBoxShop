namespace Basket.Host.Models.BasketModels;

public class UserBasket
{
    public string? UserId { get; set; }
    public List<BasketItem?> BasketList { get; set; } = new List<BasketItem?>();
}