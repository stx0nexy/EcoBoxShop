using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;

namespace Order.Host.Models.Requests
{
    public class AddOrderListRequest
    {
        public string UserId { get; set; } = null!;
        public List<OrderListItemDto> BasketList { get; set; } = null!;
    }
}
