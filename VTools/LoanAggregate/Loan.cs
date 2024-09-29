using NodaTime;

namespace VTools.LoanAggregate;

public class Loan
{
    public Loan(
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

    private Loan(Guid id, string title, string borrower, Instant loanStartDate)
    {
        Id = id;
        Title = title;
        Borrower = borrower;
        LoanStartDate = loanStartDate;
    }

    public Guid Id { get; }
    public string Title { get; private set; }
    public string Borrower { get; private set; }
    public bool IsRendered { get; private set; }
    public bool IsVisible { get; }
    public Instant LoanStartDate { get; }
    public Instant? LoanEndDate { get; private set; }

    public static Loan Create(
        string title,
        string borrower,
        Instant loanStartDate) =>
        new(
            Guid.NewGuid(),
            title,
            borrower,
            loanStartDate);

    public void Update(string title, string borrower, bool isRendered, Instant loanEndDate)
    {
        Title = title;
        Borrower = borrower;
        IsRendered = isRendered;
        LoanEndDate = loanEndDate;
    }
}