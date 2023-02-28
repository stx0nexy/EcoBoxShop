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

    public async Task<OrderListDto?> GetOrderListByUserIdAsync(string userId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderRepository.GetOrderListByUserId(userId);
            return _mapper.Map<OrderListDto>(result);
        });
    }

    public async Task<int?> Add(string userId, List<OrderListItemDto> items)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderRepository
                .AddAsync(userId, items.Select(s => _mapper
                    .Map<OrderListItemEntity>(s))
                    .ToList());
            return result;
        });
    }

    public Task<bool> Delete(int userId)
    {
        return ExecuteSafeAsync(() => _orderRepository.DeleteAsync(userId));
    }
}