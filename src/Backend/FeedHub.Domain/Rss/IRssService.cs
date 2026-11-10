using FeedHub.Domain.Models;

namespace FeedHub.Domain.Rss;

public interface IRssService
{
    Task<IList<FeedItem>> GetFeedItemsAsync(string url, int feedId);
}
