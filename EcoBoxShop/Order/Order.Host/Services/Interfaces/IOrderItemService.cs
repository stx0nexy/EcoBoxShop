using Order.Host.Models.Dtos;
using Order.Host.Models.Response;

namespace Order.Host.Services.Interfaces;

public interface IOrderItemService
{
    Task<int?> Add(int catalogItemId, int orderListId);
    Task<bool?> Delete(int itemId);
    Task<OrderListItemDto> Update(int itemId, int catalogItemId, int orderListId);
}