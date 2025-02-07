namespace VTools.Models.EnVrac;

public class Article
{
    private Article(string? title, string? description, List<string> urls, Category category)
    {
        Title = title;
        Description = description;
        Urls = urls;
        Category = category;
    }

    public string? Title { get; private set; }
    public string? Description { get; private set; }
    public List<string> Urls { get; private set; }
    public Category Category { get; private set; }

    public static Article Create(string? title, string? description, List<string> urls, Category category) =>
        new(title, description, urls, category);

    public void Update(string? title, string? description, List<string> urls, Category? category)
    {
        if (title is not null)
        {
            Title = title;
        }

        Urls = urls;

        Description = description ?? string.Empty;

        if (category is not null)
        {
            Category = (Category)category;
        }
    }

    public void AddNewUrl(string newUrl)
    {
        Urls.Add(newUrl);
    }

    public void DeleteNewUrl(string url)
    {
        Urls.Remove(url);
    }
}