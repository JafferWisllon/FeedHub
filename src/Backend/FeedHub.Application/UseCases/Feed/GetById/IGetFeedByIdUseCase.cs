namespace FeedHub.Application.UseCases.Feed.GetById;

public interface IGetFeedByIdUseCase
{
    Task<Domain.Models.Feed> Execute(int id);
}
