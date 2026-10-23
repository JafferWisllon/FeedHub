using FeedHub.API.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FeedHub.API.Middlewares;

public class ApiExceptionMiddleware
{
    private readonly RequestDelegate _next;
    public ApiExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var status = ex switch
            {
                InvalidFeedUrlException => StatusCodes.Status400BadRequest,
                FeedNotFoundException => StatusCodes.Status404NotFound,
                AlreadyExistsException => StatusCodes.Status409Conflict,
                FeedFetchException => StatusCodes.Status502BadGateway,
                BadRequestException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            
            context.Response.StatusCode = status;

            var problemDetails = new ProblemDetails
            {
                Type = ex.GetType().Name,
                Title = "An error occurred",
                Detail = ex.Message,
                Status = status
            };

            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
