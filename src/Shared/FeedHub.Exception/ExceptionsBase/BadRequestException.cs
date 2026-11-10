using System.Net;

namespace FeedHub.Exception.ExceptionsBase;

public class BadRequestException : FeedHubException
{
    private readonly List<string> _errors;
    public BadRequestException(List<string> errorMessages)
    {
        _errors = errorMessages;
    }

    public override List<string> GetErrorMessages() => _errors;

    public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadGateway;
}
