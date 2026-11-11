using FeedHub.Communication.Requests;

namespace FeedHub.Application.UseCases.Feed.Create;

public interface ICreateFeedUseCase
{
    Task<Domain.Models.Feed> Execute(RequestCreateFeed request);
}
