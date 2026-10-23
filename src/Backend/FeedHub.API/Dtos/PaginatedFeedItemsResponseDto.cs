namespace FeedHub.API.Dtos;

public class PaginatedFeedItemsResponseDto
{
    public PaginatedFeedItemsResponseDto(
        int page, 
        int pageSize, 
        int totalCount, 
        int totalPages, 
        string? nextPage, 
        List<FeedItemResponseDto> items)
    {
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = totalPages;
        NextPage = nextPage;
        Items = items;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public string? NextPage { get; set; }
    public List<FeedItemResponseDto> Items { get; set; }
  
}
