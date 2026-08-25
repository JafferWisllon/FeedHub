using FeedHub.API.Validators;

namespace FeedHub.Tests.Validators;

public class UrlValidatorTests
{

    [Fact]
    public void Should_Returns_False_When_Given_Empty_String()
    {
        var result = UrlValidator.ValidateUrl("");
        Assert.False(result);
    }

    [Fact]
    public void Should_Returns_False_When_Given_Invalid_Url()
    {
        var result = UrlValidator.ValidateUrl("invalid-url");
        Assert.False(result);
    }
    
    [Fact]
    public void Should_Returns_True_When_Given_Https_Url()
    {
        var result = UrlValidator.ValidateUrl("https://google.com");
        Assert.True(result);
    }
}