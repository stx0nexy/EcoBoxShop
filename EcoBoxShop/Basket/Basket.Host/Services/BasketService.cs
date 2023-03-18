using Basket.Host.Configurations;
using Basket.Host.Models;
using Basket.Host.Models.BasketModels;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Basket.Host.Services;

public class BasketService : IBasketService
{
    private readonly ICacheService _cacheService;
    private readonly IInternalHttpClientService _httpClient;
    private readonly Config _config;
    private readonly ILogger<BasketService> _logger;

    public BasketService(
        ICacheService cacheService,
        IInternalHttpClientService httpClientService,
        IOptions<Config> options,
        ILogger<BasketService> logger)
    {
        _cacheService = cacheService;
        _httpClient = httpClientService;
        _config = options.Value;
        _logger = logger;
    }

    public async Task<UserBasket?> AddAsync(string userId, int itemId, int catalogItemId, string title, string subTitle, string pictureUrl, decimal price)
    {
        if (userId == null)
        {
            _logger.LogInformation($"Can not created basket for user {userId} ");
            return null;
        }

        var result = await _cacheService.GetAsync<UserBasket>(userId);
        if (result == null)
        {
            result = new UserBasket();
            _logger.LogInformation($"Created basket for user {userId} ");
        }

        result.BasketList.Add(new BasketItem()
            {
                ItemId = itemId,
                CatalogItemId = catalogItemId,
                Title = title,
                SubTitle = subTitle,
                PictureUrl = pictureUrl,
                Price = price
            });

        _logger.LogInformation($"Add item {itemId} to basket ");
        await _cacheService.AddOrUpdateAsync(userId, result);
        return result;
    }

    public async Task<bool?> DeleteAsync(string userId, int itemId)
    {
        var basket = await _cacheService.GetAsync<UserBasket>(userId);
        if (basket == null)
        {
            _logger.LogInformation($"Can not find Basket for user{userId}");
            return null;
        }

        var basketItem = basket.BasketList.FirstOrDefault(f => f!.ItemId == itemId);
        basket.BasketList.Remove(basketItem);
        _logger.LogInformation($"Deleted item {itemId} in basket for user {userId} ");
        await _cacheService.AddOrUpdateAsync(userId, basket);
        return true;
    }

    public async Task<UserBasket?> GetBasketAsync(string userId)
    {
        if (userId == null)
        {
            _logger.LogInformation($"Can not created basket for user {userId} ");
            return null;
        }

        var result = await _cacheService.GetAsync<UserBasket>(userId);
        if (result == null)
        {
            result = new UserBasket();
            _logger.LogInformation($"Created basket for user {userId} ");
        }

        return result;
    }

    public async Task CreateOrderAsync(string userId)
    {
        var basket = await _cacheService.GetAsync<UserBasket>(userId);
        if (basket == null)
        {
            _logger.LogInformation($"Basket null ");
            return;
        }

        await _httpClient.SendAsync<object, UserBasket>(
            $"{_config.OrderApi}/createorder",
            HttpMethod.Post,
            new UserBasket()
            {
                UserId = userId,
                BasketList = basket.BasketList
            });
        _logger.LogInformation($"Post basket to Order");
    }
}