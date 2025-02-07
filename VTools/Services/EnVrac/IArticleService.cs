using VTools.Models.EnVrac;

namespace VTools.Services.EnVrac;

public interface IArticleService
{
    List<Article> GetArticles();
}