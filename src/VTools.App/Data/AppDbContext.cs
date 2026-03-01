using Microsoft.EntityFrameworkCore;
using VTools.App.Data.Models;

namespace VTools.App.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<LentItem> LentItems => Set<LentItem>();
}
