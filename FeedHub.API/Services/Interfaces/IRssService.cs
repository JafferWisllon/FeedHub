using FeedHub.API.Models;

namespace FeedHub.API.Services.Interfaces;

public interface IRssService
{
    Task<IList<FeedItem>> GetFeedItemsAsync(string url, int feedId);
}
