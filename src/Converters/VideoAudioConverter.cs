namespace OmniConvert.Converters;

using OmniConvert.Core;
using FFMpegCore;

public class VideoAudioConverter : IConverter
{
    static readonly string[] Formats =
    {
        "mp4",
        "mp3",
        "wav",
        "mts",
        "ogg",
    };

    public bool CanConvert(string inputFormat, string outputFormat)
    {
        return Formats.Contains(inputFormat) && Formats.Contains(outputFormat);
    }

    // TODO: support different audio/video codecs - GPU conversion (context menu should prioritize GPU)
    public void Convert(ConversionRequest request)
    {
        FFMpegArguments
            .FromFileInput(request.InputPath)
            .OutputToFile(request.OutputPath, false)
            .ProcessSynchronously();
    }
}