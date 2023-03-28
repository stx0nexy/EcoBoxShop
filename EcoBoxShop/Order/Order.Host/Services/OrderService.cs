using AutoMapper;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services;

public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderRepository orderRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedItems<OrderListDto?>> GetOrderListByUserIdAsync(string userId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderRepository.GetOrderListByUserId(userId);
            return new PaginatedItems<OrderListDto?>()
            {
                TotalCount = result.TotalCount,
                Data = result.Data.Select(s => _mapper.Map<OrderListDto>(s)).ToList()
            };
        });
    }

    public async Task<int?> Add(string userId, decimal totalCost, List<OrderListItemDto> items)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderRepository
                .AddAsync(userId, totalCost, items.Select(s => _mapper
                    .Map<OrderListItemEntity>(s))
                    .ToList());
            return result;
        });
    }

    public Task<bool?> Delete(int userId)
    {
        return ExecuteSafeAsync(() => _orderRepository.DeleteAsync(userId));
    }
}