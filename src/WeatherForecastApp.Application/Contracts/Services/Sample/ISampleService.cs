namespace WeatherForecastApp.Application.Contracts.Services.Sample;

public interface ISampleService
{
    Task<SampleDto> CreateSampleAsync(string name);
    Task<IEnumerable<SampleDto>> GetAllAsync();
}
