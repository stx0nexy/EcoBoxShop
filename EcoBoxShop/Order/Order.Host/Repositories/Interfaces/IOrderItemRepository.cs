using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces;

public interface IOrderItemRepository
{
    Task<int?> AddAsync(int catalogItemId, int orderListId, string title, string subTitle, string pictureUrl, decimal price);
    Task<bool?> DeleteAsync(int itemId);
    Task<OrderListItemEntity> UpdateAsync(OrderListItemEntity orderListItem);
    Task<OrderListItemEntity?> GetByIdAsync(int id);
    Task<int> GetCatalogItemIdByItemId(int itemId);
}