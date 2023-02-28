namespace Basket.Host.Models.Requests;

public class AddRequest
{
    public string UserId { get; set; } = null!;
    public int ItemId { get; set; }
    public int CatalogItemId { get; set; }
}