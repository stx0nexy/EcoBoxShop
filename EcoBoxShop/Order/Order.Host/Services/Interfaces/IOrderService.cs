using Order.Host.Data;
using Order.Host.Models.Dtos;

namespace Order.Host.Services.Interfaces;

public interface IOrderService
{
    Task<PaginatedItems<OrderListDto?>> GetOrderListByUserIdAsync(string userId);
    Task<int?> Add(string userId, decimal totalCost, List<OrderListItemDto> items);
    Task<bool?> Delete(int userId);
}