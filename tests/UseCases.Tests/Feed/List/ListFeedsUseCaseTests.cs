using FeedHub.Application.UseCases.Feed.List;
using FeedHub.Domain.Repositories;
using NSubstitute;

namespace UseCases.Tests.Feed.List;

public class ListFeedsUseCaseTests
{
    private readonly IFeedRepository _feedRepository;
    private readonly ListFeedsUseCase _sut;

    public ListFeedsUseCaseTests()
    {
        _feedRepository = Substitute.For<IFeedRepository>();
        _sut = new ListFeedsUseCase(_feedRepository);
    }

    [Fact]
    public async Task Should_Return_A_Feed_List()
    {
        // Act
        var result = await _sut.Execute();

        // Assert 
        result.OfType<IEnumerable<FeedHub.Domain.Models.Feed>>();
    }
}
