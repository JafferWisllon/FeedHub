using FeedHub.Application.Dtos;

namespace FeedHub.Application.UseCases.Feed.GetFeedItems;

public interface IGetFeedItemsUseCase
{
    Task<PaginatedFeedItemsResponseDto> Execute(int id, int page, int pageSize);
}
