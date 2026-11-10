using CodeHollow.FeedReader;
using FeedHub.Domain.Rss;

namespace FeedHub.Infrastructure.Rss;

public class RssService : IRssService
{
    public async Task<IList<Domain.Models.FeedItem>> GetFeedItemsAsync(string url, int feedId)
    {
        try
        {
            var feedReader = await FeedReader.ReadAsync(url);
            return ToEntity(feedReader.Items, feedId);
        }
        catch (Exception e)
        {
            throw new Exception("Error to get Feed RSS", e);
        }
    }

    private List<Domain.Models.FeedItem> ToEntity(IList<CodeHollow.FeedReader.FeedItem> feedItems, int feedId)
    {
        var response = new List<Domain.Models.FeedItem>();

        foreach (var item in feedItems)
        {
            if (string.IsNullOrEmpty(item.Title) || string.IsNullOrEmpty(item.Link))
                continue;

            response.Add(new Domain.Models.FeedItem
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
