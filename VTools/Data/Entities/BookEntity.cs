using NodaTime;
using VTools.BookEntity;
using VTools.BookEntity.Projections;

namespace VTools.Data.Entities;

public class BookEntity
{
    public BookEntity()
    {
    }

    public BookEntity(
        Guid id,
        string title,
        string author,
        bool isReading,
        string comment,
        Instant? endReadingDate,
        Instant createdAt,
        Instant? updatedAt)
    {
        Id = id;
        Title = title;
        Author = author;
        IsReading = isReading;
        Comment = comment;
        EndReadingDate = endReadingDate;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }


    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public bool IsReading { get; set; }
    public string Comment { get; set; }
    public Instant? EndReadingDate { get; set; }

    public Instant CreatedAt { get; set; }
    public Instant? UpdatedAt { get; set; }


    public static explicit operator BookEntity(Book book) =>
        new(
            book.Id,
            book.Title,
            book.Author,
            book.IsReading,
            book.Comment,
            book.EndReadingDate,
            book.CreatedAt,
            book.UpdatedAt);

    public static explicit operator Book?(BookEntity? book) =>
        book is null
            ? null
            : new Book(
                book.Id,
                book.Title,
                book.Author,
                book.Comment,
                book.IsReading,
                book.EndReadingDate,
                book.CreatedAt,
                book.UpdatedAt);

    public static explicit operator BookProjection(BookEntity book) =>
        new(
            book.Id,
            book.Title,
            book.Author,
            book.IsReading,
            book.Comment,
            book.EndReadingDate,
            book.CreatedAt,
            book.UpdatedAt);
}