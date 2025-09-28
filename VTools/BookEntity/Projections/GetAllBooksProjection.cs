namespace VTools.BookEntity.Projections;

public record GetAllBooksProjection(int NbOfBook, IEnumerable<BookProjection> Books);
