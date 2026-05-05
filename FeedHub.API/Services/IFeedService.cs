using FeedHub.API.Dtos;
using FeedHub.API.Models;

namespace FeedHub.API.Services;

public interface IFeedService
{
    Task<Feed> AddAsync(CreateFeedDto request);
    Task<IEnumerable<Feed>> ListAsync();
    Task<Feed> GetById(int id);
    Task<IList<FeedItemResponseDto>> GetFeedItemsAsync(int id);
    Task<IList<FeedItemResponseDto>> RefreshFeedAsync(int id);
}
