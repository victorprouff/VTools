using Npgsql;

namespace VTools.Data.Repositories;

public abstract class BaseRepository(string connectionString)
{
    protected NpgsqlConnection GetConnection() => new(connectionString);
}