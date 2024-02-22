using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Application.Interfaces;
using Server.Application.Services;
using Server.Infrastructure;
using System.Text;

namespace Server.Application;

public static class ServiceExtension
{
    public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(o =>
        {
            o.UseNpgsql(configuration["Database:PostgreConnectionString"] ?? throw new InvalidOperationException("Connection string not found."));
        });
        services.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IProductService, ProductService>()
            .AddScoped<ICategoryService, CategoryService>();

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(o =>
        {
            o.Password.RequiredLength = 8;
            o.Password.RequireDigit = false;
            o.Password.RequireLowercase = false;
            o.SignIn.RequireConfirmedEmail = false;
        });

        services
            .AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["JWT:ValidIssuer"],
                    ValidAudience = configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!))
                };
            });

        return services;
    }
}
