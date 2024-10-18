using VTools.BadDayPostAggregate.Commands;

namespace VTools.BadDayPostAggregate;

public interface IBadDayPostDomain
{
    Task CreateAsync(CreateBadDayPostCommand command);
    Task UpdateAsync(UpdateBadDayPostCommand command);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<BadDayPost>> GetAllAsync();

}