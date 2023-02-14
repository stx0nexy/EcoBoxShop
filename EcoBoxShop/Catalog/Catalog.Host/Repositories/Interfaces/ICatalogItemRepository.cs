using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItemEntity>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter);
    Task<int?> AddAsync(string title, string subTitle, string description, string pictureFileName, decimal price, int availableStock, int catalogBrandId, int catalogSubCategoryId);
    Task<CatalogItemEntity?> GetByIdAsync(int id);
    Task<PaginatedItems<CatalogItemEntity>> GetByBrandAsync(string brand, int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogItemEntity>> GetBySubCategoryAsync(string category, int pageIndex, int pageSize);
    Task<PaginatedItems<CatalogBrandEntity>> GetBrandsAsync();
    Task<PaginatedItems<CatalogCategoryEntity>> GetCategoriesAsync();
    Task<PaginatedItems<CatalogSubCategoryEntity>> GetSubCategoriesAsync();
    Task<bool?> DeleteAsync(int id);
    Task<CatalogItemEntity?> UpdateAsync(CatalogItemEntity catalogItem);
    Task<PaginatedItems<CatalogSubCategoryEntity>> GetSubCategoryByCategoryAsync(string category);
}