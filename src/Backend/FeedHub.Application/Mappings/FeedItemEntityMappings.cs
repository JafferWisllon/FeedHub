using FeedHub.Application.Dtos;
using FeedHub.Domain.Models;

namespace FeedHub.Application.Mappings;

public class FeedItemEntityMappings
{
    public static List<FeedItemResponseDto> ToDto(IList<FeedItem> items)
    {
        var response = new List<FeedItemResponseDto>();
        foreach (var item in items)
        {
            response.Add(new FeedItemResponseDto
            {
                Id = item.Id,
                Title = item.Title,
                Link = item.Link,
                PublishAt = item.PublishAt
            });
        }
        return response;
    }
}
