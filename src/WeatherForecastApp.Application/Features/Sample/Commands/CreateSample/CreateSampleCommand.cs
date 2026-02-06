namespace WeatherForecastApp.Application.Features.Sample.Commands.CreateSample;

public record CreateSampleCommand(string Name) : IRequest<Result<CreateSampleResponse>>;
