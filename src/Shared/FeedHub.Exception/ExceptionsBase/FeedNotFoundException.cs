using System.Net;

namespace FeedHub.Exception.ExceptionsBase;

public class FeedNotFoundException : FeedHubException
{
    private readonly string _errors;
    public FeedNotFoundException(string message)
    {
        _errors = message;
    }

    public override List<string> GetErrorMessages() => [_errors];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
}
