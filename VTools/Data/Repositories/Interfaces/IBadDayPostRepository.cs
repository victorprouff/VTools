using VTools.BadDayPostAggregate;

namespace VTools.Data.Repositories.Interfaces;

public interface IBadDayPostRepository
{
    Task CreateAsync(BadDayPost badDayPost);
    Task UpdateAsync(BadDayPost badDayPost);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<BadDayPost>> GetAllAsync();
}