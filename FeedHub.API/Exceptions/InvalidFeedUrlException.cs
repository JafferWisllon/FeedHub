namespace FeedHub.API.Exceptions;

public class InvalidFeedUrlException : Exception
{
    public InvalidFeedUrlException(string message) : base(message)
    {
    }
}
