using Order.Host.Data.Entities;

namespace Order.Host.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<int?> AddAsync(string userId,  List<OrderListItemEntity> items);
    Task<bool> DeleteAsync(int userId);
    Task<OrderListEntity?> GetOrderListByUserId(string userId);
    Task<OrderListEntity?> GetOrderListById(int id);
}