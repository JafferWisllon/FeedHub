using FeedHub.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FeedHub.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFeedService, FeedService>();
        services.AddScoped<IRssService, RssService>();
    }
}
