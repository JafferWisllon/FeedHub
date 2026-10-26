using FeedHub.Domain.Models;
using FeedHub.Domain.Repositories;
using FeedHub.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FeedHub.Infrastructure.Data.Repositories;

internal class FeedRepository : IFeedRepository
{
    private readonly ApiDbContext _context;

    public FeedRepository(ApiDbContext dbContext) 
        => _context = dbContext;

    public async Task AddAsync(Feed feed)
    {
        await _context.AddAsync(feed);
    }

    public async Task AddRangeAsync(List<FeedItem> feeds)
        => await _context.AddRangeAsync(feeds);

    public async Task<Feed> FindByIdAsync(int id) 
        => await _context.Feeds.Include(f => f.FeedItems).FirstOrDefaultAsync(f => f.Id == id);

    public async Task<Feed> FindByUrlAsync(string url) 
        => await _context.Feeds.FirstOrDefaultAsync(f => f.Url == url);

    public async Task<List<FeedItem>> GetFeedItems(int feedId, int offset, int pageSize) 
        => await _context
            .FeedItems
            .AsNoTracking()
            .Where(x => x.FeedId == feedId)
            .OrderByDescending(i => i.PublishAt)
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();

    public async Task<int> GetFeedItemsCount(int feedId) 
        => await _context.FeedItems.AsNoTracking().Where(x => x.FeedId == feedId).CountAsync();

    public async Task<IEnumerable<Feed>> ListAsync()
        => await _context.Feeds.ToListAsync();
}
