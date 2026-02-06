namespace WeatherForecastApp.Domain.Entities;

public class SampleEntity : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public static SampleEntity Create(string name)
    {
        return new SampleEntity { Name = name };
    }

    private SampleEntity() { }
}
