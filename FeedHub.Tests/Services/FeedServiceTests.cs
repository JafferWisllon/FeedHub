using FeedHub.API.Data.Context;
using FeedHub.API.Dtos;
using FeedHub.API.Exceptions;
using FeedHub.API.Models;
using FeedHub.API.Services;
using FeedHub.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;

namespace FeedHub.Tests.Services;

public class FeedServiceTests
{
    private readonly DbContextOptions<ApiDbContext> _dbContextOptions;
    private readonly ApiDbContext _context;
    private readonly IRssService _rssService;
    private readonly FeedService _sut;

    public FeedServiceTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: "FeedHubDb")
            .Options;

        _context = new ApiDbContext(_dbContextOptions);
        _context.Feeds.Add(new Feed() { Name = "feed", Url = "https://feed.com" });
        _context.SaveChangesAsync();
        
        _rssService = Substitute.For<IRssService>();
        _sut = new FeedService(_context, _rssService);
    }

    [Fact]
    public async Task Should_Returns_An_Error_When_AddSync_Receive_An_Invalid_Url()
    {
        // Arrange
        var request = new CreateFeedDto
        {
            Name = "feed_name",
            Url = "invalid-url"
        };
        
        // Act
        var exception = await Should.ThrowAsync<InvalidFeedUrlException>(async () =>
        {
            await _sut.AddAsync(request);
        });
        
        // Assert
        exception.Message.ShouldBe("The Url field is not a valid fully-qualified http, https URL");
    }

    [Fact]
    public async Task Should_Return_An_Error_When_AddSync_With_Feed_Already_Exists()
    {
        // Arrange
        var request = new CreateFeedDto
        {
            Name = "feed_name",
            Url = "https://feed.com"
        };
        
        // Act
        var exception = await Should.ThrowAsync<AlreadyExistsException>(async () =>
        {
            await _sut.AddAsync(request);
        });
        
        // Assert
        exception.Message.ShouldBe("Feed already exists");
    }
    
    [Fact]
    public async Task Should_Be_Able_Create_New_Feed_When_AddSync()
    {
        // Arrange
        var request = new CreateFeedDto
        {
            Name = "feed_name",
            Url = "https://myfeed.com"
        };
        
        // Act
        var feed = await _sut.AddAsync(request);
        
        // Assert
        feed.Url.ShouldBe("https://myfeed.com");
    }

    [Fact]
    public async Task Should_Return_An_Error_When_Feed_Dont_Exists_GetById()
    {
        var invalidFeedId = 100;
        
        // Act
        var exception = await Should.ThrowAsync<FeedNotFoundException>(async () =>
        {
            await _sut.GetById(invalidFeedId);
        });
        
        // Assert 
        exception.Message.ShouldBe($"Feed with id {invalidFeedId} not found");
    }
    
    [Fact]
    public async Task Should_Return_A_Feed_GetById()
    {
        var validFeedId = 1;
        
        // Act
        var result = await _sut.GetById(validFeedId);
        
        // Assert 
        result.Id.ShouldBe(validFeedId);
    }
    
    [Fact]
    public async Task Should_Return_A_Feed_List()
    {
        var validFeedId = 1;
        
        // Act
        var result = await _sut.ListAsync();
        
        // Assert 
        result.OfType<IEnumerable<Feed>>();
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
           await _sut.GetFeedItemsAsync(id, page, size);
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
        
        // Act
        var exception = await Should.ThrowAsync<FeedNotFoundException>(async () =>
        {
            await _sut.GetFeedItemsAsync(id, page, size);
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
        
        // Act
        var result = await _sut.GetFeedItemsAsync(id, page, size);
        
        // Assert
        result.ShouldBeOfType<PaginatedFeedItemsResponseDto>();
        result.Page.ShouldBe(page);
        result.Items.FirstOrDefault()?.Id.ShouldBe(id);
    }
    
    [Fact]
    public async Task Should_Return_Error_Refresh_Invalid_Feed()
    {
        //Arrange
        var id = 10;
        
        // Act
        var exception = await Should.ThrowAsync<FeedNotFoundException>(async () =>
        {
            await _sut.RefreshFeedAsync(id);
        });
        
        // Assert
        exception.Message.ShouldBe($"Feed with id {id} not found");
    }
}