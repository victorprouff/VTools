namespace VTools.Models.EnVrac;

public class Article
{
    private Article(string? title, string? description, Link[] urls, Category category)
    {
        Title = title;
        Description = description;
        Urls = urls;
        Category = category;
    }

    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public Link[] Urls { get; private set; }
    public Category Category { get; private set; }

    public static Article Create(string? title, string? description, Link[] urls, Category category) =>
        new(title, description, urls, category);

}

public record Link(string Url, string Title = "Source")
{
    public string ToMarkdown() => $"[{Title}]({Url})";
}