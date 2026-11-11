using FeedHub.Application.UseCases.Feed.Create;
using FeedHub.Communication.Requests;
using FeedHub.Domain.Repositories;
using FeedHub.Exception.ExceptionsBase;
using NSubstitute;
using Shouldly;

namespace UseCases.Tests.Feed.Create;

public class CreateFeedUseCaseTests
{
    private readonly IFeedRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly CreateFeedUseCase _sut;

    public CreateFeedUseCaseTests()
    {
        _repository = Substitute.For<IFeedRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();

        _sut = new CreateFeedUseCase(_repository, _unitOfWork);
    }

    [Fact]
    public async Task Should_Returns_An_Error_When_AddSync_Receive_An_Invalid_Url()
    {
        // Arrange
        var request = new RequestCreateFeed
        {
            Name = "feed_name",
            Url = "invalid-url"
        };

        // Act
        var exception = await Should.ThrowAsync<InvalidFeedUrlException>(async () =>
        {
            await _sut.Execute(request);
        });

        // Assert
        var errorMessages = exception.GetErrorMessages().First();
        errorMessages.ShouldBe("The Url field is not a valid fully-qualified http, https URL");
    }

    [Fact]
    public async Task Should_Return_An_Error_When_AddSync_With_Feed_Already_Exists()
    {
        // Arrange
        var request = new RequestCreateFeed
        {
            Name = "feed_name",
            Url = "https://feed.com"
        };

        _repository.FindByUrlAsync(Arg.Any<string>()).Returns(new FeedHub.Domain.Models.Feed { Id = 10 });

        // Act
        var exception = await Should.ThrowAsync<AlreadyExistsException>(async () =>
        {
            await _sut.Execute(request);
        });

        // Assert
        var errorMessages = exception.GetErrorMessages().First();
        errorMessages.ShouldBe("Feed already exists");
    }

    [Fact]
    public async Task Should_Be_Able_Create_New_Feed_When_AddSync()
    {
        // Arrange
        var request = new RequestCreateFeed
        {
            Name = "feed_name",
            Url = "https://myfeed.com"
        };

        // Act
        var feed = await _sut.Execute(request);

        // Assert
        feed.Url.ShouldBe("https://myfeed.com");
    }
}
