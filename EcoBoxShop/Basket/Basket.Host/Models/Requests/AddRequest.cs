namespace Basket.Host.Models.Requests;

public class AddRequest
{
    public string UserId { get; set; } = null!;
    public int ItemId { get; set; }
    public int CatalogItemId { get; set; }
    public string Title { get; set; } = null!;
    public string SubTitle { get; set; } = null!;
    public string PictureUrl { get; set; } = null!;
    public decimal Price { get; set; }
}