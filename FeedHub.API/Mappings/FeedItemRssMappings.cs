using FeedHub.API.Dtos;
using FeedReaderItem = CodeHollow.FeedReader.FeedItem;

namespace FeedHub.API.Mappings;

public class FeedItemRssMappings
{
    public static List<FeedItemResponseDto> ToDto(IList<FeedReaderItem> feedItems)
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
