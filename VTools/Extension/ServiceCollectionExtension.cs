using NodaTime;
using VTools.Services;
using VTools.Services.Interfaces;

namespace VTools.Extension;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IClock, SystemClock>(_ => SystemClock.Instance);

        var products = configuration.GetSection("Products").Get<string[]>();
        services.AddScoped<IProductService, ProductService>(_ => new ProductService(products!));

        return services;
    }
}