using AnguilarTutorialAPI.Data;
using AnguilarTutorialAPI.Data.Repositories;
using AnguilarTutorialAPI.Helpers;
using AnguilarTutorialAPI.Interfaces;
using AnguilarTutorialAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace AnguilarTutorialAPI.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(AutoMappersProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
