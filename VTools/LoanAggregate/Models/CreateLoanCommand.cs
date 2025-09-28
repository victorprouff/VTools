using NodaTime;

namespace VTools.LoanAggregate.Models;

public record CreateLoanCommand(string Title, string Borrower, Instant LoanStartDate);