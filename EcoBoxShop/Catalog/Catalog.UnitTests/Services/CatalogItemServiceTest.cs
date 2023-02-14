using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services;

public class CatalogItemServiceTest
{
    private readonly ICatalogItemService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<ICatalogBrandRepository> _catalogBrandRepository;
    private readonly Mock<ICatalogSubCategoryRepository> _catalogSubCategoryRepository;
    private readonly Mock<IMapper> _mapper;

    private readonly CatalogItemEntity _testItem = new CatalogItemEntity()
    {
        Id = 1,
        Title = "Name",
        SubTitle = "SubTitle",
        Description = "Description",
        Price = 1000,
        AvailableStock = 100,
        CatalogBrandId = 1,
        CatalogSubCategoryId = 1,
        PictureFileName = "1.png"
    };

    public CatalogItemServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _catalogBrandRepository = new Mock<ICatalogBrandRepository>();
        _catalogSubCategoryRepository = new Mock<ICatalogSubCategoryRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogItemService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _catalogBrandRepository.Object, _catalogSubCategoryRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;

        _catalogItemRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Title, _testItem.SubTitle, _testItem.Description, _testItem.PictureFileName, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogSubCategoryId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;

        _catalogItemRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<int>(),
            It.IsAny<int>(),
            It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Add(_testItem.Title, _testItem.SubTitle, _testItem.Description, _testItem.PictureFileName, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogSubCategoryId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        var testResult = true;

        _catalogItemRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == _testItem.Id))).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Delete(_testItem.Id);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var testId = 1000;
        bool? testResult = null;

        _catalogItemRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _catalogService.Delete(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var testBrandId = 1;
        var testTypeId = 1;
        var catalogBrandSuccess = new CatalogBrandEntity()
        {
            Id = 1,
            Title = "TestBrand"
        };
        var catalogSubCategorySuccess = new CatalogSubCategoryEntity()
        {
            Id = 1,
            Title = "TestType",
            CatalogCategoryId = 1
        };
        var catalogBrandDtoSuccess = new CatalogBrandDto()
        {
            Id = 1,
            Title = "TestBrand"
        };
        var catalogSubCategoryDtoSuccess = new CatalogSubCategoryDto()
        {
            Id = 1,
            Title = "TestType"
        };
        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Id = 1,
            Title = "Name",
            SubTitle = "SubTitle",
            Description = "Description",
            Price = 1000,
            AvailableStock = 100,
            CatalogBrand = catalogBrandDtoSuccess,
            CatalogSubCategory = catalogSubCategoryDtoSuccess,
            PictureUrl = "www.alevelwebsite.com/assets/img/1"
        };
        _catalogBrandRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i == testBrandId))).ReturnsAsync(catalogBrandSuccess);
        _catalogSubCategoryRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i == testTypeId))).ReturnsAsync(catalogSubCategorySuccess);
        _catalogItemRepository.Setup(s => s.UpdateAsync(
            It.IsAny<CatalogItemEntity>())).ReturnsAsync(_testItem);
        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItemEntity>(i => i.Equals(_testItem)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.Update(_testItem.Id, _testItem.Title, _testItem.SubTitle, _testItem.Description, _testItem.PictureFileName, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogSubCategoryId);

        // assert
        result.Should().NotBeNull();
        result.CatalogBrand.Should().NotBeNull();
        result.CatalogSubCategory.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.UpdateAsync(
            It.IsAny<CatalogItemEntity>())).Returns((Func<CatalogItemDto>)null!);

        // act
        var result = await _catalogService.Update(_testItem.Id, _testItem.Title, _testItem.SubTitle, _testItem.Description, _testItem.PictureFileName, _testItem.Price, _testItem.AvailableStock, _testItem.CatalogBrandId, _testItem.CatalogSubCategoryId);

        // assert
        result.Should().BeNull();
    }
}