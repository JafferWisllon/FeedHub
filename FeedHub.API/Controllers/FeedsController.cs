using FeedHub.API.Data;
using FeedHub.API.Dtos;
using FeedHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace FeedHub.API.Controllers;

[ApiController]
[Route("api/feeds")]
public class FeedsController : ControllerBase
{
    private readonly ApiDbContext _context;

    public FeedsController(ApiDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateFeedDto request)
    {
        var feed = new Feed
        {
            Url = request.Url,
            Name = request.Name,
        };

        _context.Feeds.Add(feed);
        await _context.SaveChangesAsync();

        return Ok(feed);
    }
}
