using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogSubCategoryRepository : ICatalogSubCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogSubCategoryRepository> _logger;

    public CatalogSubCategoryRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogSubCategoryRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> AddAsync(string title, int catalogCategoryId)
    {
        var item = await _dbContext.AddAsync(new CatalogSubCategoryEntity()
        {
            Title = title,
            CatalogCategoryId = catalogCategoryId
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        CatalogSubCategoryEntity result = await _dbContext.CatalogSubCategories.FirstAsync(c => c.Id == id);
        _dbContext.CatalogSubCategories.Remove(result);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<CatalogSubCategoryEntity?> UpdateAsync(CatalogSubCategoryEntity catalogSubCategory)
    {
        _dbContext.Entry(catalogSubCategory).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return catalogSubCategory;
    }

    public async Task<CatalogSubCategoryEntity?> GetByIdAsync(int id)
    {
        var result = await _dbContext.CatalogSubCategories.Where(i => i.Id == id).FirstOrDefaultAsync();
        return result;
    }
}