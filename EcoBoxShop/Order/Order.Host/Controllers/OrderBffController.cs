using System.Net;
using IdentityServer4.Extensions;
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
[Scope("order.orderbff.api")]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class OrderBffController : ControllerBase
{
    private readonly ILogger<OrderBffController> _logger;
    private readonly IOrderService _orderService;
    private readonly IOptions<OrderConfig> _config;

    public OrderBffController(
        ILogger<OrderBffController> logger,
        IOrderService orderService,
        IOptions<OrderConfig> config)
    {
        _logger = logger;
        _orderService = orderService;
        _config = config;
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderListResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrder(OrderListByUserIdRequest request)
    {
        var result = await _orderService.GetOrderListByUserIdAsync(request.UserId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateOrder(AddOrderListRequest request)
    {
        var result = await _orderService.Add(request.UserId, request.BasketList);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(CatalogItemByIdRequest request)
    {
        var result = await _orderService.Delete(request.Id);
        return Ok(result);
    }
}