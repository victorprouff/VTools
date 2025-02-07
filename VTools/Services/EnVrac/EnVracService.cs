using NodaTime;
using VTools.Models.EnVrac;

namespace VTools.Services.EnVrac;

public class EnVracService(IClock clock) : IEnVracService
{
    public List<EnVracItem> GetEnVracItems()
    {
        var now = clock.GetCurrentInstant();
        var test = now.Plus(Duration.FromDays(5));

        return new List<EnVracItem>
        {
            new(Guid.NewGuid(), "Titre 1", "Description 1", new List<Article>
            {
                Article.Create("Article1", "Description1", new List<string> { "", "" }, Category.Articles)
            }, now, test),
            new(Guid.NewGuid(), "Titre 2", "Description 2", new List<Article>(), now, test)
        };
    }
}