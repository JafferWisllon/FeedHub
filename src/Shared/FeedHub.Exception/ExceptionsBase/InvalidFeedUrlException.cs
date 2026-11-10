using System.Net;

namespace FeedHub.Exception.ExceptionsBase;

public class InvalidFeedUrlException : FeedHubException
{
    private readonly string _error;
    public InvalidFeedUrlException(string message)
        => _error = message;

    public override List<string> GetErrorMessages() => [_error];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
}
