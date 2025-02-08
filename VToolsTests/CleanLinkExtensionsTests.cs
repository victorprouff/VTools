using VTools.Models.EnVrac;

namespace VToolsTests;

public class CleanLinkExtensionsTests
{
    [Fact]
    public void ExtractRegexPattern_InputIsNull_ReturnsEmptyArray()
    {
        // Arrange
        string? input = null;
        var pattern = @"\[(.*?)\]\((.*?)\)";

        // Act
        var result = CleanLinkExtensions.ExtractRegexPattern(input, pattern);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ExtractRegexPattern_InputIsEmpty_ReturnsEmptyArray()
    {
        // Arrange
        var input = string.Empty;
        var pattern = @"\[(.*?)\]\((.*?)\)";

        // Act
        var result = CleanLinkExtensions.ExtractRegexPattern(input, pattern);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ExtractRegexPattern_ValidMarkdownLinks_ReturnsExtractedLinks()
    {
        // Arrange
        var input = "Click [here](https://example.com) for more details.";
        var pattern = @"\[(.*?)\]\((.*?)\)";

        // Act
        var result = CleanLinkExtensions.ExtractRegexPattern(input, pattern);

        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal("https://example.com", result[0].Url);
        Assert.Equal("here", result[0].Title);
    }

    [Fact]
    public void ExtractRegexPattern_MultipleLinks_ReturnsAllLinks()
    {
        // Arrange
        var input = "Check [Google](https://google.com) and [Bing](https://bing.com).";
        var pattern = @"\[(.*?)\]\((.*?)\)";

        // Act
        var result = CleanLinkExtensions.ExtractRegexPattern(input, pattern);

        // Assert
        Assert.Equal(2, result.Length);
        Assert.Equal("https://google.com", result[0].Url);
        Assert.Equal("Google", result[0].Title);
        Assert.Equal("https://bing.com", result[1].Url);
        Assert.Equal("Bing", result[1].Title);
    }

    [Fact]
    public void ExtractRegexPattern_InvalidLinks_ReturnsEmptyArray()
    {
        // Arrange
        var input = "This text does not contain valid links.";
        var pattern = @"\[(.*?)\]\((.*?)\)";

        // Act
        var result = CleanLinkExtensions.ExtractRegexPattern(input, pattern);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void ExtractRegexPattern_ValidLinksWithoutTitles_ReturnsLinksWithDefaultTitle()
    {
        // Arrange
        var input = "Visit [this](https://example.com) and http://example2.com.";
        var pattern = @"(?:http[s]?:\/\/.)?(?:www\.)?[-a-zA-Z0-9@%._\+~#=]{2,256}\.[a-z]{2,6}\b(?:[-a-zA-Z0-9@:%_\+.~#?&\/\/=]*)";

        // Act
        var result = CleanLinkExtensions.ExtractRegexPattern(input, pattern);

        // Assert
        Assert.NotEmpty(result);
        Assert.Equal(2, result.Length);
        Assert.Equal("https://example.com", result[0].Url);
        Assert.Equal("Source", result[0].Title); // Default Title
        Assert.Equal("http://example2.com", result[1].Url);
        Assert.Equal("Source", result[1].Title); // Default Title
    }

    [Fact]
    public void ExtractRegexPattern_LinksWithOnlyUrls_ReturnsUrlInTitle()
    {
        // Arrange
        var input = "Here is a link: https://example.com";
        var pattern = @"(?:http[s]?:\/\/.)?(?:www\.)?[-a-zA-Z0-9@%._\+~#=]{2,256}\.[a-z]{2,6}\b(?:[-a-zA-Z0-9@:%_\+.~#?&\/\/=]*)";

        // Act
        var result = CleanLinkExtensions.ExtractRegexPattern(input, pattern);

        // Assert
        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Equal("https://example.com", result[0].Url);
        Assert.Equal("Source", result[0].Title); // Default Title
    }
}