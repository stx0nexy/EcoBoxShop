using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogCategoryService
{
    Task<int?> Add(string category);
    Task<bool?> Delete(int id);
    Task<CatalogCategoryDto> Update(int id, string category);
}