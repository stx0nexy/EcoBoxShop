using System.Net;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogsubcategory")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogSubCategoryController : ControllerBase
{
    private readonly ILogger<CatalogSubCategoryController> _logger;
    private readonly ICatalogSubCategoryService _catalogSubCategoryService;

    public CatalogSubCategoryController(
        ILogger<CatalogSubCategoryController> logger,
        ICatalogSubCategoryService catalogSubCategoryService)
    {
        _logger = logger;
        _catalogSubCategoryService = catalogSubCategoryService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateSubCategoryRequest request)
    {
        var result = await _catalogSubCategoryService.Add(request.Title, request.CategoryId);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteRequest request)
    {
        var result = await _catalogSubCategoryService.Delete(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SubCategoryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateSubCategoryRequest request)
    {
        var result = await _catalogSubCategoryService.Update(request.Id, request.Title, request.CatalogCategoryId);
        return Ok(new SubCategoryResponse()
        {
            Id = result.Id,
            Title = result.Title,
            Category = result.CatalogCategory
        });
    }
}