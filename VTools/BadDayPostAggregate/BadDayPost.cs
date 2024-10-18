using NodaTime;

namespace VTools.BadDayPostAggregate;

public class BadDayPost
{
    public BadDayPost(Guid id, string url, string instagramId, Instant createdAt, Instant? updatedAt = null)
    {
        Id = id;
        Url = url;
        InstagramId = instagramId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; private set; }
    public string Url { get; private set; }
    public string InstagramId { get; private set; }
    public Instant CreatedAt { get; private set; }
    public Instant? UpdatedAt { get; private set; }

    public static BadDayPost Create(string url, string instagramId, Instant startedAt) =>
        new(
            Guid.NewGuid(),
            url,
            instagramId,
            startedAt);
}