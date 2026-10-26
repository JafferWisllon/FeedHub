using FeedHub.Domain.Models;

namespace FeedHub.Application.Services.Interfaces;

public interface IRssService
{
    Task<IList<FeedItem>> GetFeedItemsAsync(string url, int feedId);
}
