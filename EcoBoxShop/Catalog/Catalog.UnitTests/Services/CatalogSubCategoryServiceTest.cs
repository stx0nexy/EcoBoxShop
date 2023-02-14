using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services;

public class CatalogSubCategoryServiceTest
{
     private readonly ICatalogSubCategoryService _catalogSubCategoryService;

     private readonly Mock<ICatalogSubCategoryRepository> _catalogSubCategoryRepository;
     private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
     private readonly Mock<ICatalogCategoryRepository> _catalogCategoryRepository;
     private readonly Mock<ILogger<CatalogService>> _logger;
     private readonly Mock<IMapper> _mapper;
     public CatalogSubCategoryServiceTest()
        {
            _catalogSubCategoryRepository = new Mock<ICatalogSubCategoryRepository>();
            _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            _catalogCategoryRepository = new Mock<ICatalogCategoryRepository>();
            _logger = new Mock<ILogger<CatalogService>>();
            _mapper = new Mock<IMapper>();

            var dbContextTransaction = new Mock<IDbContextTransaction>();
            _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

            _catalogSubCategoryService = new CatalogSubCategoryService(_dbContextWrapper.Object, _logger.Object, _catalogSubCategoryRepository.Object, _catalogCategoryRepository.Object, _mapper.Object);
        }

     [Fact]
     public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;
        var testSubCategory = "test";
        var testCategoryId = 1;

        _catalogSubCategoryRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogSubCategoryService.Add(testSubCategory, testCategoryId);

        // assert
        result.Should().Be(testResult);
    }

     [Fact]
     public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;
        var testSubCategory = "test";
        var testCategoryId = 1;

        _catalogSubCategoryRepository.Setup(s => s.AddAsync(
            It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogSubCategoryService.Add(testSubCategory, testCategoryId);

        // assert
        result.Should().Be(testResult);
    }

     [Fact]
     public async Task DeleteAsync_Success()
    {
        // arrange
        var testResult = true;
        var testId = 1;

        _catalogSubCategoryRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _catalogSubCategoryService.Delete(testId);

        // assert
        result.Should().Be(testResult);
    }

     [Fact]
     public async Task DeleteAsync_Failed()
    {
        // arrange
        var testId = 1000;
        bool? testResult = null;

        _catalogSubCategoryRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _catalogSubCategoryService.Delete(testId);

        // assert
        result.Should().BeNull();
    }

     [Fact]
     public async Task UpdateAsync_Success()
    {
        // arrange
        var catalogSubCategorySuccess = new CatalogSubCategoryEntity()
        {
            Id = 1,
            Title = "TestCategory",
            CatalogCategoryId = 1
        };
        var catalogSubCategoryDtoSuccess = new CatalogSubCategoryDto()
        {
            Id = 1,
            Title = "TestCategory",
            CatalogCategory = new CatalogCategoryDto() { Id = 1, Title = "test" }
        };
        _catalogSubCategoryRepository.Setup(s => s.UpdateAsync(
            It.IsAny<CatalogSubCategoryEntity>())).ReturnsAsync(catalogSubCategorySuccess);
        _mapper.Setup(s => s.Map<CatalogSubCategoryDto>(
            It.Is<CatalogSubCategoryEntity>(i => i.Equals(catalogSubCategorySuccess)))).Returns(catalogSubCategoryDtoSuccess);

        // act
        var result = await _catalogSubCategoryService.Update(catalogSubCategorySuccess.Id, catalogSubCategorySuccess.Title, catalogSubCategorySuccess.CatalogCategoryId);

        // assert
        result.Should().NotBeNull();
    }

     [Fact]
     public async Task UpdateAsync_Failed()
    {
        // arrange
        var testId = 1;
        var testSubCategory = "test";
        var testCategoryId = 1;
        _catalogSubCategoryRepository.Setup(s => s.UpdateAsync(
            It.IsAny<CatalogSubCategoryEntity>())).Returns((Func<CatalogSubCategoryDto>)null!);

        // act
        var result = await _catalogSubCategoryService.Update(testId, testSubCategory, testCategoryId);

        // assert
        result.Should().BeNull();
    }
}