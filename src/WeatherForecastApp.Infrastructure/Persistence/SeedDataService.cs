namespace WeatherForecastApp.Infrastructure.Persistence;

public class SeedDataService(
    ISampleRepository repository,
    IUnitOfWork unitOfWork) : ISeedDataService
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        await unitOfWork.BeginTransactionAsync();
        try
        {
            var existing = await repository.GetAllAsync();
            if (existing.Any())
            {
                await unitOfWork.CommitTransactionAsync();
                return;
            }

            await repository.AddAsync(SampleEntity.Create("Default Sample"));
            await unitOfWork.SaveAsync();
            await unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
