namespace VTools.Models.EnVrac;

public record Link(string Url, string Title = "Source")
{
    public string ToMarkdown() => $"[{Title}]({Url})";
    public string ToMarkdown(string? title) => $"[{title ?? Title}]({Url})";
}