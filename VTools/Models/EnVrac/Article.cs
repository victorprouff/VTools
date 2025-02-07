using NodaTime;

namespace VTools.Models.EnVrac;

public class Article
{
    private Article(Guid id, string title, string description, List<string> urls, Category category, Instant createdAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Urls = urls;
        Category = category;
        CreatedAt = createdAt;
    }

    public Guid Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public List<string> Urls { get; private set; }
    public Category Category { get; private set; }
    public Instant CreatedAt { get; }

    public static Article Create(string title, string description, List<string> urls, Category category) =>
        new(Guid.NewGuid(), title, description, urls, category, SystemClock.Instance.GetCurrentInstant());

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