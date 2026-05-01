using FeedHub.API.Data;
using FeedHub.API.Dtos;
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
}
