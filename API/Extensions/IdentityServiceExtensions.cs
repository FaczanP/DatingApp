using System.Text;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API;

public static class IdentityServiceExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
     IConfiguration config)
    {
        services.AddIdentityCore<AppUser>(opt => {
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddRoles<AppRole>()
        .AddRoleManager<RoleManager<AppRole>>()
        .AddEntityFrameworkStores<DataContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(config["TokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        options.Events = new JwtBearerEvents{
            OnMessageReceived = context =>{
                var accsessToken = context.Request.Query["accsess_token"];

                var path = context.HttpContext.Request.Path;
                if(!string.IsNullOrEmpty(accsessToken) && path.StartsWithSegments("/hubs")){
                    context.Token = accsessToken;
                }
                return Task.CompletedTask;
            }
        };
    });

    services.AddAuthorization(opt => {
        opt.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
        opt.AddPolicy("ModeratePhotoRole", policy => policy.RequireRole("Admin", "Moderator"));
    });

    return services;
    }

}
