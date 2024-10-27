using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using Color = SixLabors.ImageSharp.Color;
using Size = SixLabors.ImageSharp.Size;


StringBuilder builder = new();
builder.AppendLine("Image Helper :");

Console.WriteLine(builder.ToString());

var arguments = ParseArguments(args);

if (arguments.Count == 0 || arguments.ContainsKey("--help") || !arguments.ContainsKey("--input") || !arguments.ContainsKey("--output"))
{
    return PrintHelp();
}

arguments.TryGetValue("--input", out var inputValue);
arguments.TryGetValue("--rot", out var rotationValue);
arguments.TryGetValue("--bg", out var backgroundColorValue);
arguments.TryGetValue("--output", out var outputPathValue);

if (outputPathValue is null)
{
    throw new ArgumentException("No output specified");
}

var rotation = string.IsNullOrEmpty(rotationValue) ? 0 : int.Parse(rotationValue);
var imgColorIsWhite = string.IsNullOrEmpty(backgroundColorValue) || backgroundColorValue == "white";

var (image, fileName) = LoadImageFromStream(inputValue!);
var stream = await ProcessImage(image, rotation, imgColorIsWhite);

await using var fileStream = File.Create($"{outputPathValue}/resized-{fileName}");
stream.CopyTo(fileStream);

return 0;


static Tuple<Image, string> LoadImageFromStream(string inputValue)
{
    var imageStream = new FileStream(inputValue, FileMode.Open, FileAccess.Read);

    var split = imageStream.Name.Split('/');
    var fileName = split[^1];

    return new Tuple<Image, string>(Image.Load(imageStream), fileName);
}

static async Task<Stream> ProcessImage(Image image, int rotation, bool imgColorIsWhite)
{
    image.Mutate(x => x
        .Resize(new ResizeOptions
        {
            Size = new Size(1080, 1080),
            Mode = ResizeMode.Pad
        }));

    image.Mutate(img => img.Rotate(rotation));

    if (imgColorIsWhite)
    {
        image.Mutate(img => img.BackgroundColor(Color.White));
    }
    else
    {
        image.Mutate(img => img.BackgroundColor(Color.Black));
    }

    var webPStream = new MemoryStream();

    await image.SaveAsync(webPStream, new WebpEncoder
    {
        Quality = 80
    });
    webPStream.Position = 0;

    return webPStream;
}

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
    Console.WriteLine("Help:");
    Console.WriteLine("------");
    Console.WriteLine("Usage: ImageHelper [options]");
    Console.WriteLine();
    Console.WriteLine("Options:");
    Console.WriteLine("  --help                             Display this help message");
    Console.WriteLine("  --input=<file path>                Specify input image");
    Console.WriteLine("  --output=<directory destination>   Specify output destination");
    Console.WriteLine("  --rot=90                           Specify rotation in degre (Default rotation : 0)");
    Console.WriteLine("  --bg=<white/black>                 Specify background color (Default color : white)");

    return 0;
}