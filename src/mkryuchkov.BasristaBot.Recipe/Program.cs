using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;

Console.WriteLine("Start");

const string text = @"🇷🇼 Убумве Пиберри
 🍷  Натуральный
 🔻  Воронка

15 г. | 64 NZ | 250 мл | 96ºC

+50 => 50  @  '00
+80 => 120 @  '30 =>  '50
+80 => 200 @ 1'00 => 1'15
+50 => 250 @ 1'55 => 2'10
        t' = 2'55";

const int width = 300, height = 400, margin = 10, fontSize = 16, scale = 2;

using Image image = new Image<Rgba32>(width * scale, height * scale);

var fc = new FontCollection();
var family = fc.Add("fonts/Unifontexmono-AL3RA.ttf");
var font = family.CreateFont(32, FontStyle.Regular);

const string fontName = "JetBrains Mono";
// const string fontName = "JetBrains Mono ExtraBold";
// const string fontName = "JetBrains Mono Medium";
// var font = SystemFonts.CreateFont(fontName, 64, FontStyle.Regular);

var location = new PointF(margin * scale, margin * scale);

var textOptions = new RichTextOptions(font)
{
    HorizontalAlignment = HorizontalAlignment.Left,
    VerticalAlignment = VerticalAlignment.Top,
    // WrappingLength = width - location.X - margin * scale,
    Origin = location,
    FallbackFontFamilies = new[] { family }
    // LineSpacing = 1.2f
};

var drawingOptions = new DrawingOptions
{
    GraphicsOptions = new GraphicsOptions
    {
        Antialias = false
    }
};

image.Mutate(ctx => ctx
    .Fill(Color.White)
    .DrawText(drawingOptions, textOptions, text, new SolidBrush(Color.Black), null)
    .Resize(new Size(width, height), new BicubicResampler(), true)
    // .BinaryThreshold(0.75f)
    .BinaryThreshold(0.95f)
);

image.Save("./test.png");

Console.WriteLine("End");