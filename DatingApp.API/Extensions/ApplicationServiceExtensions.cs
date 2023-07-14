using DatingApp.API.Data;
using DatingApp.API.Interfaces;
using DatingApp.API.Interfaces.JWT;
using DatingApp.API.Services.JWT;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<DatingAppDbContext>(opts =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                opts.UseSqlite(connectionString);
            });
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}