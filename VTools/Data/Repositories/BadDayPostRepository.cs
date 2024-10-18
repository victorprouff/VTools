using Dapper;
using VTools.BadDayPostAggregate;
using VTools.Data.Entities;
using VTools.Data.Repositories.Interfaces;

namespace VTools.Data.Repositories;

public class BadDayPostRepository(string connectionString) : BaseRepository(connectionString), IBadDayPostRepository
{
    public async Task CreateAsync(BadDayPost badDayPost)
    {
        const string sql = """
                           INSERT INTO bad_day_post (url, instagram_id, created_at)
                           VALUES (@Url, @InstagramId, @CreatedAt);
                           """;

        await using var connection = GetConnection();
        await connection.ExecuteAsync(sql,
            new
            {
                badDayPost.Url,
                badDayPost.InstagramId,
                badDayPost.CreatedAt
            }, commandTimeout: 1);
    }

    public async Task UpdateAsync(BadDayPost badDayPost)
    {
        await using var connection = GetConnection();
        const string sql = """
                           UPDATE bad_day_post 
                           SET 
                               url = @Url,
                               instagram_id = @InstagramId,
                               updated_at = @UpdatedAt
                           WHERE id = @Id;
                           """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                badDayPost.Id,
                badDayPost.Url,
                badDayPost.InstagramId,
                badDayPost.UpdatedAt
            },
            commandTimeout: 1);
    }

    public async Task DeleteAsync(Guid id)
    {
        await using var connection = GetConnection();
        const string sql = "DELETE FROM bad_day_post WHERE id = @Id;";

        await connection.ExecuteAsync(sql, new { id }, commandTimeout: 1);
    }

    public async Task<IEnumerable<BadDayPost>> GetAllAsync()
    {
        var sql = @"
                   SELECT bd.id, bd.url, bd.instagram_id, bd.created_at, bd.updated_at
                   FROM bad_day_post bd
                   ORDER BY bd.created_at DESC;
                   ";

        await using var connexion = GetConnection();
        var result = await connexion.QueryAsync<BadDayPostEntity>(
            sql,
            new { },
            commandTimeout: 1);

        return result.Select(bd => new BadDayPost(bd.Id, bd.Url, bd.InstagramId, bd.CreatedAt, bd.UpdatedAt));
    }
}