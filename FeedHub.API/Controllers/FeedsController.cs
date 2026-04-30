using FeedHub.API.Data;
using FeedHub.API.Dtos;
using FeedHub.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Create([FromBody] CreateFeedDto request)
    {
        var feed = new Feed
        {
            Url = request.Url,
            Name = request.Name,
        };

        _context.Feeds.Add(feed);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = feed.Id }, feed);
    }

    [HttpGet]
    public async Task<ActionResult> List()
    {
        return Ok(await _context.Feeds.ToListAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Get(int id)
    {
        var feed = await _context.Feeds.FindAsync(id);
        if (feed is null) return NotFound();
        return Ok(feed);
    }
}
