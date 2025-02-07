namespace Sample;

public class PasswordBreaker
{
    public static async Task Execute()
    {
        Console.WriteLine("Hello World!");

        await Break("test43GF");

    }

    private static async Task Break(string passwordToBreak)
    {
        var result = await BruteForceSearch(passwordToBreak);

        Console.WriteLine($"SUCCESS ! => {result}");
    }

    private static Task<string> BruteForceSearch(string target)
    {
        const string lowerChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Ensemble des caractères à tester

        var length = 1; // Longueur actuelle des chaînes à tester

        while (true)
        {
            foreach (var combination in GenerateCombinations(lowerChars, length))
            {
                Console.WriteLine(combination);

                if (combination == target)
                {
                    return Task.FromResult(combination); // Chaîne trouvée
                }

                // await Task.Delay(1); // Ajoute un petit délai pour visualiser chaque étape
            }
            // await Task.Delay(1); // Ajoute un petit délai pour visualiser chaque étape

            length++; // Augmente la longueur des combinaisons à tester
        }
    }

    private static IEnumerable<string> GenerateCombinations(string chars, int length)
    {
        // Utilise une approche récursive pour générer toutes les combinaisons d'une certaine longueur
        if (length == 1)
        {
            foreach (var c in chars)
            {
                yield return c.ToString();
            }
        }
        else
        {
            foreach (var c in chars)
            {
                foreach (var sub in GenerateCombinations(chars, length - 1))
                {
                    yield return c + sub;
                }
            }
        }
    }
}