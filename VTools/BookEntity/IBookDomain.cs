using VTools.BookEntity.Models;
using VTools.BookEntity.Projections;

namespace VTools.BookEntity;

public interface IBookDomain
{
    Task CreateAsync(CreateBookCommand command);
    Task UpdateAsync(UpdateBookCommand command);
    Task<BookProjection?> GetAsync(Guid id);
    Task<GetAllBooksProjection> GetAllAsync(int limit, int offset);
    Task DeleteAsync(Guid id);
}