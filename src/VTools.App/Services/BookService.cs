using Microsoft.EntityFrameworkCore;
using VTools.App.Data;
using VTools.App.Data.Models;

namespace VTools.App.Services;

public class BookService(AppDbContext db)
{
    public Task<List<Book>> GetAllAsync() =>
        db.Books.OrderBy(b => b.Title).ToListAsync();

    public Task<Book?> GetByIdAsync(int id) =>
        db.Books.FindAsync(id).AsTask();

    public async Task AddAsync(Book book)
    {
        db.Books.Add(book);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Book book)
    {
        db.Books.Update(book);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book is not null)
        {
            db.Books.Remove(book);
            await db.SaveChangesAsync();
        }
    }
}
