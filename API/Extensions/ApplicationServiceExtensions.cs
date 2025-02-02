using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.SignalR;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

namespace API;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration config)
    {

        services.AddDbContext<DataContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            /*var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
           "server=127.0.0.1;database=DatingAppDb;user=dotnetuser;password=12345"*/


        });

        services.AddCors();

        services.AddScoped<ITokenService, TokenService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        services.AddScoped<IPhotoService, PhotoService>();
        services.AddScoped<LogUserActivity>();
        services.AddSignalR();
        services.AddSingleton<PresenceTracker>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;

    }

}
