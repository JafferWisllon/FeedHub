using FeedHub.Application.UseCases.Feed.Refresh;
using FeedHub.Domain.Repositories;
using FeedHub.Domain.Rss;
using FeedHub.Exception.ExceptionsBase;
using NSubstitute;
using Shouldly;

namespace UseCases.Tests.Feed.Refresh;

public class RefreshFeedUseCaseTests
{
    private readonly RefreshFeedUseCase _sut;
    private readonly IFeedRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRssService _rssService;

    public RefreshFeedUseCaseTests()
    {
        _repository = Substitute.For<IFeedRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _rssService = Substitute.For<IRssService>();

        _sut = new RefreshFeedUseCase(_repository, _unitOfWork, _rssService);
    }

    [Fact]
    public async Task Should_Return_Error_Refresh_Invalid_Feed()
    {
        //Arrange
        var id = 10;

        // Act
        var exception = await Should.ThrowAsync<FeedNotFoundException>(async () =>
        {
            await _sut.Execute(id);
        });

        // Assert
        var errorMessages = exception.GetErrorMessages().First();
        errorMessages.ShouldBe($"Feed with id {id} not found");
    }
}
