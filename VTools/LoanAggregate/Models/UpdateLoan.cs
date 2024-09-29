using NodaTime;

namespace VTools.LoanAggregate.Models;

public record UpdateLoanCommand(Guid Id, string Title, string Borrower, bool IsRendered, Instant LoanEndDate);