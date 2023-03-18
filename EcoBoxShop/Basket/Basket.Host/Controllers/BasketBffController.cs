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
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
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
    [AllowAnonymous]
    [ProducesResponseType(typeof(BasketResponse<BasketItem>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> AddToBasket(AddRequest request)
    {
        var result = await _service.AddAsync(request.UserId, request.ItemId, request.CatalogItemId, request.Title, request.SubTitle, request.PictureUrl, request.Price);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteItemFromBasket(RemoveRequest request)
    {
        var result = await _service.DeleteAsync(request.UserId, request.ItemId);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(BasketResponse<BasketItem>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBasket(GetRequest request)
    {
        var result = await _service.GetBasketAsync(request.UserId);
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateOrder(GetRequest request)
    {
        await _service.CreateOrderAsync(request.UserId);
        return Ok();
    }
}