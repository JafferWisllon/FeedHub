using CodeHollow.FeedReader;
using FeedHub.Application.Exceptions;
using FeedHub.Application.Mappings;
using FeedItemEntity = FeedHub.Domain.Models.FeedItem;

namespace FeedHub.Application.Services;

public class RssService : IRssService
{
    public async Task<IList<FeedItemEntity>> GetFeedItemsAsync(string url, int feedId)
    {
        try
        {
            var feedReader = await FeedReader.ReadAsync(url);
            return FeedItemRssMappings.ToEntity(feedReader.Items, feedId);
        }
        catch (Exception e)
        {
            throw new FeedFetchException("Error to get Feed RSS", e);
        }
    }
}
