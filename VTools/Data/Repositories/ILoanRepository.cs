using VTools.LoanAggregate;
using VTools.LoanAggregate.Projections;

namespace VTools.Data.Repositories;

public interface ILoanRepository
{
    Task CreateAsync(Loan loan);
    Task UpdateAsync(Loan loan);
    Task Delete(Guid id);
    Task<GetAllLoansProjection> GetAllAsync(int limit, int offset, bool withInvisibleLoan = false);
    Task<LoanProjection?> GetByIdAsync(Guid id);
    Task<Loan?> GetById(Guid commandId);
}