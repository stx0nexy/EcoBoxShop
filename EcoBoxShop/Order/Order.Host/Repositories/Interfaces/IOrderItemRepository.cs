using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces;

public interface IOrderItemRepository
{
    Task<int?> AddAsync(int catalogItemId);
    Task<bool> DeleteAsync(int itemId);
    Task<OrderListItemEntity> UpdateAsync(OrderListItemEntity orderListItem);
    Task<OrderListItemEntity?> GetByIdAsync(int id);
    Task<int> GetCatalogItemIdByItemId(int itemId);
}