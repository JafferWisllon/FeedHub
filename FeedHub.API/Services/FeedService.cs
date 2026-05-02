using FeedHub.API.Data;
using FeedHub.API.Dtos;
using FeedHub.API.Exceptions;
using FeedHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FeedHub.API.Services;

public class FeedService : IFeedService
{
    private readonly ApiDbContext _context;

    public FeedService(ApiDbContext context) 
        => _context = context;

    public async Task<Feed> AddAsync(CreateFeedDto request)
    {
        var urlValid = ValidateUrl(request.Url);
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
        => await _context.Feeds.FindAsync(id);

    public async Task<IEnumerable<Feed>> ListAsync() 
        => await _context.Feeds.ToListAsync();

    public async Task<IList<FeedItemResponseDto>> GetFeedItemsAsync(int id)
    {
        var feedExists = await _context.Feeds.AnyAsync(x => x.Id == id);

        if (feedExists is false)
            throw new FeedNotFoundException("Feed not found");

        var items = await _context.FeedItems.Where(x => x.FeedId == id).ToListAsync();
        return FeedItemResponseDto.FromEntity(items);
    }

    private bool ValidateUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return false;

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return false;

        return uri.Scheme.Equals(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase)
            || uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);
    }
}
