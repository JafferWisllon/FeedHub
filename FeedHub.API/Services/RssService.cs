using CodeHollow.FeedReader;
using FeedHub.API.Dtos;
using FeedHub.API.Exceptions;
using FeedHub.API.Mappings;
using FeedHub.API.Services.Interfaces;
using FeedItemEntity = FeedHub.API.Models.FeedItem;

namespace FeedHub.API.Services;

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
