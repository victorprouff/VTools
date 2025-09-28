using NodaTime;

namespace VTools.BookEntity.Models;

public record UpdateBookCommand(Guid Id, string Title, string Author, string Comment, bool IsReading, Instant? EndReadingDate);