namespace VTools.Models.EnVrac;

public enum Category
{
    Vidéos,
    Articles,
    Tools,
    Podcasts,
    Livres,
    PutAside
}

public static class CategoryExtensions
{
    public static Category ConvertToCategory(this string? content) =>
        content switch
        {
            nameof(Category.Vidéos) => Category.Vidéos,
            nameof(Category.Articles) => Category.Articles,
            nameof(Category.Tools) => Category.Tools,
            nameof(Category.Podcasts) => Category.Podcasts,
            nameof(Category.Livres) => Category.Livres,
            _ => Category.PutAside
        };

    public static string ToCategoryString(this Category category) =>
        category.ToString() switch
        {
            nameof(Category.Vidéos) => "🎞️ Vidéos",
            nameof(Category.Articles) => "📖 Articles",
            nameof(Category.Tools) => "🛠️ Tools",
            nameof(Category.Podcasts) => "🎧 Podcasts",
            nameof(Category.Livres) => "📚 Livres",
            _ => "Autres"
        };

}