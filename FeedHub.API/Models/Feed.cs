impnamespace FeedHub.API.Models;

public class Feed
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
