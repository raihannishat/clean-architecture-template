namespace WeatherForecastApp.UnitTests.Services;

public class SampleServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ISampleRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly SampleService _sut;

    public SampleServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _repositoryMock = new Mock<ISampleRepository>();
        _mapperMock = new Mock<IMapper>();
        _sut = new SampleService(_unitOfWorkMock.Object, _repositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task CreateSampleAsync_CallsUnitOfWorkAndRepository_ReturnsMappedDto()
    {
        var entity = SampleEntity.Create("Test");
        var dto = new SampleDto { Id = "1", Name = "Test" };
        _mapperMock.Setup(m => m.Map<SampleEntity, SampleDto>(It.IsAny<SampleEntity>())).Returns(dto);

        var result = await _sut.CreateSampleAsync("Test");

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(), Times.Once);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<SampleEntity>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveAsync(), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitTransactionAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtos()
    {
        var items = new List<SampleEntity> { SampleEntity.Create("A") };
        var dtos = new List<SampleDto> { new() { Id = "1", Name = "A" } };
        _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(items);
        _mapperMock.Setup(m => m.Map<SampleEntity, SampleDto>(It.IsAny<IEnumerable<SampleEntity>>())).Returns(dtos);

        var result = await _sut.GetAllAsync();

        Assert.NotNull(result);
        Assert.Single(result);
        _repositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
