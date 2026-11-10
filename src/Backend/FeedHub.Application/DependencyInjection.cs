using FeedHub.Application.Services;
using FeedHub.Application.UseCases.Feed.Create;
using FeedHub.Application.UseCases.Feed.GetById;
using FeedHub.Application.UseCases.Feed.GetFeedItems;
using FeedHub.Application.UseCases.Feed.List;
using FeedHub.Application.UseCases.Feed.Refresh;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FeedHub.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddUseCases();
        services.AddScoped<IRssService, RssService>();
        services.AddScoped<IAuthService, AuthService>();

    }
    public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JwtConfig");
        var jwtSecret = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.MapInboundClaims = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(jwtSecret),
                ClockSkew = TimeSpan.Zero
            };
        });
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateFeedUseCase, CreateFeedUseCase>();
        services.AddScoped<IGetFeedByIdUseCase, GetFeedByIdUseCase>();
        services.AddScoped<IGetFeedItemsUseCase, GetFeedItemsUseCase>();
        services.AddScoped<IListFeedsUseCase, ListFeedsUseCase>();
        services.AddScoped<IRefreshFeedUseCase, RefreshFeedUseCase>();
    }
}
