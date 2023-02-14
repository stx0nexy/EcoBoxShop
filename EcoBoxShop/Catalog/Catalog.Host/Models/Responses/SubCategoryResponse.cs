using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Responses;

public class SubCategoryResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = null!;
    public CatalogCategoryDto? Category { get; set; }
}