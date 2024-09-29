using VTools.LoanAggregate.Models;
using VTools.LoanAggregate.Projections;

namespace VTools.LoanAggregate;

public interface ILoanDomain
{
    Task CreateAsync(CreateLoanCommand command, CancellationToken cancellationToken);
    Task UpdateAsync(UpdateLoanCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<LoanProjection?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<GetAllLoansProjection> GetAllAsync(int limit, int offset, CancellationToken cancellationToken, bool withInvisibleLoan = false);
}