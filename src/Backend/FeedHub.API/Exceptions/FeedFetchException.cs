namespace FeedHub.API.Exceptions
{
    public class FeedFetchException : Exception
    {
        public FeedFetchException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
