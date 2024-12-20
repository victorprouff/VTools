using VTools.UserAggregate;

namespace VTools.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailOrUsernameAsync(string emailOrUsername, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> CheckIfIsAdmin(Guid id, CancellationToken cancellationToken);
    Task CreateAsync(User user, CancellationToken cancellationToken);
    Task UpdateEmail(Guid userId, string newEmailValue, CancellationToken cancellationToken);
    Task UpdateUsername(Guid userId, string username, CancellationToken cancellationToken);
    Task UpdatePassword(Guid id, string salt, string passwordHash, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}