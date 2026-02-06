namespace WeatherForecastApp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        string connectionString)
    {
        // Database Configuration
        services.AddDbContext<WeatherForecastAppDbContext>(options =>
            options.UseNpgsql(connectionString));

        // Repository Registration
        services.AddScoped<ISampleRepository, SampleRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Seed (uses UoW + repository only)
        services.AddScoped<ISeedDataService, SeedDataService>();

        return services;
    }
}
