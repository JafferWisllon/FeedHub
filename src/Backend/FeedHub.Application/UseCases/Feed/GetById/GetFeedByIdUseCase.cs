using FeedHub.Domain.Repositories;
using FeedHub.Exception.ExceptionsBase;

namespace FeedHub.Application.UseCases.Feed.GetById;

public class GetFeedByIdUseCase : IGetFeedByIdUseCase
{
    private readonly IFeedRepository _repository;

    public GetFeedByIdUseCase(IFeedRepository repository) 
        => _repository = repository;

    public async Task<Domain.Models.Feed> Execute(int id)
    {
        var feed = await _repository.FindByIdAsync(id);
        if (feed is null)
            throw new FeedNotFoundException($"Feed with id {id} not found");

        return feed;
    }
}
