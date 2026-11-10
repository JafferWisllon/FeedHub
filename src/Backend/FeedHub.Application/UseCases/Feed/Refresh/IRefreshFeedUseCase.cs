using FeedHub.Application.Dtos;

namespace FeedHub.Application.UseCases.Feed.Refresh;

public interface IRefreshFeedUseCase
{
    Task<RefreshFeedDto> Execute(int id);
}
