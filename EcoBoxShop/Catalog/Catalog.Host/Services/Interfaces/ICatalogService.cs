using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogService
{
    Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItems(int pageSize, int pageIndex, Dictionary<CatalogItemFilter, int>? filters);
    Task<CatalogItemDto> GetCatalogItemById(int id);
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemByBrand(string brand, int pageIndex, int pageSize);
    Task<PaginatedItemsResponse<CatalogItemDto>> GetCatalogItemBySubCategory(string category, int pageIndex, int pageSize);
    Task<ItemsResponse<CatalogSubCategoryDto>> GetSubCategoriesByCategory(string category);
    Task<ItemsResponse<CatalogBrandDto>> GetCatalogBrands();
    Task<ItemsResponse<CatalogCategoryDto>> GetCatalogCategories();
    Task<ItemsResponse<CatalogSubCategoryDto>> GetCatalogSubCategories();
}