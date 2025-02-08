using System.Text.RegularExpressions;

namespace VTools.Models.EnVrac;

public static class CleanLinkExtensions
{
    public static Link[] ExtractRegexPattern(string? input, string pattern)
    {
        if (string.IsNullOrEmpty(input))
        {
            return [];
        }

        var matches = Regex.Matches(input, pattern);

        var links = new List<Link>();

        foreach (Match match in matches)
        {
            if (match.Groups.Count == 1)
            {
                links.Add(new Link(RemoveLastCharIfIsPoint(match.Groups[0].Value)));
                continue;
            }

            if (match.Groups.Count > 2)
            {
                links.Add(new Link(RemoveLastCharIfIsPoint(match.Groups[2].Value), match.Groups[1].Value));
            }
        }

        return links.ToArray();
    }

    private static string RemoveLastCharIfIsPoint(string input) =>
        input.EndsWith('.') ? input.Remove(input.Length - 1) : input;

    public static string RemoveRegexPattern(string? input, string pattern)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }

        var matches = Regex.Matches(input, pattern);

        foreach (Match match in matches)
        {
            input = input.Replace(match.Value, "");
        }

        return input;
    }

    public static Link[] RemoveDoublon(IEnumerable<Link> links)
    {
        var distinctLinks = links
            .GroupBy(link => link.Url)         // Grouper par URL
            .Select(group => group.First())   // Garde le premier élément de chaque groupe
            .ToList();

        return distinctLinks.ToArray();
    }

    public static bool IsContainPattern(string? input, string pattern) =>
        !string.IsNullOrEmpty(input) && Regex.IsMatch(input, pattern);
}