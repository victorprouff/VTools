using Dapper;
using VTools.Data.Entities;
using VTools.Data.Repositories.Interfaces;
using VTools.LoanAggregate;
using VTools.LoanAggregate.Projections;

namespace VTools.Data.Repositories;

public class LoanRepository(string connectionString) : BaseRepository(connectionString), ILoanRepository
{
    public async Task CreateAsync(Loan loan)
    {
        const string sql = """
                           INSERT INTO loans (title, borrower, loan_start_date)
                           VALUES (@Title, @Borrower, @LoanStartDate);
                           """;

        await using var connection = GetConnection();
        await connection.ExecuteAsync(sql,
            new
            {
                loan.Title,
                loan.Borrower,
                loan.LoanStartDate
            }, commandTimeout: 1);
    }

    public async Task UpdateAsync(Loan loan)
    {
        await using var connection = GetConnection();
        const string sql = """
                           UPDATE loans 
                           SET 
                               title = @Title,
                               borrower = @Borrower,
                               is_rendered = @IsRendered,
                               is_visible = @isVisible,
                               loan_start_date = @loanStartDate,
                               loan_end_date = @LoanEndDate
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

    public async Task Delete(Guid id)
    {
        await using var connection = GetConnection();
        const string sql = """
                           UPDATE loans 
                           SET 
                               is_visible = false
                           WHERE id = @Id;
                           """;

        await connection.ExecuteAsync(sql, new { id }, commandTimeout: 1);
    }

    public async Task<GetAllLoansProjection> GetAllAsync(int limit, int offset, bool withInvisibleLoan = false)
    {
        var where = withInvisibleLoan is false ? "WHERE l.is_visible = true" : string.Empty;

        var sql = $@"SELECT count(*) FROM loans;
       
                   SELECT l.id, l.title, l.borrower, l.is_rendered, l.is_visible, l.loan_start_date, l.loan_end_date
                   FROM loans l
                   {where}
                    ORDER BY l.loan_start_date DESC
                   LIMIT @Limit OFFSET @Offset;
                   ";

        await using var connexion = GetConnection();
        await using var multi = await connexion.QueryMultipleAsync(
            sql,
            new { Limit = limit, Offset = offset },
            commandTimeout: 1);

        var nbOfLoans = multi.Read<int>().Single();
        var loans = await multi.ReadAsync<LoanEntity>();

        return new GetAllLoansProjection(nbOfLoans, loans.Select(l => (LoanProjection)l));
    }

    public async Task<LoanProjection?> GetByIdAsync(Guid id)
    {
        const string sql = """
                               SELECT l.id, l.title, l.borrower, l.is_rendered, l.is_visible, l.loan_start_date, l.loan_end_date
                               FROM loans l
                               WHERE l.id = @Id;
                           """;

        await using var connexion = GetConnection();
        return await connexion.QueryFirstOrDefaultAsync<LoanProjection?>(
            sql,
            new { Id = id },
            commandTimeout: 1);
    }

    public async Task<Loan?> GetById(Guid id)
    {
        const string sql = """
                               SELECT l.id, l.title, l.borrower, l.is_rendered, l.is_visible, l.loan_start_date, l.loan_end_date
                              FROM loans l
                              WHERE l.id = @Id;
                           """;

        await using var connexion = GetConnection();
        return (Loan?)await connexion.QueryFirstOrDefaultAsync<LoanEntity?>(
            sql,
            new { Id = id },
            commandTimeout: 1);
    }
}