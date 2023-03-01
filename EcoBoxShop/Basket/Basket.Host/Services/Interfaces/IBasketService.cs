using Basket.Host.Models;
using Basket.Host.Models.BasketModels;
using Basket.Host.Models.Responses;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
     Task<UserBasket?> AddAsync(string userId, int itemId, int catalogItemId);
     Task<bool?> DeleteAsync(string userId, int itemId);
     Task<UserBasket?> GetBasketAsync(string userId);
     Task CreateOrderAsync(string userId);
}