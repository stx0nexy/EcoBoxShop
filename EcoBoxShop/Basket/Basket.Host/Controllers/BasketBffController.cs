using System.Net;
using Basket.Host.Models.BasketModels;
using Basket.Host.Models.Requests;
using Basket.Host.Models.Responses;
using Basket.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Host.Controllers;

[ApiController]
[Route(ComponentDefaults.DefaultRoute)]
[Scope("basket.api")]
public class BasketBffController : ControllerBase
{
    private readonly IBasketService _service;
    private readonly ILogger<BasketBffController> _logger;

    public BasketBffController(
        IBasketService service,
        ILogger<BasketBffController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddToBasket(AddRequest request)
    {
        await _service.AddAsync(request.UserId, request.ItemId, request.CatalogItemId);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteItemFromBasket(RemoveRequest request)
    {
        await _service.DeleteAsync(request.UserId, request.ItemId);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(BasketResponse<BasketItem>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBasket(GetRequest request)
    {
        var result = await _service.GetBasketAsync(request.UserId);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateOrder(GetRequest request)
    {
        await _service.CreateOrderAsync(request.UserId);
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetCatalogItem(CatalogItemByIdRequest request)
    {
        var result = await _service.GetItemAsync(request.Id);
        return Ok(result);
    }
}