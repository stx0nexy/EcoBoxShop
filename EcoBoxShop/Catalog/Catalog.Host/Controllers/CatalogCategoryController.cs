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
[Scope("catalog.catalogcategory")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogCategoryController : ControllerBase
{
    private readonly ILogger<CatalogCategoryController> _logger;
    private readonly ICatalogCategoryService _catalogCategoryService;

    public CatalogCategoryController(
        ILogger<CatalogCategoryController> logger,
        ICatalogCategoryService catalogCategoryService)
    {
        _logger = logger;
        _catalogCategoryService = catalogCategoryService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(CreateFilterRequest request)
    {
        var result = await _catalogCategoryService.Add(request.Title);
        return Ok(new AddItemResponse<int?>() { Id = result });
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Delete(DeleteRequest request)
    {
        var result = await _catalogCategoryService.Delete(request.Id);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(FilterResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateFilterRequest request)
    {
        var result = await _catalogCategoryService.Update(request.Id, request.Title);
        return Ok(new FilterResponse()
        {
            Id = result.Id,
            Title = result.Title
        });
    }
}