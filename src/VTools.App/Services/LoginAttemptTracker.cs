using System.Collections.Concurrent;

namespace VTools.App.Services;

public class LoginAttemptTracker
{
    private record AttemptInfo(int Count, DateTimeOffset LockedUntil);

    private readonly ConcurrentDictionary<string, AttemptInfo> _attempts = new();

    private const int MaxAttempts = 5;
    private static readonly TimeSpan LockoutDuration = TimeSpan.FromMinutes(5);

    public bool IsLockedOut(string ip) =>
        _attempts.TryGetValue(ip, out var info) && info.LockedUntil > DateTimeOffset.UtcNow;

    public long GetLockoutExpiryUnix(string ip)
    {
        if (_attempts.TryGetValue(ip, out var info))
            return info.LockedUntil.ToUnixTimeSeconds();
        return 0;
    }

    public void RecordFailure(string ip) =>
        _attempts.AddOrUpdate(ip,
            _ => new AttemptInfo(1, DateTimeOffset.MinValue),
            (_, existing) =>
            {
                var newCount = existing.Count + 1;
                var lockedUntil = newCount >= MaxAttempts
                    ? DateTimeOffset.UtcNow.Add(LockoutDuration)
                    : existing.LockedUntil;
                return new AttemptInfo(newCount, lockedUntil);
            });

    public void RecordSuccess(string ip) => _attempts.TryRemove(ip, out _);
}
