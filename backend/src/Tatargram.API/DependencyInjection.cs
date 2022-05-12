using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Tatargram.Data;
using Tatargram.Interfaces.Repositories;
using Tatargram.Interfaces.Services;
using Tatargram.Models;
using Tatargram.Repositories;
using Tatargram.Services;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(config.GetConnectionString("psql")));

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 8;
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = config["JWT:ValidIssuer"],
                ValidAudience = config["JWT:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JWT:Secret"])),
                ValidateLifetime = false,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddHttpContextAccessor();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<TokenService>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<IPostService, PostService>();



        return services;
    }
}