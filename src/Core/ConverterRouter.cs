using OmniConvert.Core;

public class ConverterRouter
{
    readonly List<IConverter> converters;

    public ConverterRouter(List<IConverter> converters)
    {
        this.converters = converters;
    }

    public void Route(ConversionRequest request)
    {
        string inputFormat = FormatHelper.GetExtensionFromPath(request.InputPath);
        string outputFormat = FormatHelper.GetExtensionFromPath(request.OutputPath);
        
        if (inputFormat == outputFormat)
            throw new Exception("Input and output formats are the same.");

        var converter = converters.FirstOrDefault(c => c.CanConvert(inputFormat, outputFormat)) 
            ?? throw new Exception($"No converter found for {inputFormat} → {outputFormat}");

        converter.Convert(request);
    }
}