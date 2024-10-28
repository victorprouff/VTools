using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ImageHelper.Services;

public static class ImageService
{
    public static Tuple<Image, string> LoadImageFromStream(string inputValue)
    {
        var imageStream = new FileStream(inputValue, FileMode.Open, FileAccess.Read);

        var split = imageStream.Name.Split('/');
        var fileName = split[^1];

        return new Tuple<Image, string>(Image.Load(imageStream), fileName);
    }

    public static async Task<Stream> ProcessImage(Image image, int rotation, bool imgColorIsWhite)
    {
        image.Mutate(x => x
            .Resize(new ResizeOptions
            {
                Size = new Size(1080, 1080),
                Mode = ResizeMode.Pad
            })
            .Rotate(rotation)
            .BackgroundColor(imgColorIsWhite ? Color.White : Color.Black)
        );

        var jpgStream = new MemoryStream();
        await image.SaveAsync(jpgStream, new JpegEncoder { Quality = 80 });
        jpgStream.Position = 0;

        return jpgStream;
    }
}