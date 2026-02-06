namespace WeatherForecastApp.Application.Specifications;

public class NameContainsSpecification(string searchTerm) : BaseSpecification<SampleEntity>
{
    public override Expression<Func<SampleEntity, bool>> ToExpression()
    {
        return e => e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
    }
}
