using FeedHub.Application.Dtos;
using FeedHub.Domain.Repositories;
using FeedHub.Domain.Rss;
using FeedHub.Exception.ExceptionsBase;

namespace FeedHub.Application.UseCases.Feed.Refresh;

public class RefreshFeedUseCase : IRefreshFeedUseCase
{
    private readonly IFeedRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRssService _rssService;
    public RefreshFeedUseCase(
        IFeedRepository repository, 
        IUnitOfWork unitOfWork, 
        IRssService rssService)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _rssService = rssService;
    }

    public async Task<RefreshFeedDto> Execute(int id)
    {
        var feed = await _repository.FindByIdAsync(id);

        if (feed is null)
            throw new FeedNotFoundException($"Feed with id {id} not found");

        var feedItems = await _rssService.GetFeedItemsAsync(feed.Url, feed.Id);

        var existingFeedsItems = feed
            .FeedItems
            .Select(x => x.Link)
            .ToHashSet();

        var remaining = feedItems
            .Where(x => !existingFeedsItems.Contains(x.Link))
            .ToList();

        await _repository.AddRangeAsync(remaining);
        await _unitOfWork.Commit();

        return new RefreshFeedDto(feedItems.Count(), remaining.Count());
    }
}
