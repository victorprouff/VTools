using Dapper;
using VTools.Data.Entities;
using VTools.LoanAggregate;
using VTools.LoanAggregate.Projections;

namespace VTools.Data.Repositories;

public class LoanRepository(string connectionString) : BaseRepository(connectionString), ILoanRepository
{
    public async Task CreateAsync(Loan loan, CancellationToken cancellationToken)
    {
        const string sql = """
                           INSERT INTO loans (titre, borrower, loanStartDate)
                           VALUES (@Titre, @Borrower, @LoanStartDate);
                           """;

        await using var connection = GetConnection();
        await connection.ExecuteAsync(sql, (LoanEntity)loan, commandTimeout: 1);
    }

    public async Task UpdateAsync(Loan loan, CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        const string sql = """
                           UPDATE nuggets 
                           SET 
                               title = @Title,
                               borrower = @Borrower,
                               isRendered = @IsRendered,
                               isVisible = @isVisible,
                               loanStartDate = @loanStartDate,
                               loanEndDate = @LoanEndDate
                           WHERE id = @Id;
                           """;

        await connection.ExecuteAsync(
            sql,
            new
            {
                loan.Id,
                loan.Title,
                loan.Borrower,
                loan.IsRendered,
                loan.IsVisible,
                loan.LoanStartDate,
                loan.LoanEndDate
            },
            commandTimeout: 1);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        await using var connection = GetConnection();
        const string sql = """
                           UPDATE nuggets 
                           SET 
                               isVisible = false
                           WHERE id = @Id;
                           """;

        await connection.ExecuteAsync(sql, new { id }, commandTimeout: 1);
    }

    public async Task<GetAllLoansProjection> GetAllAsync(int limit, int offset, CancellationToken cancellationToken, bool withInvisibleLoan = false)
    {
        var where = withInvisibleLoan is false ? "WHERE l.visible = true" : string.Empty;

        var sql = $@"SELECT count(*) FROM loans
       
                   SELECT l.id, l.title, l.borrower, l.isRendered, l.isVisible, l.loanStartDate, l.loanEndDate
                   FROM loans l
                   {where}
                   ORDER BY n.loanStartDate DESC
                   LIMIT @Limit OFFSET @Offset;
                   ";

        await using var connexion = GetConnection();
        await using var multi = await connexion.QueryMultipleAsync(
            sql,
            new { Limit = limit, Offset = offset },
            commandTimeout: 1);

        var nbOfLoans = multi.Read<int>().Single();
        var loans = await multi.ReadAsync<LoanProjection>();

        return new GetAllLoansProjection(nbOfLoans, loans);

    }

    public async Task<LoanProjection?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT l.id, l.title, l.borrower, l.isRendered, l.isVisible, l.loanStartDate, l.loanEndDate
            FROM loans l
            WHERE n.id = @Id
        """;

        await using var connexion = GetConnection();
        return await connexion.QueryFirstOrDefaultAsync<LoanProjection?>(
            sql,
            new { Id = id },
            commandTimeout: 1);
    }

    public async Task<Loan?> GetById(Guid id, CancellationToken cancellationToken)
    {
        const string sql = """
           SELECT l.id, l.title, l.borrower, l.isRendered, l.isVisible, l.loanStartDate, l.loanEndDate
           FROM loans l
           WHERE n.id = @Id
        """;

        await using var connexion = GetConnection();
        return (Loan?)await connexion.QueryFirstOrDefaultAsync<LoanEntity?>(
            sql,
            new { Id = id },
            commandTimeout: 1);
    }
}