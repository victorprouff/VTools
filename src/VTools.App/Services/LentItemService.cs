using Microsoft.EntityFrameworkCore;
using VTools.App.Data;
using VTools.App.Data.Models;

namespace VTools.App.Services;

public class LentItemService(AppDbContext db)
{
    public Task<List<LentItem>> GetAllAsync() =>
        db.LentItems.OrderByDescending(i => i.LentDate).ToListAsync();

    public Task<LentItem?> GetByIdAsync(int id) =>
        db.LentItems.FindAsync(id).AsTask();

    public async Task AddAsync(LentItem item)
    {
        db.LentItems.Add(item);
        await db.SaveChangesAsync();
    }

    public async Task UpdateAsync(LentItem item)
    {
        db.LentItems.Update(item);
        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await db.LentItems.FindAsync(id);
        if (item is not null)
        {
            db.LentItems.Remove(item);
            await db.SaveChangesAsync();
        }
    }
}
