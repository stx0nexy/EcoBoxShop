using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogSubCategoryService
{
    Task<int?> Add(string category, int catalogCategoryId);
    Task<bool?> Delete(int id);
    Task<CatalogSubCategoryDto> Update(int id, string category, int catalogCategoryId);
}