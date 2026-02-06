namespace WeatherForecastApp.Infrastructure.Persistence;

public sealed class WeatherForecastAppDbContext(DbContextOptions<WeatherForecastAppDbContext> options) : DbContext(options)
{
    public DbSet<SampleEntity> Samples => Set<SampleEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
