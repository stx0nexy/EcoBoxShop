using System.Net;
using Catalog.Host.Configurations;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBffController : ControllerBase
{
    private readonly ILogger<CatalogBffController> _logger;
    private readonly ICatalogService _catalogService;
    private readonly IOptions<CatalogConfig> _config;

    public CatalogBffController(
        ILogger<CatalogBffController> logger,
        ICatalogService catalogService,
        IOptions<CatalogConfig> config)
    {
        _logger = logger;
        _catalogService = catalogService;
        _config = config;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ItemsResponse<CatalogCategoryDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Categories()
    {
        var result = await _catalogService.GetCatalogCategories();
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ItemsResponse<CatalogSubCategoryDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SubCategories()
    {
        var result = await _catalogService.GetCatalogSubCategories();
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ItemsResponse<CatalogBrandDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Brands()
    {
        var result = await _catalogService.GetCatalogBrands();
        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogItemFilter> request)
    {
        var result = await _catalogService.GetCatalogItems(request.PageSize, request.PageIndex, request.Filters);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemByIdResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Item(ItemByIdRequest request)
    {
        var result = await _catalogService.GetCatalogItemById(request.Id);
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

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsByBrand(PaginatedItemsByBrandRequest request)
    {
        var result = await _catalogService.GetCatalogItemByBrand(request.Brand, request.PageSize, request.PageIndex);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> ItemsBySubCategory(PaginatedItemsBySubCategoryRequest request)
    {
        var result = await _catalogService.GetCatalogItemBySubCategory(request.SubCategory, request.PageSize, request.PageIndex);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ItemsResponse<CatalogSubCategoryDto>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SubCategoriesByCategory(SubCategoriesByCategoryRequest request)
    {
        var result = await _catalogService.GetSubCategoriesByCategory(request.Category);
        return Ok(result);
    }
}