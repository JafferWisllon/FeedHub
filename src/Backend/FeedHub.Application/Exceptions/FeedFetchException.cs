namespace FeedHub.Application.Exceptions;

public class FeedFetchException : Exception
{
    public FeedFetchException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
