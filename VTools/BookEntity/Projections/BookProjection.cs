using NodaTime;

namespace VTools.BookEntity.Projections;

public record BookProjection(
    Guid Id,
    string Title,
    string Author,
    bool IsReading,
    string Comment,
    Instant? EndReadingDate,
    Instant CreatedAt,
    Instant? UpdatedAt);