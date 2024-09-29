using NodaTime;
using VTools.LoanAggregate;
using VTools.LoanAggregate.Projections;

namespace VTools.Data.Entities;

public class LoanEntity
{
    public LoanEntity()
    {
    }

    public LoanEntity(
        Guid id,
        string title,
        string borrower,
        bool isRendered,
        bool isVisible,
        Instant loanStartDate,
        Instant? loanEndDate)
    {
        Id = id;
        Title = title;
        Borrower = borrower;
        IsRendered = isRendered;
        IsVisible = isVisible;
        LoanStartDate = loanStartDate;
        LoanEndDate = loanEndDate;
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Borrower { get; set; }
    public bool IsRendered { get; set; }
    public bool IsVisible { get; set; }
    public Instant LoanStartDate { get; set; }
    public Instant? LoanEndDate { get; set; }

    public static explicit operator LoanEntity(Loan loan) =>
        new(
            loan.Id,
            loan.Title,
            loan.Borrower,
            loan.IsRendered,
            loan.IsVisible,
            loan.LoanStartDate,
            loan.LoanEndDate);

    public static explicit operator Loan?(LoanEntity? loan) =>
        loan is null
            ? null
            : new(
                loan.Id,
                loan.Title,
                loan.Borrower,
                loan.IsRendered,
                loan.IsVisible,
                loan.LoanStartDate,
                loan.LoanEndDate);

    public static explicit operator LoanProjection(LoanEntity loan) =>
        new(
            loan.Id,
            loan.Title,
            loan.Borrower,
            loan.IsRendered,
            loan.IsVisible,
            loan.LoanStartDate,
            loan.LoanEndDate);
}