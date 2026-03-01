namespace VTools.App.Data.Models;

public enum ItemType
{
    Book,
    Other
}

public class LentItem
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public ItemType Type { get; set; }
    public DateOnly LentDate { get; set; }
    public string LentTo { get; set; } = "";
}
