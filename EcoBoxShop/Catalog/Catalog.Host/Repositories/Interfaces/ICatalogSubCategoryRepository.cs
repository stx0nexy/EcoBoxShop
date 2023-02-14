using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogSubCategoryRepository
{
    Task<int?> AddAsync(string title, int catalogCategoryId);
    Task<bool?> DeleteAsync(int id);
    Task<CatalogSubCategoryEntity?> UpdateAsync(CatalogSubCategoryEntity catalogSubCategory);
    Task<CatalogSubCategoryEntity?> GetByIdAsync(int id);
}