using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly IMapper _mapper;

    public CatalogBrandService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogBrandRepository catalogBrandRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
        _mapper = mapper;
    }

    public Task<int?> Add(string title)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.AddAsync(title));
    }

    public Task<bool?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogBrandRepository.DeleteAsync(id));
    }

    public Task<CatalogBrandDto> Update(int id, string title)
    {
        return ExecuteSafeAsync(async () =>
        {
            var result = await _catalogBrandRepository.UpdateAsync(new CatalogBrandEntity() { Id = id, Title = title });
            return _mapper.Map<CatalogBrandDto>(result);
        });
    }
}