using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(string title, string subTitle, string description, string pictureFileName, decimal price, int availableStock, int catalogBrandId, int catalogSubCategoryId);
    Task<bool?> Delete(int id);
    Task<CatalogItemDto> Update(int id, string title, string subTitle, string description, string pictureFileName, decimal price, int availableStock, int catalogBrandId, int catalogSubCategoryId);
}