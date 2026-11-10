using System.Net;

namespace FeedHub.Exception.ExceptionsBase;

public abstract class FeedHubException : System.Exception
{
    public abstract HttpStatusCode GetStatusCode();
    public abstract List<string> GetErrorMessages();
}
