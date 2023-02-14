using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<int?> AddAsync(string title);
    Task<bool?> DeleteAsync(int id);
    Task<CatalogBrandEntity?> UpdateAsync(CatalogBrandEntity catalogBrand);
    Task<CatalogBrandEntity?> GetByIdAsync(int id);
}