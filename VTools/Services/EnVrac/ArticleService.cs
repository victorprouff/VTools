using VTools.Models.EnVrac;

namespace VTools.Services.EnVrac;

public class ArticleService : IArticleService
{
    public List<Article> GetArticles()
    {
        return new List<Article>
        {
            Article.Create("Bye Twitter, ou X, peu importe",
                @"Hugo de EventuallyCoding quitte X et explique pourquoi.
Les émissions que l'on regarde, les commerces dans lesquels on consomme, les réseaux sociaux que l'on utilise, ce sont des actes économiques, et, in fine, politiques.
Nous votons avec notre portefeuille, ou notre temps de cerveau.
Et, pour moi, rester sur X, ça devient de la complicité.", new List<string> { "https://eventuallycoding.com/2025/01/bye-twitter" }, Category.Article),
            Article.Create("[Underscore_] Pourquoi le business du DDOS explose (et c'est inquiétant)", "", new List<string> { "https://www.youtube.com/watch?v=OxSUNLTj-l8" }, Category.Youtube),
            Article.Create("Privés de vie privée ? - #DATAGUEULE 40", "", new List<string> { "https://youtu.be/5jDMTSTXMnU?si=iTB4mTekyi0tiK66", "" }, Category.Youtube),
            Article.Create("Cyberattaque contre E.Leclerc\u00a0: des données personnelles «\u00a0ont pu être exposées\u00a0»", "", new List<string> { "https://next.ink/167199/cyberattaque-contre-e-leclerc-des-donnees-personnelles-ont-pu-etre-exposees/", "" }, Category.Article),
            Article.Create("OwnTracks - Autogérez vos données de localisation en toute confiance", "", new List<string> { "https://korben.info/owntracks-gestion-securisee-donnees-localisation.html", "https://owntracks.org/" }, Category.Tools)
        };
    }
}