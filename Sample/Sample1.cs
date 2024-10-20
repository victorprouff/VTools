using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace Sample;

public class Sample1
{
    public static void Execute()
    {
        Directory.CreateDirectory("sample1");

        using Image image = Image.Load("fb.jpg");

// Sample 1
        int outerRadii = Math.Min(image.Width, image.Height) / 2;

        IPath star =
            new Star(new PointF(image.Width / 2, image.Height / 2), 5, outerRadii / 2, outerRadii).AsClosedPath();

// Apply the effect here inside the shape
        image.Mutate(x => x.Clip(star, y => GaussianBlurExtensions.GaussianBlur(y, 15)));

        image.Save("sample1/fb.png");
    }
}