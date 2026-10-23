using FeedHub.API.Dtos;
using FeedHub.API.Models;
using FeedReaderItem = CodeHollow.FeedReader.FeedItem;

namespace FeedHub.API.Mappings;

public class FeedItemRssMappings
{
    public static List<FeedItem> ToEntity(IList<FeedReaderItem> feedItems, int feedId)
    {
        var response = new List<FeedItem>();

        foreach (var item in feedItems)
        {
            if (string.IsNullOrEmpty(item.Title) || string.IsNullOrEmpty(item.Link))
                continue;

            response.Add(new FeedItem
            {
                Link = item.Link,
                Title = item.Title,
                PublishAt = item.PublishingDate,
                FeedId = feedId
            });
        }
        return response;
    }
}
