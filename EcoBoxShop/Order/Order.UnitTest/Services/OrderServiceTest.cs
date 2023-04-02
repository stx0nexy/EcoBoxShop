using System.Collections.Generic;
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

public class OrderServiceTest
{
    private readonly IOrderService _orderService;

    private readonly Mock<IOrderRepository> _orderRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<OrderService>> _logger;
    private readonly Mock<IMapper> _mapper;

    public OrderServiceTest()
    {
        _orderRepository = new Mock<IOrderRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<OrderService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _orderService = new OrderService(_dbContextWrapper.Object, _logger.Object, _orderRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        var testResult = true;
        var testId = 1;

        _orderRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _orderService.Delete(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var testId = 1000;
        bool? testResult = null;

        _orderRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _orderService.Delete(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var id = "test";
        var totalCost = 100;
        var itemDto = new OrderListItemDto()
        {
            ItemId = 1,
            CatalogItemId = 1
        };
        var item = new OrderListItemEntity()
        {
            ItemId = 1,
            CatalogItemId = 1
        };
        var items = new List<OrderListItemDto>
        {
            itemDto
        };
        var itemsEntities = new List<OrderListItemEntity>
        {
            item
        };
        int? testResult = 1;

        _orderRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(), totalCost, itemsEntities)).ReturnsAsync(testResult);
        _mapper.Setup(s => s
            .Map<OrderListItemEntity>(It.Is<OrderListItemDto>(i => i
                .Equals(itemDto)))).Returns(item);

        // act
        var result = await _orderService.Add(id, totalCost, items);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        var id = "test";
        var totalCost = 100;
        List<OrderListItemDto> items = new List<OrderListItemDto>()
        {
            new OrderListItemDto()
            {
                ItemId = 1,
                CatalogItemId = 1
            }
        };
        OrderListItemEntity item = new OrderListItemEntity()
        {
            ItemId = 1,
            CatalogItemId = 1,
            OrderListId = 1
        };
        List<OrderListItemEntity> itemsEntities = new List<OrderListItemEntity>()
        {
            new OrderListItemEntity()
            {
                ItemId = 1,
                CatalogItemId = 1,
                OrderListId = 1
            }
        };
        int? testResult = null!;

        _mapper.Setup(s => s.Map<OrderListItemEntity>(
            It.Is<OrderListItemEntity>(i => i.Equals(items)))).Returns(item);
        _orderRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(), totalCost, itemsEntities)).ReturnsAsync(testResult);

        // act
        var result = await _orderService.Add(id, totalCost, items);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task GetOrderListByUserIdAsync_Success()
    {
        // arrange
        var id = "test";
        var paginatedOrderListEntity = new PaginatedItems<OrderListEntity?>()
        {
            TotalCount = 1,
            Data = new List<OrderListEntity>()
            {
                new OrderListEntity()
                {
                    OrderListId = 1,
                    UserId = id
                }
            }
        };
        var paginatedOrderListDto = new PaginatedItems<OrderListDto>()
        {
            TotalCount = 1,
            Data = new List<OrderListDto>()
            {
                new OrderListDto()
                {
                    OrderListId = 1,
                    UserId = id
                }
            }
        };
        var orderListEntity = new OrderListEntity()
        {
          OrderListId = 1,
          UserId = id
        };
        var orderListDto = new OrderListDto()
        {
            OrderListId = 1,
            UserId = id
        };

        _orderRepository.Setup(s => s.GetOrderListByUserId(
            id)).ReturnsAsync(paginatedOrderListEntity);
        _mapper.Setup(s => s.Map<OrderListDto>(
            It.Is<OrderListEntity>(i => i.Equals(orderListEntity)))).Returns(orderListDto);

        // act
        var result = await _orderService.GetOrderListByUserIdAsync(id);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOrderListByUserIdAsync_Failed()
    {
        // arrange
        var id = "test";
        _orderRepository.Setup(s => s.GetOrderListByUserId(
            It.IsAny<string>())).ReturnsAsync((Func<PaginatedItems<OrderListEntity?>>)null!);

        // act
        var result = await _orderService.GetOrderListByUserIdAsync(id);

        // assert
        result.Should().BeNull();
    }
}
