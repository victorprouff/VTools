using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Sample;

public class Sample2
{
    public static void Execute()
    {
        Directory.CreateDirectory("sample2");

        using var img = Image.Load<Rgba32>("fb.jpg");

// As Clone returns a new image make sure we dispose of it
        using (Image destRound = img.Clone(x => Helper.ConvertToAvatar(x, new Size(200, 200), 20)))
        {
            destRound.Save("sample2/fb.png");
        }

        using (Image destRound = img.Clone(x => x.ConvertToAvatar(new Size(200, 200), 100)))
        {
            destRound.Save("sample2/fb-round.png");
        }

        using (Image destRound = img.Clone(x => x.ConvertToAvatar(new Size(200, 200), 150)))
        {
            destRound.Save("sample2/fb-rounder.png");
        }
    }
}