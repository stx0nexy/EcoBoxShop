using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogCategoryService : BaseDataService<ApplicationDbContext>, ICatalogCategoryService
{
    private readonly ICatalogCategoryRepository _catalogCategoryRepository;
    private readonly IMapper _mapper;

    public CatalogCategoryService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogCategoryRepository catalogCategoryRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogCategoryRepository = catalogCategoryRepository;
        _mapper = mapper;
    }

    public Task<int?> Add(string category)
    {
        return ExecuteSafeAsync(() => _catalogCategoryRepository.AddAsync(category));
    }

    public Task<bool?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogCategoryRepository.DeleteAsync(id));
    }

    public Task<CatalogCategoryDto> Update(int id, string title)
    {
        return ExecuteSafeAsync(async () =>
        {
            var result = await _catalogCategoryRepository.UpdateAsync(new CatalogCategoryEntity() { Id = id, Title = title });
            return _mapper.Map<CatalogCategoryDto>(result);
        });
    }
}