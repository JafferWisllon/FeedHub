using FeedHub.API.Dtos;
using FeedHub.API.Exceptions;
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
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Create([FromBody] CreateFeedDto request)
    {
        try
        {
            var feed = await _feedService.AddAsync(request);
            return CreatedAtAction(nameof(Get), new { id = feed.Id }, feed);
        }
        catch (InvalidFeedUrlException e)
        {
            return ValidationProblem(e.Message, statusCode: StatusCodes.Status400BadRequest);
        }
        catch (AlreadyExistsException e)
        {
            return Conflict(new { message = e.Message });
        }        
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

    [HttpGet("{id:int}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> GetItems(int id)
    {
        try
        {
            return Ok(await _feedService.GetFeedItemsAsync(id));
        }
        catch (FeedNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }        
    }

    [HttpPost("/feeds/{id}/refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Refresh(int id)
    {
        try
        {
            var feeds = await _feedService.RefreshFeedAsync(id);

            return Ok(feeds);

        }
        catch (FeedNotFoundException e)
        {
            return NotFound(new { message = e.Message });
        }
        catch(FeedFetchException e)
        {
            return new ObjectResult(new { message = e.Message, detail = e.InnerException?.Message })
            {
                StatusCode = StatusCodes.Status502BadGateway
            };
        }
    }
}
