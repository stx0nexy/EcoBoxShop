using Basket.Host.Configurations;
using Basket.Host.Models.BasketModels;
using Basket.Host.Services;
using Basket.Host.Services.Interfaces;
using FluentAssertions;
using Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Basket.UnitTest.Services;

public class BasketServiceTest
{
    private readonly IBasketService _basketService;
    private readonly Mock<ICacheService> _cacheService;
    private readonly Mock<IInternalHttpClientService> _httpClient;
    private readonly Config _config;
    private readonly Mock<ILogger<BasketService>> _logger;

    public BasketServiceTest()
    {
        _cacheService = new Mock<ICacheService>();
        _httpClient = new Mock<IInternalHttpClientService>();
        _config = new Config { OrderApi = "https://example.com" };
        _logger = new Mock<ILogger<BasketService>>();

        _basketService = new BasketService( _cacheService.Object,  _httpClient.Object,  Options.Create(_config),  _logger.Object);
    }
    
    [Fact]
    public async Task AddAsync_Success()
    {
        // Arrange
        string userId = "user1";
        int itemId = 1;
        int catalogItemId = 1;
        string title = " ";
        string subTitle = " ";
        string pictureUrl = " ";
        decimal price = 1;
            var userBasket = new UserBasket()
        {
            UserId = userId,
            BasketList = new List<BasketItem?>
            {
                new BasketItem()
                {
                    ItemId = itemId,
                    CatalogItemId = catalogItemId
                }
            }
        };
        _cacheService
            .Setup(x => x.GetAsync<UserBasket>(userId))
            .ReturnsAsync(userBasket);

        // Act
        var result = await _basketService.AddAsync(userId, itemId, catalogItemId, title, subTitle, pictureUrl, price);

        // Assert
        result.Should().Be(userBasket);
    }
    
    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        string userId = null!;
        int itemId = 1;
        int catalogItemId = 1;
        string title = " ";
        string subTitle = " ";
        string pictureUrl = " ";
        decimal price = 1;
        UserBasket userBasket = null!;

        _cacheService
            .Setup(x => x.GetAsync<UserBasket>(userId))
            .ReturnsAsync(userBasket);

        // act
        var result = await _basketService.AddAsync( userId, itemId, catalogItemId, title, subTitle, pictureUrl, price);

        // assert
        _logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString() !
                    .Contains($"Can not created basket for user {userId} ")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
            Times.Once);
    }
    
    [Fact]
    public async Task DeleteAsync_Success()
    {
        // Arrange
        bool testResult = true;
        string userId = "user1";
        int itemId = 1;
        int catalogItemId = 1;
        var userBasket = new UserBasket()
        {
            UserId = userId,
            BasketList = new List<BasketItem?>
            {
                new BasketItem()
                {
                    ItemId = itemId,
                    CatalogItemId = catalogItemId
                }
            }
        };
        _cacheService
            .Setup(x => x.GetAsync<UserBasket>(userId))
            .ReturnsAsync(userBasket);

        // Act
        var result = await _basketService.DeleteAsync(userId, itemId);

        // Assert
        result.Should().Be(testResult);
    }
    
    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        string userId = "user1";
        int itemId = 1000;
        UserBasket userBasket = null!;

        _cacheService
            .Setup(x => x.GetAsync<UserBasket>( It.IsAny<string>()))
            .ReturnsAsync(userBasket);

        // act
        var result = await _basketService.DeleteAsync(userId, itemId);

        // assert
        _logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString() !
                    .Contains($"Can not find Basket for user{userId}")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
            Times.Once);
    }

    [Fact]
    public async Task GetBasketAsync_Success()
    {
        // Arrange
        string userId = "user1";
        int itemId = 1;
        int catalogItemId = 1;
        var userBasket = new UserBasket()
        {
            UserId = userId,
            BasketList = new List<BasketItem?>
            {
                new BasketItem()
                {
                    ItemId = itemId,
                    CatalogItemId = catalogItemId
                }
            }
        };
        _cacheService
            .Setup(x => x.GetAsync<UserBasket>(userId))
            .ReturnsAsync(userBasket);

        // Act
        var result = await _basketService.GetBasketAsync(userId);

        // Assert
        result.Should().Be(userBasket);
    }

    [Fact]
    public async Task GetBasketAsync_Failed()
    {
        // Arrange
        string userId = null!;
        UserBasket userBasket = null!;
        _cacheService
            .Setup(x => x.GetAsync<UserBasket>(userId))
            .ReturnsAsync(userBasket);

        // Act
        await _basketService.GetBasketAsync(userId);

        // Assert
        _logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString() !
                    .Contains($"Can not created basket for user {userId} ")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
            Times.Once);
    }
    
    [Fact]
    public async Task CreateOrderAsync_Success()
    {
        // Arrange
        string userId = "user1";
        int itemId = 1;
        int totalCost = 100;
        int catalogItemId = 1;
        var userBasket = new UserBasket()
        {
            UserId = userId,
            TotalCost = totalCost,
            BasketList = new List<BasketItem?>
            {
                new BasketItem()
                {
                    ItemId = itemId,
                    CatalogItemId = catalogItemId
                }
            }
        };
        _cacheService
            .Setup(x => x.GetAsync<UserBasket>(userId))
            .ReturnsAsync(userBasket);

        // Act
        await _basketService.CreateOrderAsync(userId);

        // Assert
        _logger.Verify(
            v => v.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString()!.Contains("Post basket to Order")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!), Times.Once);
    }

    [Fact]
    public async Task CreateOrderAsync_Failed()
    {
        // Arrange
        string userId = "user1";
        UserBasket userBasket = null!;
        _cacheService
            .Setup(x => x.GetAsync<UserBasket>(userId))
            .ReturnsAsync(userBasket);

        // Act
        await _basketService.CreateOrderAsync(userId);
        
        // Assert
        _logger.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString() !
                    .Contains($"Basket null ")),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>() !),
            Times.Once);
    }
}