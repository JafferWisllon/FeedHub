namespace FeedHub.API.Dtos;

public class RefreshFeedDto
{
    public int TotalFetched { get; set; }
    public int NewItems { get; set; }
    public int Duplicates { get; set; }

    public RefreshFeedDto(int totalFetched, int newItems)
    {
        TotalFetched = totalFetched;
        NewItems = newItems;
        Duplicates = TotalFetched - newItems;
    }
}
