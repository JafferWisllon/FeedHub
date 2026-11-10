using FeedHub.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FeedHub.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is FeedHubException feedHubException)
        {
            context.HttpContext.Response.StatusCode = (int)feedHubException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorJson(feedHubException.GetErrorMessages()));
        }
        else
        {
            context.HttpContext.Response.StatusCode = (int)StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorJson("Internal Server error"));
        }
    }
}

public class ResponseErrorJson
{
    public List<string> Errors { get; private set; }

    public ResponseErrorJson(List<string> errorMessages) => Errors = errorMessages;

    public ResponseErrorJson(string errorMessage) => Errors = [errorMessage];
}
