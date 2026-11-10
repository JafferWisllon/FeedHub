using FeedHub.Application.Exceptions;
using FeedHub.Application.UseCases.Feed.GetById;
using FeedHub.Domain.Repositories;
using NSubstitute;
using Shouldly;

namespace UseCases.Tests.Feed.GetById;

public class GetFeedByIdUseCaseTests
{
    private readonly GetFeedByIdUseCase _sut;
    private readonly IFeedRepository _repository;

    public GetFeedByIdUseCaseTests()
    {
        _repository = Substitute.For<IFeedRepository>();

        _sut = new GetFeedByIdUseCase(_repository);
    }

    [Fact]
    public async Task Should_Return_An_Error_When_Feed_Dont_Exists_GetById()
    {
        var invalidFeedId = 100;

        // Act
        var exception = await Should.ThrowAsync<FeedNotFoundException>(async () =>
        {
            await _sut.Execute(invalidFeedId);
        });

        // Assert 
        exception.Message.ShouldBe($"Feed with id {invalidFeedId} not found");
    }

    [Fact]
    public async Task Should_Return_A_Feed_GetById()
    {
        var validFeedId = 1;

        _repository.FindByIdAsync(Arg.Any<int>())
            .Returns(new FeedHub.Domain.Models.Feed { Id = validFeedId });

        // Act
        var result = await _sut.Execute(validFeedId);

        // Assert 
        result.Id.ShouldBe(validFeedId);
    }
}
