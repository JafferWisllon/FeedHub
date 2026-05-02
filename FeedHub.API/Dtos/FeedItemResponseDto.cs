using FeedHub.API.Models;

namespace FeedHub.API.Dtos;

public class FeedItemResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Link { get; set; }
    public DateTime? PublishAt { get; set; }

    public static List<FeedItemResponseDto> FromEntity(List<FeedItem> items)
    {
        var response = new List<FeedItemResponseDto>();
        foreach (var item in items)
        {
            response.Add(new FeedItemResponseDto
            {
                Id = item.Id,
                Title = item.Title,
                Link = item.Link,
                PublishAt = item.PublishAt,
            });
        }
        return response;
    }
}
