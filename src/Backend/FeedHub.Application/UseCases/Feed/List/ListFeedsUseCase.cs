using FeedHub.Domain.Repositories;

namespace FeedHub.Application.UseCases.Feed.List;

public class ListFeedsUseCase : IListFeedsUseCase
{
    private readonly IFeedRepository _repository;

    public ListFeedsUseCase(IFeedRepository repository) 
        => _repository = repository;

    public async Task<IEnumerable<Domain.Models.Feed>> Execute() 
        => await _repository.ListAsync();
}
