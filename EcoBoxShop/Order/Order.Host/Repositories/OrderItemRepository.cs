using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Repositories.Interfaces;

namespace Order.Host.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderRepository> _logger;

    public OrderItemRepository(
        IDbContextWrapper<ApplicationDbContext> wrapper,
        ILogger<OrderRepository> logger)
    {
        _dbContext = wrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> AddAsync(int catalogItemId)
    {
        var entity = await _dbContext.OrderListItems.AddAsync(new OrderListItemEntity()
        {
            CatalogItemId = catalogItemId
        });
        _logger.LogInformation("Add Order List Item to DB");

        await _dbContext.SaveChangesAsync();
        return entity?.Entity.ItemId;
    }

    public async Task<bool> DeleteAsync(int itemId)
    {
        var result = await _dbContext.OrderListItems.FirstAsync(c => c.ItemId == itemId);
        _dbContext.OrderListItems.Remove(result);
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Deleted item {itemId} from DB");
        return true;
    }

    public async Task<OrderListItemEntity> UpdateAsync(OrderListItemEntity orderListItem)
    {
        _dbContext.Entry(orderListItem).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Updated item {orderListItem.ItemId}");
        return orderListItem;
    }

    public async Task<OrderListItemEntity?> GetByIdAsync(int id)
    {
        var result = await _dbContext.OrderListItems.Where(i => i.ItemId == id).FirstOrDefaultAsync();
        return result;
    }

    public async Task<int> GetCatalogItemIdByItemId(int itemId)
    {
        var result = await _dbContext.OrderListItems
            .Where(w => w.ItemId == itemId).FirstOrDefaultAsync();
        return result!.CatalogItemId;
    }
}