using Order.Host.Data;
using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<int?> AddAsync(string userId, decimal totalCost,  List<OrderListItemEntity> items);
    Task<bool?> DeleteAsync(int userId);
    Task<PaginatedItems<OrderListEntity?>> GetOrderListByUserId(string userId);
    Task<OrderListEntity?> GetOrderListById(int id);
}