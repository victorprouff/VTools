using NodaTime;
using VTools.Data.Repositories;
using VTools.Data.Repositories.Interfaces;
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

    public async Task CreateAsync(CreateLoanCommand command)
    {
        var loan = Loan.Create(
            command.Title,
            command.Borrower,
            _clock.GetCurrentInstant());

        await _repository.CreateAsync(loan);
    }

    public async Task UpdateAsync(UpdateLoanCommand command)
    {
        var loan = await _repository.GetById(command.Id)
                   ?? throw new KeyNotFoundException();
        // ?? throw new NotFoundException($"The nugget with id {command.Id} is not found.");

        loan.Update(command.Title, command.Borrower, command.IsRendered, command.LoanEndDate);

        await _repository.UpdateAsync(loan);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.Delete(id);
    }

    public async Task<LoanProjection?> GetAsync(Guid id) =>
        await _repository.GetByIdAsync(id);

    public async Task<GetAllLoansProjection> GetAllAsync(
        int limit,
        int offset,
        bool withInvisibleLoan = false) =>
        await _repository.GetAllAsync(limit, offset, withInvisibleLoan);
}