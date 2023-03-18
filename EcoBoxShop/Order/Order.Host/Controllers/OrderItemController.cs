using System.Net;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Order.Host.Configurations;
using Order.Host.Models.Requests;
using Order.Host.Models.Response;
using Order.Host.Services.Interfaces;

namespace Order.Host.Controllers;

[ApiController]
[Scope("order.oprderitem")]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderItemController : ControllerBase
{
    private readonly ILogger<OrderBffController> _logger;
    private readonly IOrderItemService _orderItemService;
    private readonly IOptions<OrderConfig> _config;

    public OrderItemController(
        ILogger<OrderBffController> logger,
        IOrderItemService orderItemService,
        IOptions<OrderConfig> config)
    {
        _logger = logger;
        _orderItemService = orderItemService;
        _config = config;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddOrderItem(AddOrderListItemRequest request)
    {
        var result = await _orderItemService.Add(request.CatalogItem, request.OrderListId, request.Title, request.SubTitle, request.PictureUrl, request.Price);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteOrderItem(CatalogItemByIdRequest request)
    {
        var result = await _orderItemService.Delete(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderListItemResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateOrderItem(UpdateOrderListItemRequest request)
    {
        var result = await _orderItemService.Update(request.ItemId, request.CatalogItemId, request.OrderListId, request.Title, request.SubTitle, request.PictureUrl, request.Price);
        return Ok(result);
    }
}