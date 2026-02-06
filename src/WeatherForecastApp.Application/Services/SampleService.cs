namespace WeatherForecastApp.Application.Services;

public class SampleService : ISampleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISampleRepository _repository;
    private readonly IMapper _mapper;

    public SampleService(
        IUnitOfWork unitOfWork,
        ISampleRepository repository,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<SampleDto> CreateSampleAsync(string name)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var entity = SampleEntity.Create(name);
            await _repository.AddAsync(entity);

            await _unitOfWork.SaveAsync();
            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<SampleEntity, SampleDto>(entity);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IEnumerable<SampleDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return _mapper.Map<SampleEntity, SampleDto>(items);
    }
}
