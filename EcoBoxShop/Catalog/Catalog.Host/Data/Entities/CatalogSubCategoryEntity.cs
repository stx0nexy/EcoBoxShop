namespace Catalog.Host.Data.Entities;

public class CatalogSubCategoryEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public int CatalogCategoryId { get; set; }
    public CatalogCategoryEntity? CatalogCategory { get; set; } = null!;
}