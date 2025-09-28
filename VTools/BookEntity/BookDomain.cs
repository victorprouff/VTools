using NodaTime;
using VTools.BookEntity.Models;
using VTools.BookEntity.Projections;
using VTools.Data.Repositories.Interfaces;

namespace VTools.BookEntity;

public class BookDomain(IBookRepository repository, IClock clock) : IBookDomain
{
    public async Task CreateAsync(CreateBookCommand command)
    {
        var loan = Book.Create(
            command.Title,
            command.Author,
            command.Comment,
            command.IsReading,
            command.EndReadingDate,
            clock.GetCurrentInstant());

        await repository.CreateAsync(loan);
    }

    public async Task UpdateAsync(UpdateBookCommand command)
    {
        var book = await repository.GetById(command.Id)
                   ?? throw new KeyNotFoundException();

        book.Update(
            command.Title,
            command.Author,
            command.Comment,
            command.IsReading,
            command.EndReadingDate,
            clock.GetCurrentInstant());

        await repository.UpdateAsync(book);
    }

    public async Task DeleteAsync(Guid id)
    {
        await repository.Delete(id);
    }

    public async Task<BookProjection?> GetAsync(Guid id) =>
        await repository.GetByIdAsync(id);

    public async Task<GetAllBooksProjection> GetAllAsync(
        int limit,
        int offset) =>
        await repository.GetAllAsync(limit, offset);
}