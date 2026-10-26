using FeedHub.Domain.Models;

namespace FeedHub.Domain.Repositories;

public interface IFeedRepository
{
    Task<IEnumerable<Feed>> ListAsync();
    Task<Feed> FindByIdAsync(int id);
    Task AddAsync(Feed feed);
    Task AddRangeAsync(List<FeedItem> feeds);
    Task<Feed> FindByUrlAsync(string url);

    Task<int> GetFeedItemsCount(int feedId);
    Task<List<FeedItem>> GetFeedItems(int feedId, int offset, int pageSize);
}
