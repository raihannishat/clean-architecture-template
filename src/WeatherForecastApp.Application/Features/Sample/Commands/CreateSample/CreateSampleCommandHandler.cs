namespace WeatherForecastApp.Application.Features.Sample.Commands.CreateSample;

public class CreateSampleCommandHandler(
    ISampleService service,
    IMapper mapper) : IRequestHandler<CreateSampleCommand, Result<CreateSampleResponse>>
{
    public async Task<Result<CreateSampleResponse>> Handle(CreateSampleCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var dto = await service.CreateSampleAsync(request.Name);

            var response = mapper.Map<SampleDto, CreateSampleResponse>(dto);
            response = response with { Message = "Sample created successfully" };

            return Result<CreateSampleResponse>.Success(response);
        }
        catch (Exception ex)
        {
            return Result<CreateSampleResponse>.Failure(ex.Message);
        }
    }
}
