using NodaTime;

namespace VTools.BookEntity.Models;

public record CreateBookCommand(string Title, string Author, string Comment, bool IsReading, Instant? EndReadingDate);