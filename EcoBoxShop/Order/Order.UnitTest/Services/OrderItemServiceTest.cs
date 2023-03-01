using System.Threading;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services;
using Order.Host.Services.Interfaces;

namespace Order.UnitTest.Services;

public class OrderItemServiceTest
{
    private readonly IOrderItemService _orderItemService;

    private readonly Mock<IOrderItemRepository> _orderItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<OrderItemService>> _logger;
    private readonly Mock<IMapper> _mapper;

    public OrderItemServiceTest()
    {
        _orderItemRepository = new Mock<IOrderItemRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<OrderItemService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _orderItemService = new OrderItemService(_dbContextWrapper.Object, _logger.Object, _orderItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var catalogItemId = 1;
        var orderListId = 1;
        var testResult = 1;

        _orderItemRepository.Setup(s => s.AddAsync(
            It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _orderItemService.Add(catalogItemId, orderListId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        var catalogItemId = 1;
        var orderListId = 1;
        int? testResult = null!;

        _orderItemRepository.Setup(s => s.AddAsync(
            It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _orderItemService.Add(catalogItemId, orderListId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        var testResult = true;
        var testId = 1;

        _orderItemRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _orderItemService.Delete(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var testId = 1000;
        bool? testResult = null;

        _orderItemRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _orderItemService.Delete(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var orderListItemEntity = new OrderListItemEntity()
        {
            ItemId = 1,
            CatalogItemId = 1,
            OrderListId = 1
        };
        var orderListItemDto = new OrderListItemDto()
        {
            ItemId = 1,
            CatalogItemId = 1
        };
        _orderItemRepository.Setup(s => s.UpdateAsync(
            It.IsAny<OrderListItemEntity>())).ReturnsAsync(orderListItemEntity);
        _mapper.Setup(s => s.Map<OrderListItemDto>(
            It.Is<OrderListItemEntity>(i => i.Equals(orderListItemEntity)))).Returns(orderListItemDto);

        // act
        var result = await _orderItemService.Update(orderListItemEntity.ItemId, orderListItemEntity.CatalogItemId, orderListItemEntity.OrderListId);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        int itemId = 1;
        int catalogItemId = 1;
        int orderId = 1;
        _orderItemRepository.Setup(s => s.UpdateAsync(
            It.IsAny<OrderListItemEntity>())).Returns((Func<OrderListItemDto>)null!);

        // act
        var result = await _orderItemService.Update(itemId, catalogItemId, orderId);

        // assert
        result.Should().BeNull();
    }
}