namespace WeatherForecastApp.Infrastructure.Persistence.Configurations;

public class SampleEntityConfiguration : IEntityTypeConfiguration<SampleEntity>
{
    public void Configure(EntityTypeBuilder<SampleEntity> builder)
    {
        builder.ToTable("Samples");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasMaxLength(50);
        builder.Property(x => x.Name).HasMaxLength(200);
        builder.Property(x => x.CreatedAt);
        builder.Property(x => x.UpdatedAt);
    }
}
