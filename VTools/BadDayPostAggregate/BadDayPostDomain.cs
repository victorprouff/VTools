using NodaTime;
using VTools.BadDayPostAggregate.Commands;
using VTools.Data.Repositories.Interfaces;

namespace VTools.BadDayPostAggregate;

public class BadDayPostDomain : IBadDayPostDomain
{
    private readonly IClock _clock;
    private readonly IBadDayPostRepository _repository;

    public BadDayPostDomain(IClock clock, IBadDayPostRepository repository)
    {
        _clock = clock;
        _repository = repository;
    }

    public async Task CreateAsync(CreateBadDayPostCommand command)
    {
        var badDayPost = BadDayPost.Create(
            command.Url,
            command.InstagramId,
            _clock.GetCurrentInstant());

        await _repository.CreateAsync(badDayPost);
    }

    public Task UpdateAsync(UpdateBadDayPostCommand command)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<BadDayPost>> GetAllAsync() => await _repository.GetAllAsync();
}