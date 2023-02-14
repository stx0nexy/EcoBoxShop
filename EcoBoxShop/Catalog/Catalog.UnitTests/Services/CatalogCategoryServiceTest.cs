using System.Threading;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;

namespace Catalog.UnitTests.Services;

public class CatalogCategoryServiceTest
{
    private readonly ICatalogCategoryService _catalogCategoryService;

    private readonly Mock<ICatalogCategoryRepository> _catalogCategoryRepository;
    private readonly Mock<IDbContextWrapper<ApplicationDbContext>> _dbContextWrapper;
    private readonly Mock<ILogger<CatalogService>> _logger;
    private readonly Mock<IMapper> _mapper;

    public CatalogCategoryServiceTest()
    {
        _catalogCategoryRepository = new Mock<ICatalogCategoryRepository>();
        _dbContextWrapper = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        _logger = new Mock<ILogger<CatalogService>>();
        _mapper = new Mock<IMapper>();

        var dbContextTransaction = new Mock<IDbContextTransaction>();
        _dbContextWrapper.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransaction.Object);

        _catalogCategoryService = new CatalogCategoryService(_dbContextWrapper.Object, _logger.Object, _catalogCategoryRepository.Object, _mapper.Object);
    }

    [Fact]
    public async Task AddAsync_Success()
    {
        // arrange
        var testResult = 1;
        var testCategory = "test";

        _catalogCategoryRepository.Setup(s => s.AddAsync(
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogCategoryService.Add(testCategory);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task AddAsync_Failed()
    {
        // arrange
        int? testResult = null;
        var testCategory = "test";

        _catalogCategoryRepository.Setup(s => s.AddAsync(
            It.IsAny<string>())).ReturnsAsync(testResult);

        // act
        var result = await _catalogCategoryService.Add(testCategory);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteAsync_Success()
    {
        // arrange
        var testResult = true;
        var testId = 1;

        _catalogCategoryRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _catalogCategoryService.Delete(testId);

        // assert
        result.Should().Be(testResult);
    }

    [Fact]
    public async Task DeleteAsync_Failed()
    {
        // arrange
        var testId = 1000;
        bool? testResult = null;

        _catalogCategoryRepository.Setup(s => s.DeleteAsync(
            It.Is<int>(i => i == testId))).ReturnsAsync(testResult);

        // act
        var result = await _catalogCategoryService.Delete(testId);

        // assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_Success()
    {
        // arrange
        var catalogCategorySuccess = new CatalogCategoryEntity()
        {
            Id = 1,
            Title = "TestCategory"
        };
        var catalogCategoryDtoSuccess = new CatalogCategoryDto()
        {
            Id = 1,
            Title = "TestCategory"
        };
        _catalogCategoryRepository.Setup(s => s.UpdateAsync(
            It.IsAny<CatalogCategoryEntity>())).ReturnsAsync(catalogCategorySuccess);
        _mapper.Setup(s => s.Map<CatalogCategoryDto>(
            It.Is<CatalogCategoryEntity>(i => i.Equals(catalogCategorySuccess)))).Returns(catalogCategoryDtoSuccess);

        // act
        var result = await _catalogCategoryService.Update(catalogCategorySuccess.Id, catalogCategorySuccess.Title);

        // assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task UpdateAsync_Failed()
    {
        // arrange
        var testId = 1;
        var testCategory = "test";
        _catalogCategoryRepository.Setup(s => s.UpdateAsync(
            It.IsAny<CatalogCategoryEntity>())).Returns((Func<CatalogCategoryDto>)null!);

        // act
        var result = await _catalogCategoryService.Update(testId, testCategory);

        // assert
        result.Should().BeNull();
    }
}