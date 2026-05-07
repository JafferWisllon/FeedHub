using FeedHub.API.Dtos;
using FeedHub.API.Services.Interfaces;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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
        return Ok(await _feedService.GetById(id));
    }

    [HttpGet("{id:int}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> GetItems(int id)
    {
        return Ok(await _feedService.GetFeedItemsAsync(id));
    }

    [HttpPost("/feeds/{id}/refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Refresh(int id)
    {
        return Ok(await _feedService.RefreshFeedAsync(id));
    }
}
