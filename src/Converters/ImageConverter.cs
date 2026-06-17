namespace OmniConvert.Converters;

using OmniConvert.Core;
using ImageMagick;

public class ImageConverter : IConverter
{
    static readonly Dictionary<string, MagickFormat> Formats = new()
    {
        ["gif"] = MagickFormat.Gif,
        ["jpeg"] = MagickFormat.Jpeg,
        ["jpg"] = MagickFormat.Jpeg,
        ["png"] = MagickFormat.Png,
        ["webp"] = MagickFormat.WebP,
    };

    public bool CanConvert(string inputFormat, string outputFormat)
    {
        return Formats.ContainsKey(inputFormat.ToLower()) && Formats.ContainsKey(outputFormat.ToLower());
    }

    public void Convert(ConversionRequest request)
    {
        string outputPath = request.OutputPath;

        if (File.Exists(outputPath))
            throw new Exception($"Output file already exists: {outputPath}");

        string outputFormat = FormatHelper.GetExtensionFromPath(request.OutputPath!);

        MagickFormat magickFormat = Formats[outputFormat];

        using var image = new MagickImage(request.InputPath);
        image.Format = magickFormat;
        image.Write(outputPath);
    }
    
    /*
    private static string GenerateOutputPath(ConversionRequest request)
    {
        return Path.Combine(
            Path.GetDirectoryName(request.InputPath)!,
            $"{Path.GetFileNameWithoutExtension(request.InputPath)}.{request.OutputFormat}"
        );
    }
    */
}