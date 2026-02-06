namespace WeatherForecastApp.Infrastructure.Repositories;

public class SampleRepository(WeatherForecastAppDbContext context)
    : GenericRepository<SampleEntity>(context), ISampleRepository
{
}
