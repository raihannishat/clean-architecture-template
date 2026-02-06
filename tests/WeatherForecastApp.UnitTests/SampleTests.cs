namespace WeatherForecastApp.UnitTests;

public class SampleTests
{
    [Fact]
    public void Result_Success_ReturnsValue()
    {
        var result = Result<CreateSampleResponse>.Success(new CreateSampleResponse("1", "Test"));
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Test", result.Value!.Name);
    }
}

