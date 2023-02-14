using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogSubCategoryService : BaseDataService<ApplicationDbContext>, ICatalogSubCategoryService
{
    private readonly ICatalogSubCategoryRepository _catalogSubCategoryRepository;
    private readonly ICatalogCategoryRepository _catalogCategoryRepository;
    private readonly IMapper _mapper;

    public CatalogSubCategoryService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogSubCategoryRepository catalogSubCategoryRepository,
        ICatalogCategoryRepository catalogCategoryRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogSubCategoryRepository = catalogSubCategoryRepository;
        _catalogCategoryRepository = catalogCategoryRepository;
        _mapper = mapper;
    }

    public Task<int?> Add(string category, int catalogCategoryId)
    {
        return ExecuteSafeAsync(() => _catalogSubCategoryRepository.AddAsync(category, catalogCategoryId));
    }

    public Task<bool?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogSubCategoryRepository.DeleteAsync(id));
    }

    public Task<CatalogSubCategoryDto> Update(int id, string title, int catalogCategoryId)
    {
        return ExecuteSafeAsync(async () =>
        {
            var catalogCategory = await _catalogCategoryRepository.GetByIdAsync(catalogCategoryId);
            var result = await _catalogSubCategoryRepository.UpdateAsync(new CatalogSubCategoryEntity() { Id = id, Title = title, CatalogCategory = catalogCategory });
            return _mapper.Map<CatalogSubCategoryDto>(result);
        });
    }
}