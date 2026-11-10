using FeedHub.Application.Dtos;

namespace FeedHub.Application.UseCases.Feed.Create;

public interface ICreateFeedUseCase
{
    Task<Domain.Models.Feed> Execute(CreateFeedDto request);
}
