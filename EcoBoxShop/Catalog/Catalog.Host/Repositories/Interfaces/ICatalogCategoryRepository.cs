using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogCategoryRepository
{
    Task<int?> AddAsync(string title);
    Task<bool?> DeleteAsync(int id);
    Task<CatalogCategoryEntity?> UpdateAsync(CatalogCategoryEntity catalogBrand);
    Task<CatalogCategoryEntity?> GetByIdAsync(int id);
}