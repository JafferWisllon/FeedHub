using System.Net;

namespace FeedHub.Exception.ExceptionsBase;

public class AlreadyExistsException : FeedHubException
{
    private readonly string _errors;
    public AlreadyExistsException(string message) 
        => _errors = message;

    public override List<string> GetErrorMessages() => [_errors];

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Conflict;
}
