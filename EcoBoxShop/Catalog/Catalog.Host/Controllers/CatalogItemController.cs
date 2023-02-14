using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogitem")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogItemController : ControllerBase
{
    private readonly ILogger<CatalogItemController> _logger;
    private readonly ICatalogItemService _catalogItemService;

    public CatalogItemController(
        ILogger<CatalogItemController> logger,
        ICatalogItemService catalogItemService)
    {
        _logger = logger;
        _catalogItemService = catalogItemService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateItemRequest request)
    {
        var result = await _catalogItemService.Add(request.Title, request.SubTitle, request.Description, request.PictureFileName, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogSubCategoryId);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteRequest request)
    {
        var result = await _catalogItemService.Delete(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateItemRequest request)
    {
        var result = await _catalogItemService.Update(request.Id, request.Title, request.SubTitle, request.Description, request.PictureFileName, request.Price, request.AvailableStock, request.CatalogBrandId, request.CatalogSubCategoryId);
        return Ok(new ItemByIdResponse()
        {
            Id = result.Id,
            Title = result.Title,
            SubTitle = result.SubTitle,
            Description = result.Description,
            PictureFileName = result.PictureUrl,
            Price = result.Price,
            AvailableStock = result.AvailableStock,
            CatalogBrand = result.CatalogBrand,
            CatalogSubCategory = result.CatalogSubCategory
        });
    }
}