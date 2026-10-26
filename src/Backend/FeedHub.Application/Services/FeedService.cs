using FeedHub.Application.Dtos;
using FeedHub.Application.Exceptions;
using FeedHub.Application.Mappings;
using FeedHub.Application.Validators;
using FeedHub.Domain.Models;
using FeedHub.Domain.Repositories;

namespace FeedHub.Application.Services;

public class FeedService : IFeedService
{
    private readonly IFeedRepository _feedRepository;
    private readonly IRssService _rssService;
    private readonly IUnitOfWork _unitOfWork;
    public FeedService(IFeedRepository feedRepository, IRssService rssService, IUnitOfWork unitOfWork)
    {
        _feedRepository = feedRepository;
        _rssService = rssService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Feed> AddAsync(CreateFeedDto request)
    {
        var urlValid = UrlValidator.ValidateUrl(request.Url);
        if (!urlValid)
            throw new InvalidFeedUrlException("The Url field is not a valid fully-qualified http, https URL");

        var feedExists = await _feedRepository.FindByUrlAsync(request.Url);

        if (feedExists is not null)
            throw new AlreadyExistsException("Feed already exists");

        var feed = new Feed
        {
            Url = request.Url,
            Name = request.Name,
        };

        await _feedRepository.AddAsync(feed);

        await _unitOfWork.Commit();
        return feed;
    }

    public async Task<Feed> GetById(int id)
    {
        var feed = await _feedRepository.FindByIdAsync(id);
        if (feed is null)
            throw new FeedNotFoundException($"Feed with id {id} not found");

        return feed;
    }

    public async Task<IEnumerable<Feed>> ListAsync() 
        => await _feedRepository.ListAsync();

    public async Task<PaginatedFeedItemsResponseDto> GetFeedItemsAsync(int id, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
            throw new BadRequestException("Invalid query params");

        var feed = await GetById(id);

        var totalCount = await _feedRepository.GetFeedItemsCount(feed.Id);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var offset = (page - 1) * pageSize;

        var items = await _feedRepository.GetFeedItems(feed.Id, offset, pageSize);

        var nextPage = NextPage(totalPages, page, pageSize, id);

        return new PaginatedFeedItemsResponseDto(page, pageSize, totalCount, totalPages, nextPage, FeedItemEntityMappings.ToDto(items));
    }

    public async Task<RefreshFeedDto> RefreshFeedAsync(int id)
    {
        var feed = await _feedRepository.FindByIdAsync(id);

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

        await _feedRepository.AddRangeAsync(remaining);
        await _unitOfWork.Commit();

        return new RefreshFeedDto(feedItems.Count(), remaining.Count());
    }

    private string? NextPage(int totalPages, int page, int pageSize, int feedId)
    {
        if (page >= totalPages)
            return null;

        return $"GET /feeds/{feedId}/items?page={page+1}&pageSize={pageSize}";
    }
}
