using FeedHub.Application.Dtos;
using FeedHub.Application.UseCases.Feed.Create;
using FeedHub.Application.UseCases.Feed.GetById;
using FeedHub.Application.UseCases.Feed.GetFeedItems;
using FeedHub.Application.UseCases.Feed.List;
using FeedHub.Application.UseCases.Feed.Refresh;
using FeedHub.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace FeedHub.API.Controllers;

[ApiController]
[Route("api/feeds")]
public class FeedsController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Create([FromServices] ICreateFeedUseCase useCase, 
        [FromBody] RequestCreateFeed request)
    {
        var feed = await useCase.Execute(request);
        return CreatedAtAction(nameof(Get), new { id = feed.Id }, feed);  
    }

    [HttpGet]
    public async Task<ActionResult> List([FromServices] IListFeedsUseCase useCase) 
        => Ok(await useCase.Execute());

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Get([FromServices] IGetFeedByIdUseCase useCase, int id) 
        => Ok(await useCase.Execute(id));

    [HttpGet("{id:int}/items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary), StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> GetItems([FromServices] IGetFeedItemsUseCase useCase, int id, [FromQuery] GetItemsQueryParams queryParams) 
        => Ok(await useCase.Execute(id, queryParams.Page, queryParams.PageSize));

    [HttpPost("/feeds/{id}/refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> Refresh([FromServices] IRefreshFeedUseCase useCase, int id) 
        => Ok(await useCase.Execute(id));
}
