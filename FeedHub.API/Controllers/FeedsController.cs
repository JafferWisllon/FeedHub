using FeedHub.API.Dtos;
using FeedHub.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeedHub.API.Controllers;

[ApiController]
[Route("api/feeds")]
public class FeedsController : ControllerBase
{
    private readonly IFeedService _feedService;

    public FeedsController(IFeedService feedService) 
        => _feedService = feedService;

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Create([FromBody] CreateFeedDto request)
    {
        var feed = await _feedService.AddAsync(request);
        return CreatedAtAction(nameof(Get), new { id = feed.Id }, feed);
    }

    [HttpGet]
    public async Task<ActionResult> List()
    {
        return Ok(await _feedService.ListAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Get(int id)
    {
        var feed = await _feedService.GetById(id);
        if (feed is null) return NotFound();
        return Ok(feed);
    }
}
