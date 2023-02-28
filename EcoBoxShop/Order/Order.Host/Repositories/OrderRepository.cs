using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderRepository> _logger;

    public OrderRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<OrderRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> AddAsync(string userId, List<OrderListItemEntity> items)
    {
        var entity = await _dbContext.OrderLists.AddAsync(new OrderListEntity()
        {
            UserId = userId
        });
        _logger.LogInformation("Add Order List to DB");
        await _dbContext.OrderListItems.AddRangeAsync(items.Select(s => new OrderListItemEntity()
        {
            CatalogItemId = s.CatalogItemId,
            OrderListId = entity.Entity.OrderListId,
            OrderList = entity.Entity
        }).ToList());
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation("Add Order List Item to DB");
        return entity.Entity.OrderListId;
    }

    public async Task<bool> DeleteAsync(int orderId)
    {
        var result = await _dbContext.OrderLists.FirstAsync(c => c.OrderListId == orderId);
        _dbContext.OrderLists.Remove(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Deleted order {orderId} from DB");
        return true;
    }

    public async Task<OrderListEntity?> GetOrderListByUserId(string userId)
    {
        var result = await _dbContext.OrderLists
            .Include(i => i.OrderListItems)
            .Where(w => w.UserId == userId).FirstOrDefaultAsync();
        return result;
    }

    public async Task<OrderListEntity?> GetOrderListById(int id)
    {
        var result = await _dbContext.OrderLists
            .Include(i => i.OrderListItems)
            .Where(i => i.OrderListId == id).FirstOrDefaultAsync();
        return result;
    }
}