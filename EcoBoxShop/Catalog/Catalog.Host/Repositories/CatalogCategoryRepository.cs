using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Host.Repositories;

public class CatalogCategoryRepository : ICatalogCategoryRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogCategoryRepository> _logger;

    public CatalogCategoryRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogCategoryRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> AddAsync(string title)
    {
        var item = await _dbContext.AddAsync(new CatalogCategoryEntity()
        {
            Title = title
        });

        await _dbContext.SaveChangesAsync();

        return item.Entity.Id;
    }

    public async Task<bool?> DeleteAsync(int id)
    {
        CatalogCategoryEntity result = await _dbContext.CatalogCategories.FirstAsync(c => c.Id == id);
        _dbContext.CatalogCategories.Remove(result);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<CatalogCategoryEntity?> UpdateAsync(CatalogCategoryEntity catalogType)
    {
        _dbContext.Entry(catalogType).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return catalogType;
    }

    public async Task<CatalogCategoryEntity?> GetByIdAsync(int id)
    {
        var result = await _dbContext.CatalogCategories.Where(i => i.Id == id).FirstOrDefaultAsync();
        return result;
    }
}