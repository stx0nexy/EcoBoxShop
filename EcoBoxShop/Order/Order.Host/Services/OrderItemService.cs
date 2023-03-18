using AutoMapper;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderItemService : BaseDataService<ApplicationDbContext>, IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderItemRepository orderItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    public Task<int?> Add(int catalogItemId, int orderListId, string title, string subTitle, string pictureUrl, decimal price)
    {
        return ExecuteSafeAsync(() => _orderItemRepository.AddAsync(catalogItemId, orderListId, title, subTitle, pictureUrl, price));
    }

    public Task<bool?> Delete(int itemId)
    {
        return ExecuteSafeAsync(() => _orderItemRepository.DeleteAsync(itemId));
    }

    public Task<OrderListItemDto> Update(int itemId, int catalogItemId, int orderListId, string title, string subTitle, string pictureUrl, decimal price)
    {
        return ExecuteSafeAsync(async () =>
        {
            var result = await _orderItemRepository.UpdateAsync(new OrderListItemEntity() { ItemId = itemId, CatalogItemId = catalogItemId, OrderListId = orderListId, Title = title, SubTitle = subTitle, Price = price, PictureUrl = pictureUrl });
            return _mapper.Map<OrderListItemDto>(result);
        });
    }
}