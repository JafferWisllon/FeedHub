using FeedHub.Domain.Repositories;
using FeedHub.Infrastructure.Data.Context;
using FeedHub.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FeedHub.Infrastructure.Data;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseSqlServer(connection);
        });

        services.AddScoped<IFeedRepository, FeedRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
