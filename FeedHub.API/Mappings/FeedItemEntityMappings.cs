using FeedHub.API.Dtos;
using FeedHub.API.Models;

namespace FeedHub.API.Mappings;

public class FeedItemEntityMappings
{
    public static List<FeedItemResponseDto> ToDto(List<FeedItem> items)
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
