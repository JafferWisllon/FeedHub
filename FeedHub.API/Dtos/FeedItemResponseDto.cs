using FeedHub.API.Models;
using FeedReaderItem = CodeHollow.FeedReader.FeedItem;

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

    public static List<FeedItemResponseDto> FromFeedReader(IList<FeedReaderItem> feedItems)
    {
        var response = new List<FeedItemResponseDto>();

        foreach (var item in feedItems)
        {
            if (string.IsNullOrEmpty(item.Title) || string.IsNullOrEmpty(item.Link))
                continue;

            response.Add(new FeedItemResponseDto
            {
                Link = item.Link,
                Title = item.Title,
                PublishAt = item.PublishingDate
            });
        }
        return response;
    }
}
