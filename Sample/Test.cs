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

        var backgroundColor = Image.Load("fb.jpg");

        backgroundColor.Mutate(img => img.Rotate(RotateMode.Rotate90));
        backgroundColor.Mutate(img => img
            // .Resize(new ResizeOptions
            // {
            //     Size = new Size(300, 300),
            //     Mode = ResizeMode.Pad
            // })
            .Rotate(45)
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