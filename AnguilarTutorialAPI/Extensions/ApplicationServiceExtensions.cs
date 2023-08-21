using AnguilarTutorialAPI.Data;
using AnguilarTutorialAPI.Interfaces;
using AnguilarTutorialAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AnguilarTutorialAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
