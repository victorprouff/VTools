using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Sample;

public static class Test
{
    public static void Execute()
    {
        const string directory = "test";

        Directory.CreateDirectory(directory);

        using var rotate90 = Image.Load("fb.jpg");
        var rotate180 = Image.Load("fb.jpg");
        var rotate270 = Image.Load("fb.jpg");
        var backgroundColor = Image.Load("fb.jpg");

        rotate90.Mutate(img => img.Rotate(RotateMode.Rotate90));
        rotate90.Save($"{directory}/fb-rotate-90.png");

        rotate180.Mutate(img => img.Rotate(RotateMode.Rotate180));
        rotate180.Save($"{directory}/fb-rotate-180.png");

        rotate270.Mutate(img => img.Rotate(RotateMode.Rotate270));
        rotate270.Save($"{directory}/fb-rotate-270.png");


        backgroundColor.Mutate(img => img
            .Resize(new ResizeOptions
            {
                Size = new Size(300, 300),
                Mode = ResizeMode.Pad
            })
            .BackgroundColor(Color.Red));
        backgroundColor.Save($"{directory}/fb-BackgroundColor.png");

        var resize = Image.Load("fb.jpg");
        resize.Mutate(x => x
            .Resize(new ResizeOptions
            {
                Size = new Size(300, 300),
                Mode = ResizeMode.Pad
            })
            .BackgroundColor(new Rgba32(255, 0, 0))
        );
        resize.Save($"{directory}/fb-resize.png");

        var resize2 = Image.Load("sample.jpg");
        resize2.Mutate(x => x
            .Resize(new ResizeOptions
            {
                Size = new Size(700, 700),
                Mode = ResizeMode.Pad
            })
            .BackgroundColor(new Rgba32(12, 12, 221))
        );
        resize2.Save($"{directory}/fb-resize2.png");
    }
}