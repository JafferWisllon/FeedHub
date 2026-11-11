using FeedHub.Domain.Identity;
using FeedHub.Domain.Models;
using FeedHub.Domain.Repositories;
using FeedHub.Domain.Rss;
using FeedHub.Infrastructure.Data.Context;
using FeedHub.Infrastructure.Data.Repositories;
using FeedHub.Infrastructure.Identity;
using FeedHub.Infrastructure.Rss;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace FeedHub.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseSqlServer(connection);
        });

        services.AddRepositories();
        services.AddServices();

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<ApiDbContext>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFeedRepository, FeedRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IRssService, RssService>();
        services.AddScoped<IIdentityService, IdentityService>();
    }
}
