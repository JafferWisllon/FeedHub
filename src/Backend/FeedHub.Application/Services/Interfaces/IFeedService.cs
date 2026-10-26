using FeedHub.Application.Dtos;
using FeedHub.Domain.Models;

namespace FeedHub.Application.Services.Interfaces;

public interface IFeedService
{
    Task<Feed> AddAsync(CreateFeedDto request);
    Task<IEnumerable<Feed>> ListAsync();
    Task<Feed> GetById(int id);
    Task<PaginatedFeedItemsResponseDto> GetFeedItemsAsync(int id, int page, int pageSize);
    Task<RefreshFeedDto> RefreshFeedAsync(int id);
}
