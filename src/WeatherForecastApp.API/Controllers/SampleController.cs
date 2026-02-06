namespace WeatherForecastApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting(RateLimitingPolicies.SlidingWindow)]
public class SampleController(IMediator mediator) : ControllerBase
{
    [HttpPost(Name = "CreateSample")]
    public async Task<ActionResult<Result<CreateSampleResponse>>> Create([FromBody] CreateSampleRequest request)
    {
        var result = await mediator.Send(new CreateSampleCommand(request.Name));
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}

public record CreateSampleRequest(string Name);
