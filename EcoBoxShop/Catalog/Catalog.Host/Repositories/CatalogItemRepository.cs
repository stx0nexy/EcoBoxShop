using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItemEntity>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? categoryFilter)
    {
        IQueryable<CatalogItemEntity> query = _dbContext.CatalogItems;

        if (brandFilter.HasValue)
        {
            query = query.Where(w => w.CatalogBrandId == brandFilter.Value);
        }

        if (categoryFilter.HasValue)
        {
            query = query.Where(w => w.CatalogSubCategoryId == categoryFilter.Value);
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogSubCategory)
            .Include(i => i.CatalogSubCategory!.CatalogCategory)
            .OrderBy(c => c.Title)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItemEntity>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<int?> AddAsync(string title, string subTitle, string description, string pictureFileName, decimal price, int availableStock, int catalogBrandId, int catalogSubCategoryId)
    {
        var item = await _dbContext.AddAsync(new CatalogItemEntity()
        {
            Title = title,
            SubTitle = subTitle,
            Description = description,
            PictureFileName = pictureFileName,
            Price = price,
            AvailableStock = availableStock,
            CatalogBrandId = catalogBrandId,
            CatalogSubCategoryId = catalogSubCategoryId
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<CatalogItemEntity?> GetByIdAsync(int id)
    {
        var result = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogSubCategory)
            .Include(i => i.CatalogSubCategory!.CatalogCategory)
            .Where(i => i.Id == id).FirstOrDefaultAsync();
        return result;
    }

    public async Task<PaginatedItems<CatalogItemEntity>> GetByBrandAsync(string brand, int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .Where(w => w.CatalogBrand!.Title == brand)
            .CountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogSubCategory)
            .Include(i => i.CatalogSubCategory!.CatalogCategory)
            .Where(w => w.CatalogBrand!.Title == brand)
            .OrderBy(c => c.Title)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItemEntity>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<PaginatedItems<CatalogItemEntity>> GetBySubCategoryAsync(string category, int pageIndex, int pageSize)
    {
        var totalItems = await _dbContext.CatalogItems
            .Where(w => w.CatalogSubCategory!.Title == category)
            .CountAsync();

        var itemsOnPage = await _dbContext.CatalogItems
            .Include(i => i.CatalogBrand)
            .Include(i => i.CatalogSubCategory)
            .Include(i => i.CatalogSubCategory!.CatalogCategory)
            .Where(w => w.CatalogSubCategory!.Title == category)
            .OrderBy(c => c.Title)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();
        return new PaginatedItems<CatalogItemEntity>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<PaginatedItems<CatalogBrandEntity>> GetBrandsAsync()
    {
        var totalItems = await _dbContext.CatalogBrands
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogBrands
            .OrderBy(c => c.Title)
            .ToListAsync();

        return new PaginatedItems<CatalogBrandEntity>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<PaginatedItems<CatalogCategoryEntity>> GetCategoriesAsync()
    {
        var totalItems = await _dbContext.CatalogCategories
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogCategories
            .OrderBy(c => c.Title)
            .ToListAsync();

        return new PaginatedItems<CatalogCategoryEntity>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<PaginatedItems<CatalogSubCategoryEntity>> GetSubCategoriesAsync()
    {
        var totalItems = await _dbContext.CatalogSubCategories
            .LongCountAsync();

        var itemsOnPage = await _dbContext.CatalogSubCategories
            .Include(i => i.CatalogCategory)
            .OrderBy(c => c.Title)
            .ToListAsync();

        return new PaginatedItems<CatalogSubCategoryEntity>() { TotalCount = totalItems, Data = itemsOnPage };
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        bool? res = true;
        CatalogItemEntity result = await _dbContext.CatalogItems.FirstAsync(c => c.Id == id);
        _dbContext.CatalogItems.Remove(result);
        await _dbContext.SaveChangesAsync();
        return res;
    }

    public async Task<CatalogItemEntity?> UpdateAsync(CatalogItemEntity catalogItem)
    {
        _dbContext.Entry(catalogItem).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return catalogItem;
    }

    public async Task<PaginatedItems<CatalogSubCategoryEntity>> GetSubCategoryByCategoryAsync(string category)
    {
        var totalItems = await _dbContext.CatalogSubCategories
            .Where(w => w.CatalogCategory!.Title == category)
            .CountAsync();

        var itemsOnPage = _dbContext.CatalogSubCategories
            .Include(i => i.CatalogCategory)
            .Where(w => w.CatalogCategory!.Title == category)
            .OrderBy(c => c.Title);

        return new PaginatedItems<CatalogSubCategoryEntity>() { TotalCount = totalItems, Data = itemsOnPage };
    }
}