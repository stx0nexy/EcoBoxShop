namespace Basket.Host.Models.Requests;

public class RemoveRequest
{
    public string UserId { get; set; } = null!;
    public int ItemId { get; set; }
}