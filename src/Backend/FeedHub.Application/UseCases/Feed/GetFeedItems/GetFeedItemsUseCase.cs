using FeedHub.Application.Dtos;
using FeedHub.Application.Mappings;
using FeedHub.Application.UseCases.Feed.GetById;
using FeedHub.Domain.Repositories;
using FeedHub.Exception.ExceptionsBase;

namespace FeedHub.Application.UseCases.Feed.GetFeedItems;

public class GetFeedItemsUseCase : IGetFeedItemsUseCase
{
    private readonly IFeedRepository _repository;
    private readonly IGetFeedByIdUseCase _getFeedByIdUseCase;
    public GetFeedItemsUseCase(IFeedRepository repository, IGetFeedByIdUseCase getFeedByIdUseCase)
    {
        _repository = repository;
        _getFeedByIdUseCase = getFeedByIdUseCase;
    }

    public async Task<PaginatedFeedItemsResponseDto> Execute(int id, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
            throw new BadRequestException(["Invalid query params"]);

        var feed = await _getFeedByIdUseCase.Execute(id);

        var totalCount = await _repository.GetFeedItemsCount(feed.Id);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var offset = (page - 1) * pageSize;

        var items = await _repository.GetFeedItems(feed.Id, offset, pageSize);

        var nextPage = NextPage(totalPages, page, pageSize, id);

        return new PaginatedFeedItemsResponseDto(page, pageSize, totalCount, totalPages, nextPage, FeedItemEntityMappings.ToDto(items));
    }

    private string? NextPage(int totalPages, int page, int pageSize, int feedId)
    {
        if (page >= totalPages)
            return null;

        return $"GET /feeds/{feedId}/items?page={page + 1}&pageSize={pageSize}";
    }
}
