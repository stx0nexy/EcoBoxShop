using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly ICatalogSubCategoryRepository _catalogSubCategoryRepository;
    private readonly IMapper _mapper;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        ICatalogBrandRepository catalogBrandRepository,
        ICatalogSubCategoryRepository catalogSubCategoryRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
        _catalogBrandRepository = catalogBrandRepository;
        _catalogSubCategoryRepository = catalogSubCategoryRepository;
        _mapper = mapper;
    }

    public Task<int?> Add(string title, string subTitle, string description, string pictureFileName, decimal price, int availableStock, int catalogBrandId, int catalogSubCategoryId)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.AddAsync(title, subTitle, description, pictureFileName, price, availableStock, catalogBrandId, catalogSubCategoryId));
    }

    public Task<bool?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.DeleteAsync(id));
    }

    public Task<CatalogItemDto> Update(int id, string title, string subTitle, string description, string pictureFileName, decimal price, int availableStock, int catalogBrandId, int catalogSubCategoryId)
    {
        return ExecuteSafeAsync(async () =>
        {
            var catalogBrand = await _catalogBrandRepository.GetByIdAsync(catalogBrandId);
            var catalogSubCategory = await _catalogSubCategoryRepository.GetByIdAsync(catalogSubCategoryId);
            var result = await _catalogItemRepository.UpdateAsync(new CatalogItemEntity()
            {
                Id = id,
                Title = title,
                SubTitle = subTitle,
                Description = description,
                PictureFileName = pictureFileName,
                Price = price,
                AvailableStock = availableStock,
                CatalogBrand = catalogBrand,
                CatalogSubCategory = catalogSubCategory
            });
            return _mapper.Map<CatalogItemDto>(result);
        });
    }
}