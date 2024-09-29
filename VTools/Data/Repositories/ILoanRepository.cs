using VTools.LoanAggregate;
using VTools.LoanAggregate.Projections;

namespace VTools.Data.Repositories;

public interface ILoanRepository
{
    Task CreateAsync(Loan loan, CancellationToken cancellationToken);
    Task UpdateAsync(Loan loan, CancellationToken cancellationToken);
    Task Delete(Guid id, CancellationToken cancellationToken);
    Task<GetAllLoansProjection> GetAllAsync(int limit, int offset, CancellationToken cancellationToken, bool withInvisibleLoan = false);
    Task<LoanProjection?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Loan?> GetById(Guid commandId, CancellationToken cancellationToken);
}