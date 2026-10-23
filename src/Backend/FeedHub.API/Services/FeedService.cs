using FeedHub.API.Data.Context;
using FeedHub.API.Dtos;
using FeedHub.API.Exceptions;
using FeedHub.API.Mappings;
using FeedHub.API.Models;
using FeedHub.API.Services.Interfaces;
using FeedHub.API.Validators;
using Microsoft.EntityFrameworkCore;

namespace FeedHub.API.Services;

public class FeedService : IFeedService
{
    private readonly ApiDbContext _context;
    private readonly IRssService _rssService;
    public FeedService(ApiDbContext context, IRssService rssService)
    {
        _context = context;
        _rssService = rssService;
    }

    public async Task<Feed> AddAsync(CreateFeedDto request)
    {
        var urlValid = UrlValidator.ValidateUrl(request.Url);
        if (!urlValid)
            throw new InvalidFeedUrlException("The Url field is not a valid fully-qualified http, https URL");

        var feedExists = _context.Feeds.FirstOrDefault(f => f.Url == request.Url);

        if (feedExists is not null)
            throw new AlreadyExistsException("Feed already exists");

        var feed = new Feed
        {
            Url = request.Url,
            Name = request.Name,
        };

        _context.Feeds.Add(feed);
        await _context.SaveChangesAsync();
        return feed;
    }

    public async Task<Feed> GetById(int id)
    {
        var feed = await _context.Feeds.FindAsync(id);
        if (feed is null)
            throw new FeedNotFoundException($"Feed with id {id} not found");

        return feed;
    }

    public async Task<IEnumerable<Feed>> ListAsync() 
        => await _context.Feeds.ToListAsync();

    public async Task<PaginatedFeedItemsResponseDto> GetFeedItemsAsync(int id, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
            throw new BadRequestException("Invalid query params");

        var feedExists = await _context.Feeds.AnyAsync(x => x.Id == id);

        if (feedExists is false)
            throw new FeedNotFoundException($"Feed with id {id} not found");

        var totalCount = await _context.FeedItems.AsNoTracking().Where(x => x.FeedId == id).CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var offset = (page - 1) * pageSize;

        var items = await _context
            .FeedItems
            .AsNoTracking()
            .Where(x => x.FeedId == id)
            .OrderByDescending(i => i.PublishAt)
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();

        var nextPage = NextPage(totalPages, page, pageSize, id);

        return new PaginatedFeedItemsResponseDto(page, pageSize, totalCount, totalPages, nextPage, FeedItemEntityMappings.ToDto(items));
    }

    public async Task<RefreshFeedDto> RefreshFeedAsync(int id)
    {
        var feed = await _context
            .Feeds
            .Include(f => f.FeedItems)
            .FirstOrDefaultAsync(f => f.Id == id);

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

        _context.AddRange(remaining);
        await _context.SaveChangesAsync();

        return new RefreshFeedDto(feedItems.Count(), remaining.Count());
    }

    private string? NextPage(int totalPages, int page, int pageSize, int feedId)
    {
        if (page >= totalPages)
            return null;

        return $"GET /feeds/{feedId}/items?page={page+1}&pageSize={pageSize}";
    }
}
