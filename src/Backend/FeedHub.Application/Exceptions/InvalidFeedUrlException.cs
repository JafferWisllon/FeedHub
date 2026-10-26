namespace FeedHub.Application.Exceptions;

public class InvalidFeedUrlException : Exception
{
    public InvalidFeedUrlException(string message) : base(message)
    {
    }
}
