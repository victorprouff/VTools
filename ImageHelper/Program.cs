using System.Text;
using ImageHelper.Services;

Console.WriteLine("\\<Image Helper>/");

var arguments = ParseArguments(args);

if (arguments.Count == 0 || arguments.ContainsKey("--help") || !arguments.ContainsKey("--input") || !arguments.ContainsKey("--output"))
{
    return PrintHelp();
}

arguments.TryGetValue("--input", out var inputValue);
arguments.TryGetValue("--output", out var outputPathValue);

arguments.TryGetValue("--rot", out var rotationValue);
var rotation = string.IsNullOrEmpty(rotationValue) ? 0 : int.Parse(rotationValue);

arguments.TryGetValue("--bg", out var backgroundColorValue);
var imgColorIsWhite = string.IsNullOrEmpty(backgroundColorValue) || backgroundColorValue == "white";


var (image, fileName) = ImageService.LoadImageFromStream(inputValue!);
var stream = await ImageService.ProcessImage(image, rotation, imgColorIsWhite);

await using var fileStream = File.Create($"{outputPathValue}/resized-{fileName}");
stream.CopyTo(fileStream);

return 0;


static Dictionary<string, string> ParseArguments(string[] args)
{
    var arguments = new Dictionary<string, string>();

    foreach (var arg in args)
    {
        var parts = arg.Split('=');

        if (parts.Length == 2)
        {
            arguments[parts[0]] = parts[1];
        }
        else
        {
            arguments[arg] = string.Empty;
        }
    }

    return arguments;
}

static int PrintHelp()
{
    StringBuilder builder = new();

    builder.AppendLine("Help:");
    builder.AppendLine("------");
    builder.AppendLine("Usage: ImageHelper [options]");
    builder.AppendLine();
    builder.AppendLine("Options:");
    builder.AppendLine("  --help                             Display this help message");
    builder.AppendLine("  --input=<file path>                Specify input image");
    builder.AppendLine("  --output=<directory destination>   Specify output destination");
    builder.AppendLine("  --rot=90                           Specify rotation in degre (Default rotation : 0)");
    builder.AppendLine("  --bg=<white/black>                 Specify background color (Default color : white)");

    Console.WriteLine(builder.ToString());

    return 0;
}