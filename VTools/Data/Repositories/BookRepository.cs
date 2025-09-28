using Dapper;
using VTools.BookEntity;
using VTools.BookEntity.Projections;
using VTools.Data.Repositories.Interfaces;

namespace VTools.Data.Repositories;

public class BookRepository(string connectionString) : BaseRepository(connectionString), IBookRepository
{
    public async Task CreateAsync(Book book)
    {
        const string sql = """
                           INSERT INTO books (title, author, is_reading, end_reading_date, comment, created_at, updated_at)
                           VALUES (@Title, @Author, @IsReading, @EndReadingDate, @Comment, @CreatedAt, @UpdatedAt);
                           """;

        await using var connection = GetConnection();
        await connection.ExecuteAsync(sql,
            new
            {
                book.Title,
                book.Author,
                book.IsReading,
                book.EndReadingDate,
                book.Comment,
                book.CreatedAt,
                book.UpdatedAt,
            }, commandTimeout: 1);
    }

    public async Task UpdateAsync(Book book)
    {
        await using var connection = GetConnection();
        const string sql = """
                           UPDATE books 
                           SET 
                               title = @Title,
                               author = @Author,
                               is_reading = @IsReading,
                               comment = @Comment,
                               end_reading_date = @EndReadingDate
                               updated_at = @UpdatedAt
                           WHERE id = @Id;
                           """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                book.Title,
                book.Author,
                book.IsReading,
                book.EndReadingDate,
                book.Comment
            },
            commandTimeout: 1);    }

    public async Task Delete(Guid id)
    {
        await using var connection = GetConnection();
        const string sql = """
                           DELETE FROM Books 
                           WHERE id = @Id;
                           """;

        await connection.ExecuteAsync(sql, new { id }, commandTimeout: 1);
    }

    public async Task<GetAllBooksProjection> GetAllAsync(int limit, int offset)
    {
        const string sql = """
                            SELECT count(*) FROM books;
                            
                            SELECT b.id, b.title, b.author, b.is_reading, b.comment, b.end_reading_date, b.created_at, b.updated_at
                            FROM books b
                            ORDER BY b.created_at DESC
                            LIMIT @Limit OFFSET @Offset;
                            """;

        await using var connexion = GetConnection();
        await using var multi = await connexion.QueryMultipleAsync(
            sql,
            new { Limit = limit, Offset = offset },
            commandTimeout: 1);

        var nbOfBooks = multi.Read<int>().Single();
        var loans = await multi.ReadAsync<Entities.BookEntity>();

        return new GetAllBooksProjection(nbOfBooks, loans.Select(l => (BookProjection)l));
    }

    public async Task<BookProjection?> GetByIdAsync(Guid id)
    {
        const string sql = """
                               SELECT b.id, b.title, b.author, b.is_reading, b.comment, b.end_reading_date, b.created_at, b.updated_at
                               FROM books l
                               WHERE b.id = @Id;
                           """;

        await using var connexion = GetConnection();
        return await connexion.QueryFirstOrDefaultAsync<BookProjection?>(
            sql,
            new { Id = id },
            commandTimeout: 1);
    }

    public async Task<Book?> GetById(Guid id)
    {
        const string sql = """
                               SELECT l.id, l.title, l.borrower, l.is_rendered, l.is_visible, l.loan_start_date, l.loan_end_date, l.created_at, l.updated_at
                              FROM loans l
                              WHERE l.id = @Id;
                           """;

        await using var connexion = GetConnection();
        return (Book?)await connexion.QueryFirstOrDefaultAsync<Entities.BookEntity?>(
            sql,
            new { Id = id },
            commandTimeout: 1);
    }
}