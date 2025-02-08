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

    public static string ToCategoryString(this Category category) =>
        category.ToString() switch
        {
            nameof(Category.Youtube) => "🎞️ Youtube",
            nameof(Category.Articles) => "📖 Articles",
            nameof(Category.Tools) => "🛠️ Tools",
            nameof(Category.Podcast) => "🎧 Podcasts",
            nameof(Category.Livre) => "📚 Livres",
            _ => "Autre"
        };

}