using VTools.BookEntity;
using VTools.BookEntity.Projections;

namespace VTools.Data.Repositories.Interfaces;

public interface IBookRepository
{
    Task CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task Delete(Guid id);
    Task<GetAllBooksProjection> GetAllAsync(int limit, int offset);
    Task<BookProjection?> GetByIdAsync(Guid id);
    Task<Book?> GetById(Guid commandId);
}