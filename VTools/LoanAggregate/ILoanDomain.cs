using VTools.LoanAggregate.Models;
using VTools.LoanAggregate.Projections;

namespace VTools.LoanAggregate;

public interface ILoanDomain
{
    Task CreateAsync(CreateLoanCommand command);
    Task UpdateAsync(UpdateLoanCommand command);
    Task DeleteAsync(Guid id);
    Task<LoanProjection?> GetAsync(Guid id);
    Task<GetAllLoansProjection> GetAllAsync(int limit, int offset, bool withInvisibleLoan = false);
}