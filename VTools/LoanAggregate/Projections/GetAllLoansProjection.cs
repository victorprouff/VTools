namespace VTools.LoanAggregate.Projections;

public record GetAllLoansProjection(int NbOfNuggets, IEnumerable<LoanProjection> Loans);
