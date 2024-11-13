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

    public static Image ProcessImage(Image image, int rotation, bool imgColorIsWhite)
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

        return image;
    }

    public static void SaveImageToFile(Image image, string outputPathValue, string fileName)
    {
        image.SaveAsJpeg($"{outputPathValue}/resized-{fileName}", new JpegEncoder { Quality = 80 });
    }
}