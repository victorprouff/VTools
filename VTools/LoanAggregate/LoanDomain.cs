using NodaTime;
using VTools.Data.Repositories;
using VTools.LoanAggregate.Models;
using VTools.LoanAggregate.Projections;

namespace VTools.LoanAggregate;

public class LoanDomain : ILoanDomain
{
    private readonly IClock _clock;
    private readonly ILoanRepository _repository;

    public LoanDomain(IClock clock, ILoanRepository repository)
    {
        _clock = clock;
        _repository = repository;
    }

    public async Task CreateAsync(CreateLoanCommand command, CancellationToken cancellationToken)
    {
        var loan = Loan.Create(
            command.Title,
            command.Borrower,
            _clock.GetCurrentInstant());

        await _repository.CreateAsync(loan, cancellationToken);
    }

    public async Task UpdateAsync(UpdateLoanCommand command, CancellationToken cancellationToken)
    {
        var loan = await _repository.GetById(command.Id, cancellationToken)
                   ?? throw new KeyNotFoundException();
        // ?? throw new NotFoundException($"The nugget with id {command.Id} is not found.");

        loan.Update(command.Title, command.Borrower, command.IsRendered, command.LoanEndDate);

        await _repository.UpdateAsync(loan, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.Delete(id, cancellationToken);
    }

    public async Task<LoanProjection?> GetAsync(Guid id, CancellationToken cancellationToken) =>
        await _repository.GetByIdAsync(id, cancellationToken);

    public async Task<GetAllLoansProjection> GetAllAsync(
        int limit,
        int offset,
        CancellationToken cancellationToken,
        bool withInvisibleLoan = false) =>
        await _repository.GetAllAsync(limit, offset, cancellationToken, withInvisibleLoan);
}