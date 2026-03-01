namespace VTools.App.Data.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Description { get; set; }
    public DateOnly? EndReadingDate { get; set; }
    public bool IsRead => EndReadingDate.HasValue;
}
