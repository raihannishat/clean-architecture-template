namespace WeatherForecastApp.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddEndpointsApiExplorer();

        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous",
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = configuration.GetValue<int>("RateLimiting:PermitLimit", 100),
                        Window = TimeSpan.FromMinutes(configuration.GetValue<int>("RateLimiting:WindowMinutes", 1))
                    }));

            options.AddFixedWindowLimiter(RateLimitingPolicies.FixedWindow, options =>
            {
                options.PermitLimit = configuration.GetValue<int>("RateLimiting:PermitLimit", 100);
                options.Window = TimeSpan.FromMinutes(configuration.GetValue<int>("RateLimiting:WindowMinutes", 1));
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = configuration.GetValue<int>("RateLimiting:QueueLimit", 10);
            });

            options.AddSlidingWindowLimiter(RateLimitingPolicies.SlidingWindow, options =>
            {
                options.PermitLimit = configuration.GetValue<int>("RateLimiting:SlidingWindowPermitLimit", 50);
                options.Window = TimeSpan.FromMinutes(configuration.GetValue<int>("RateLimiting:SlidingWindowMinutes", 1));
                options.SegmentsPerWindow = configuration.GetValue<int>("RateLimiting:SegmentsPerWindow", 2);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = configuration.GetValue<int>("RateLimiting:QueueLimit", 10);
            });

            options.AddTokenBucketLimiter(RateLimitingPolicies.TokenBucket, options =>
            {
                options.TokenLimit = configuration.GetValue<int>("RateLimiting:TokenLimit", 100);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = configuration.GetValue<int>("RateLimiting:QueueLimit", 10);
                options.ReplenishmentPeriod = TimeSpan.FromSeconds(configuration.GetValue<int>("RateLimiting:ReplenishmentPeriodSeconds", 10));
                options.TokensPerPeriod = configuration.GetValue<int>("RateLimiting:TokensPerPeriod", 10);
                options.AutoReplenishment = true;
            });

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

                var retryAfterSeconds = 60;
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    retryAfterSeconds = (int)retryAfter.TotalSeconds;
                    context.HttpContext.Response.Headers.RetryAfter = retryAfterSeconds.ToString();
                }

                await context.HttpContext.Response.WriteAsJsonAsync(new
                {
                    error = "Too many requests. Please try again later.",
                    retryAfter = retryAfterSeconds
                }, cancellationToken);
            };
        });

        return services;
    }
}
