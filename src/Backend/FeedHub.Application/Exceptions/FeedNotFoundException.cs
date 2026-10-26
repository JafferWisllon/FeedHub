namespace FeedHub.Application.Exceptions;

public class FeedNotFoundException : Exception
{
    public FeedNotFoundException(string message) : base(message)
    {
    }
}
