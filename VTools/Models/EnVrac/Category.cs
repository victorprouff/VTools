namespace VTools.Models.EnVrac;

public enum Category
{
    Youtube,
    Articles,
    Tools,
    Podcast,
    Livre,
    PutAside
}

public static class CategoryExtensions
{
    public static Category ConvertToCategory(this string? content) =>
        content switch
        {
            nameof(Category.Youtube) => Category.Youtube,
            nameof(Category.Articles) => Category.Articles,
            nameof(Category.Tools) => Category.Tools,
            nameof(Category.Podcast) => Category.Podcast,
            nameof(Category.Livre) => Category.Livre,
            _ => Category.PutAside
        };
}