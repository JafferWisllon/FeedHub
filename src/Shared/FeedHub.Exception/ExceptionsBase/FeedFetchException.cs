using System.Net;

namespace FeedHub.Exception.ExceptionsBase;

public class FeedFetchException : FeedHubException
{
    private readonly string _errors;
    public FeedFetchException(string message)
        => _errors = message;

    public override List<string> GetErrorMessages() => [_errors];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadGateway;
}
