using AutoMapper;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Order.Host.Configurations;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Models.Requests;
using Order.Host.Models.Response;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderItemService : BaseDataService<ApplicationDbContext>, IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IInternalHttpClientService _httpClient;
    private readonly OrderConfig _config;
    private readonly IMapper _mapper;

    public OrderItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderItemRepository orderItemRepository,
        IOrderRepository orderRepository,
        IOptions<OrderConfig> options,
        IInternalHttpClientService httpClientService,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _orderItemRepository = orderItemRepository;
        _orderRepository = orderRepository;
        _httpClient = httpClientService;
        _config = options.Value;
        _mapper = mapper;
    }

    public Task<int?> Add(int catalogItemId)
    {
        return ExecuteSafeAsync(() => _orderItemRepository.AddAsync(catalogItemId));
    }

    public Task<bool> Delete(int itemId)
    {
        return ExecuteSafeAsync(() => _orderItemRepository.DeleteAsync(itemId));
    }

    public Task<OrderListItemDto> Update(int itemId, int catalogItemId, int orderListId)
    {
        return ExecuteSafeAsync(async () =>
        {
            var orderList = await _orderRepository.GetOrderListById(orderListId);
            var result = await _orderItemRepository.UpdateAsync(new OrderListItemEntity() { ItemId = itemId, CatalogItemId = catalogItemId, OrderListId = orderListId, OrderList = orderList });
            return _mapper.Map<OrderListItemDto>(result);
        });
    }

    public async Task<ItemResponse> GetCatalogItemIdByItemId(int catalogItemId)
    {
        var result = await _httpClient.SendAsync<ItemResponse, CatalogItemByIdRequest>(
            $"{_config.CatalogApi}/item",
            HttpMethod.Post,
            new CatalogItemByIdRequest()
            {
                Id = catalogItemId
            });

        return result;
    }
}