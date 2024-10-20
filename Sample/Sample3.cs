using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace Sample;

public static class Sample3
{
    public static void Execute()
    {
        Directory.CreateDirectory("sample3");

        using Image image = Image.Load("fb.jpg");

// fixup the source image to be rotated correctly for the image
        image.Mutate(img => img.Transform(new AffineTransformBuilder()
            .AppendRotationDegrees(10)
        ));

// lets get a few overlapping shapes to blur
        var parts = new[]
        {
            new BlurRegion()
            {
                Rect = new RectangleF(70, 20, 150, 150),
                Blur = true,
                Rotation = -10f
            },
            new BlurRegion()
            {
                Rect = new RectangleF(120, 120, 20, 20),
                Blur = false,
                Rotation = -20f
            },
            new BlurRegion()
            {
                Rect = new RectangleF(20, 100, 300, 50),
                Blur = true
            },
            new BlurRegion()
            {
                Rect = new RectangleF(170, 115, 20, 20),
                Blur = false,
                Rotation = 20f
            }
        };

        using var layederd = image.Clone(o => o.BlurRegionsLayered(parts));
        layederd.Save("sample3/fb-layer.png");

        using var combined = image.Clone(o => o.BlurRegionsCombined(parts));
        combined.Save("sample3/fb-combined.png");

        image.Save("sample3/fb.png");
    }

    public class BlurRegion
    {
        public RectangleF Rect { get; set; }
        public bool Blur { get; set; }
        public float Rotation { get; set; } = 0;

        public IPath AsPath()
            => new RectangularPolygon(Rect.X, Rect.Y, Rect.Width, Rect.Height)
                .Transform(
                    Matrix3x2Extensions.CreateRotationDegrees(
                        Rotation,
                        new PointF((Rect.Width / 2 + Rect.X), (Rect.Height / 2 + Rect.Y))
                    )
                );
    }


    // the order of blur/unblur makes no difference in this verion
    public static IImageProcessingContext BlurRegionsCombined(this IImageProcessingContext processingContext,
        IEnumerable<BlurRegion> blurRegions)
    {
        var blurPaths = blurRegions.Where(x => x.Blur == true)
            .Select(x => x.AsPath())
            .ToList();

        // use the first shape as the shape to combine into
        IPath path = blurPaths[0];

        // we have more than 1 blur region, combine them all together
        if (blurPaths.Count > 1)
        {
            // skip 1 as path is already the first item in the collection
            var remainingPaths = blurPaths.Skip(1);
            path = path.Clip(new ShapeOptions() { ClippingOperation = ClippingOperation.Union },
                remainingPaths); // merge all the blurs together
        }

        // now we have some blurs lets start choping out regions to leave unblured
        var unblurPaths = blurRegions.Where(x => x.Blur == false)
            .Select(x => x.AsPath())
            .ToList();

        if (unblurPaths.Count > 0)
        {
            // chop out the unblur regions fro the overall blur path
            path = path.Clip(new ShapeOptions() { ClippingOperation = ClippingOperation.Difference }, unblurPaths);
        }

        return processingContext.Clip(path, y => y.GaussianBlur(15));
    }

    // apply each blur/unblur operation in turn such that a blur ontop of an unblur will reblur a section
    // the order of blur/unblur will effect the outcome in this verion
    public static IImageProcessingContext BlurRegionsLayered(this IImageProcessingContext processingContext,
        IEnumerable<BlurRegion> blurRegions)
    {
        IPath path = null;
        foreach (var p in blurRegions)
        {
            if (p.Blur)
            {
                if (path == null)
                {
                    path = p.AsPath();
                }
                else
                {
                    path = path.Clip(new ShapeOptions() { ClippingOperation = ClippingOperation.Union },
                        p.AsPath());
                }
            }
            else
            {
                if (path == null)
                {
                    // noop can't unblur when no blue has happend yet
                }
                else
                {
                    path = path.Clip(new ShapeOptions() { ClippingOperation = ClippingOperation.Difference },
                        p.AsPath());
                }
            }
        }

        return processingContext.Clip(path, y => y.GaussianBlur(15));
    }
}