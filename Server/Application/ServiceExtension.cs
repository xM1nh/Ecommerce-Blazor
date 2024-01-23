using Microsoft.EntityFrameworkCore;
using Server.Application.Interfaces;
using Server.Application.Services;
using Server.Infrastructure;

namespace Server.Application;

public static class ServiceExtension
{
    public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(o =>
        {
            o.UseNpgsql(configuration["Database:PostgreConnectionString"] ?? throw new InvalidOperationException("Connection string not found."));
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
