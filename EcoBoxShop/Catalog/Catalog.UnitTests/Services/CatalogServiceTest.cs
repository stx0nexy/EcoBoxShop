using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Response;
using Catalog.Host.Models.Responses;

namespace Catalog.UnitTests.Services;

public class CatalogServiceTest
{
    private readonly ICatalogService _catalogService;

    private readonly Mock<ICatalogItemRepository> _catalogItemRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;

    public CatalogServiceTest()
    {
        _catalogItemRepository = new Mock<ICatalogItemRepository>();
        _mapper = new Mock<IMapper>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogService = new CatalogService(_dbContextWrapper.Object, _logger.Object, _catalogItemRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;

        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItemEntity>()
        {
            Data = new List<CatalogItemEntity>()
            {
                new CatalogItemEntity()
                {
                    Title = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogItemSuccess = new CatalogItemEntity()
        {
            Title = "TestName"
        };

        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Title = "TestName"
        };

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).ReturnsAsync(pagingPaginatedItemsSuccess);

        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItemEntity>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItems(testPageSize, testPageIndex, null);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemsAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;

        _catalogItemRepository.Setup(s => s.GetByPageAsync(
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize),
            It.IsAny<int?>(),
            It.IsAny<int?>())).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItems(testPageSize, testPageIndex, null);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Success()
    {
        // arrange
        var catalogItemSuccess = new CatalogItemEntity()
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
        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Id = 1,
            Title = "Name",
            SubTitle = "SubTitle",
            Description = "Description",
            Price = 1000,
            AvailableStock = 100,
            CatalogBrand = new CatalogBrandDto()
            {
                Id = 1,
                Title = "TestBrand"
            },
            CatalogSubCategory = new CatalogSubCategoryDto()
            {
                Id = 1,
                Title = "TestType"
            },
            PictureUrl = "www.alevelwebsite.com/assets/img/1"
        };
        _catalogItemRepository.Setup(s => s.GetByIdAsync(
            It.Is<int>(i => i == catalogItemSuccess.Id))).ReturnsAsync(catalogItemSuccess);
        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItemEntity>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemById(catalogItemSuccess.Id);

        // assert
        result.Should().NotBeNull();
        result.CatalogBrand.Should().NotBeNull();
        result.CatalogSubCategory.Should().NotBeNull();
    }

    [Fact]
    public async Task GetCatalogItemByIdAsync_Failed()
    {
        // arrange
        var testId = 1000;
        _catalogItemRepository.Setup(s => s.GetByIdAsync(
            It.IsAny<int>())).Returns((Func<CatalogItemDto>)null!);

        // act
        var result = await _catalogService.GetCatalogItemById(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemByBrandAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;
        var testBrand = "test";
        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItemEntity>()
        {
            Data = new List<CatalogItemEntity>()
            {
                new CatalogItemEntity()
                {
                    Title = "TestName",
                },
            },
            TotalCount = testTotalCount,
        };
        var catalogItemSuccess = new CatalogItemEntity()
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
        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Id = 1,
            Title = "Name",
            SubTitle = "SubTitle",
            Description = "Description",
            Price = 1000,
            AvailableStock = 100,
            CatalogBrand = new CatalogBrandDto()
            {
                Id = 1,
                Title = "TestBrand"
            },
            CatalogSubCategory = new CatalogSubCategoryDto()
            {
                Id = 1,
                Title = "TestType"
            },
            PictureUrl = "www.alevelwebsite.com/assets/img/1"
        };
        _catalogItemRepository.Setup(s => s.GetByBrandAsync(
            It.Is<string>(i => i == testBrand),
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);
        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItemEntity>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemByBrand(testBrand, testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemByBrandAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;
        var testBrand = "test";

        _catalogItemRepository.Setup(s => s.GetByBrandAsync(
            It.Is<string>(i => i == testBrand),
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemByBrand(testBrand, testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogItemBySubCategoryAsync_Success()
    {
        // arrange
        var testPageIndex = 0;
        var testPageSize = 4;
        var testTotalCount = 12;
        var testSubCategory = "test";
        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItemEntity>()
        {
            Data = new List<CatalogItemEntity>()
            {
                new CatalogItemEntity()
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
                },
            },
            TotalCount = testTotalCount,
        };
        var catalogItemSuccess = new CatalogItemEntity()
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
        var catalogItemDtoSuccess = new CatalogItemDto()
        {
            Id = 1,
            Title = "Name",
            SubTitle = "SubTitle",
            Description = "Description",
            Price = 1000,
            AvailableStock = 100,
            CatalogBrand = new CatalogBrandDto()
            {
                Id = 1,
                Title = "TestBrand"
            },
            CatalogSubCategory = new CatalogSubCategoryDto()
            {
                Id = 1,
                Title = "TestType"
            },
            PictureUrl = "www.alevelwebsite.com/assets/img/1"
        };
        _catalogItemRepository.Setup(s => s.GetBySubCategoryAsync(
            It.Is<string>(i => i == testSubCategory),
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).ReturnsAsync(pagingPaginatedItemsSuccess);
        _mapper.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItemEntity>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogItemBySubCategory(testSubCategory, testPageSize, testPageIndex);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
        result?.PageIndex.Should().Be(testPageIndex);
        result?.PageSize.Should().Be(testPageSize);
    }

    [Fact]
    public async Task GetCatalogItemBySubCategoryAsync_Failed()
    {
        // arrange
        var testPageIndex = 1000;
        var testPageSize = 10000;
        var testSubCategory = "test";

        _catalogItemRepository.Setup(s => s.GetBySubCategoryAsync(
            It.Is<string>(i => i == testSubCategory),
            It.Is<int>(i => i == testPageIndex),
            It.Is<int>(i => i == testPageSize))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogItemBySubCategory(testSubCategory, testPageSize, testPageIndex);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_Success()
    {
        // arrange
        var testTotalCount = 12;

        var pagingPaginatedBrandsSuccess = new PaginatedItems<CatalogBrandEntity>()
        {
            Data = new List<CatalogBrandEntity>()
            {
                new CatalogBrandEntity()
                {
                    Id = 1,
                    Title = "test"
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogBrandSuccess = new CatalogBrandEntity()
        {
            Id = 1,
            Title = "test"
        };

        var catalogBrandDtoSuccess = new CatalogBrandDto()
        {
            Id = 1,
            Title = "test"
        };

        _catalogItemRepository.Setup(s => s.GetBrandsAsync())
            .ReturnsAsync(pagingPaginatedBrandsSuccess);
        _mapper.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrandEntity>(i => i.Equals(catalogBrandSuccess)))).Returns(catalogBrandDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogBrands();

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
    }

    [Fact]
    public async Task GetCatalogBrandsAsync_Failed()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.GetBrandsAsync())
            .Returns((Func<PaginatedItemsResponse<CatalogBrandDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogBrands();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogSubCategoriesAsync_Success()
    {
        // arrange
        var testTotalCount = 12;

        var pagingPaginatedTypesSuccess = new PaginatedItems<CatalogSubCategoryEntity>()
        {
            Data = new List<CatalogSubCategoryEntity>()
            {
                new CatalogSubCategoryEntity()
                {
                    Id = 1,
                    Title = "test",
                    CatalogCategoryId = 1
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogSubCategorySuccess = new CatalogSubCategoryEntity()
        {
            Id = 1,
            Title = "test",
            CatalogCategoryId = 1
        };

        var catalogSubCategoryDtoSuccess = new CatalogSubCategoryDto()
        {
            Id = 1,
            Title = "test",
            CatalogCategory = new CatalogCategoryDto() { Id = 1, Title = "test" }
        };

        _catalogItemRepository.Setup(s => s.GetSubCategoriesAsync()).ReturnsAsync(pagingPaginatedTypesSuccess);
        _mapper.Setup(s => s.Map<CatalogSubCategoryDto>(
            It.Is<CatalogSubCategoryEntity>(i => i.Equals(catalogSubCategorySuccess)))).Returns(catalogSubCategoryDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogSubCategories();

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
    }

    [Fact]
    public async Task GetCatalogSubCategoriesAsync_Failed()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.GetSubCategoriesAsync())
            .Returns((Func<PaginatedItemsResponse<CatalogSubCategoryDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogSubCategories();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetCatalogCategoriesAsync_Success()
    {
        // arrange
        var testTotalCount = 12;

        var pagingPaginatedTypesSuccess = new PaginatedItems<CatalogCategoryEntity>()
        {
            Data = new List<CatalogCategoryEntity>()
            {
                new CatalogCategoryEntity()
                {
                    Id = 1,
                    Title = "test"
                },
            },
            TotalCount = testTotalCount,
        };

        var catalogCategorySuccess = new CatalogCategoryEntity()
        {
            Id = 1,
            Title = "test"
        };

        var catalogCategoryDtoSuccess = new CatalogCategoryDto()
        {
            Id = 1,
            Title = "test"
        };

        _catalogItemRepository.Setup(s => s.GetCategoriesAsync()).ReturnsAsync(pagingPaginatedTypesSuccess);
        _mapper.Setup(s => s.Map<CatalogCategoryDto>(
            It.Is<CatalogCategoryEntity>(i => i.Equals(catalogCategorySuccess)))).Returns(catalogCategoryDtoSuccess);

        // act
        var result = await _catalogService.GetCatalogCategories();

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
        result?.Count.Should().Be(testTotalCount);
    }

    [Fact]
    public async Task GetCatalogCategoriesAsync_Failed()
    {
        // arrange
        _catalogItemRepository.Setup(s => s.GetCategoriesAsync())
            .Returns((Func<PaginatedItemsResponse<CatalogCategoryDto>>)null!);

        // act
        var result = await _catalogService.GetCatalogCategories();

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetSubCategoriesByCategoryAsync_Success()
    {
        // arrange
        var testTotalCount = 12;
        var testCategory = "test";
        var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogSubCategoryEntity>()
        {
            Data = new List<CatalogSubCategoryEntity>()
            {
                new CatalogSubCategoryEntity()
                {
                    Id = 1,
                    Title = "Name",
                    CatalogCategoryId = 1
                },
            },
            TotalCount = testTotalCount
        };
        var catalogSubCategorySuccess = new CatalogSubCategoryEntity()
        {
            Id = 1,
            Title = "Name",
            CatalogCategoryId = 1
        };
        var catalogSubCategoryDtoSuccess = new CatalogSubCategoryDto()
        {
            Id = 1,
            Title = "Name",
            CatalogCategory = new CatalogCategoryDto() { Id = 1, Title = "test" }
        };
        _catalogItemRepository.Setup(s => s.GetSubCategoryByCategoryAsync(
            It.Is<string>(i => i == testCategory))).ReturnsAsync(pagingPaginatedItemsSuccess);
        _mapper.Setup(s => s.Map<CatalogSubCategoryDto>(
            It.Is<CatalogSubCategoryEntity>(i => i.Equals(catalogSubCategorySuccess)))).Returns(catalogSubCategoryDtoSuccess);

        // act
        var result = await _catalogService.GetSubCategoriesByCategory(testCategory);

        // assert
        result.Should().NotBeNull();
        result?.Data.Should().NotBeNull();
    }

    [Fact]
    public async Task GetSubCategoriesByCategoryAsync_Failed()
    {
        // arrange
        var testCategory = "test";

        _catalogItemRepository.Setup(s => s.GetSubCategoryByCategoryAsync(
            It.Is<string>(i => i == testCategory))).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

        // act
        var result = await _catalogService.GetSubCategoriesByCategory(testCategory);

        // assert
        result.Should().BeNull();
    }
}