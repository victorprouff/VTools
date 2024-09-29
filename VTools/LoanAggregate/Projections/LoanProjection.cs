using NodaTime;

namespace VTools.LoanAggregate.Projections;

public record LoanProjection(
    Guid Id,
    string Title,
    string Borrower,
    bool IsRendered,
    bool IsVisible,
    Instant LoanStartDate,
    Instant? LoanEndDate);