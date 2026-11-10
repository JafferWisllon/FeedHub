namespace FeedHub.Application.UseCases.Feed.List;

public interface IListFeedsUseCase
{
    Task<IEnumerable<Domain.Models.Feed>> Execute();
}
