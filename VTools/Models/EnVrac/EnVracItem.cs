using NodaTime;

namespace VTools.Models.EnVrac;

public class EnVracItem
{
    public EnVracItem(Guid id, string title, string description, List<Article> articles, Instant createdAt, Instant publishAt)
    {
        Id = id;
        Title = title;
        Description = description;
        Articles = articles;
        CreatedAt = createdAt;
        PublishAt = publishAt;
    }

    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public List<Article> Articles { get; init; }
    public Instant CreatedAt { get; init; }
    public Instant PublishAt { get; init; }
}