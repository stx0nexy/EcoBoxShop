namespace Catalog.Host.Models.Dtos;

public class CatalogSubCategoryDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public CatalogCategoryDto CatalogCategory { get; set; } = null!;
}