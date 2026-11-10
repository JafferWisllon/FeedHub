using FeedHub.Application.Dtos;
using FeedHub.Application.Exceptions;
using FeedHub.Application.UseCases.Feed.GetById;
using FeedHub.Application.UseCases.Feed.GetFeedItems;
using FeedHub.Domain.Models;
using FeedHub.Domain.Repositories;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Shouldly;

namespace UseCases.Tests.Feed.GetFeedItems;

public class GetFeedItemsUseCaseTests
{
    private readonly GetFeedItemsUseCase _sut;
    private readonly IFeedRepository _repository;
    private readonly IGetFeedByIdUseCase _getFeedByIdUseCase;


    public GetFeedItemsUseCaseTests()
    {
        _repository = Substitute.For<IFeedRepository>();
        _getFeedByIdUseCase = Substitute.For<IGetFeedByIdUseCase>();
        _sut = new GetFeedItemsUseCase(_repository, _getFeedByIdUseCase);
    }

    [Fact]
    public async Task Should_Return_Error_GetFeedItems_With_Invalid_QueryParams()
    {
        //Arrange
        var id = 1;
        var page = 1;
        var size = 0;

        // Act
        var exception = await Should.ThrowAsync<BadRequestException>(async () =>
        {
            await _sut.Execute(id, page, size);
        });

        // Assert
        exception.Message.ShouldBe("Invalid query params");
    }

    [Fact]
    public async Task Should_Return_Error_GetFeedItems_With_Invalid_FeedId()
    {
        //Arrange
        var id = 100;
        var page = 1;
        var size = 1;

        _getFeedByIdUseCase
            .Execute(Arg.Any<int>())
            .ThrowsAsync(new FeedNotFoundException($"Feed with id {id} not found"));

        // Act
        var exception = await Should.ThrowAsync<FeedNotFoundException>(async () =>
        {
            await _sut.Execute(id, page, size);
        });

        // Assert
        exception.Message.ShouldBe($"Feed with id {id} not found");
    }

    [Fact]
    public async Task Should_Return_With_Success_GetFeedItems()
    {
        //Arrange
        var id = 1;
        var page = 1;
        var size = 1;

        _getFeedByIdUseCase.Execute(Arg.Any<int>())
            .Returns(new FeedHub.Domain.Models.Feed { Id = id, FeedItems = new List<FeedItem> { new FeedItem { Id = 1 } } });

        _repository.GetFeedItems(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<int>())
            .Returns(new List<FeedItem> { new FeedItem { Id = 1} });

        // Act
        var result = await _sut.Execute(id, page, size);

        // Assert
        result.ShouldBeOfType<PaginatedFeedItemsResponseDto>();
        result.Page.ShouldBe(page);
        result.Items.FirstOrDefault()?.Id.ShouldBe(id);
    }
}
