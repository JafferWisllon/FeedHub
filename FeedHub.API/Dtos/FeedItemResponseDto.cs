namespace FeedHub.API.Dtos;

public class FeedItemResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Link { get; set; }
    public DateTime? PublishAt { get; set; }
}
