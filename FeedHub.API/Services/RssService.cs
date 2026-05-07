using CodeHollow.FeedReader;
using FeedHub.API.Dtos;
using FeedHub.API.Exceptions;
using FeedHub.API.Mappings;
using FeedHub.API.Services.Interfaces;

namespace FeedHub.API.Services;

public class RssService : IRssService
{
    public async Task<IList<FeedItemResponseDto>> GetFeedItemsAsync(string url)
    {
        try
        {
            var feedReader = await FeedReader.ReadAsync(url);
            return FeedItemRssMappings.ToDto(feedReader.Items);
        }
        catch (Exception e)
        {
            throw new FeedFetchException("Error to get Feed RSS", e);
        }
    }
}
