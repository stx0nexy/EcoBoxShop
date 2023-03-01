using Order.Host.Models.Dtos;

namespace Order.Host.Services.Interfaces;

public interface IOrderService
{
    Task<OrderListDto?> GetOrderListByUserIdAsync(string userId);
    Task<int?> Add(string userId, List<OrderListItemDto> items);
    Task<bool?> Delete(int userId);
}