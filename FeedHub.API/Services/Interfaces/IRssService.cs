using FeedHub.API.Dtos;

namespace FeedHub.API.Services.Interfaces;

public interface IRssService
{
    Task<IList<FeedItemResponseDto>> GetFeedItemsAsync(string url);
}
