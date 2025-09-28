using NodaTime;

namespace VTools.BookEntity;

public class Book
{
    public Book(
        Guid id,
        string title,
        string author,
        string comment,
        bool isReading,
        Instant? endReadingDate,
        Instant createdAt,
        Instant? updatedAt)
    {
        Id = id;
        Title = title;
        Author = author;
        Comment = comment;
        IsReading = isReading;
        EndReadingDate = endReadingDate;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public Guid Id { get; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsReading { get; set; }
    public string Comment { get; set; }
    public Instant? EndReadingDate { get; set; }
    public Instant CreatedAt { get; private set; }
    public Instant? UpdatedAt { get; private set; }

    public static Book Create(string title, string author, string comment, bool isReading, Instant? endReadingDate, Instant createdAt)
        => new(Guid.NewGuid(), title, author, comment, isReading, endReadingDate, createdAt, null);

    public void Update(string title, string author, string comment, bool isReading, Instant? endReadingDate, Instant updatedAt)
    {
        Title = title;
        Author = author;
        Comment = comment;
        IsReading = isReading;
        EndReadingDate = endReadingDate;
        UpdatedAt = updatedAt;
    }
}