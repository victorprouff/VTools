using NodaTime;

namespace VTools.Data.Entities;

public class BadDayPostEntity
{
    public BadDayPostEntity()
    {
    }

    public BadDayPostEntity(Guid id, string url, string instagramId, Instant createdAt, Instant? updatedAt)
    {
        Id = id;
        Url = url;
        InstagramId = instagramId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; set; }
    public string Url { get; set; }
    public string InstagramId { get; set; }
    public Instant CreatedAt { get; set; }
    public Instant? UpdatedAt { get; set; }
}