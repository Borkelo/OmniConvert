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
        if(!File.Exists(request.InputPath))
            throw new Exception("Input file does not exist");

        string inputFormat = FormatHelper.GetExtensionNameFromPath(request.InputPath);
        string outputFormat = FormatHelper.GetExtensionNameFromPath(request.OutputPath);
        
        if (inputFormat == outputFormat)
            throw new Exception("Input and output formats are the same.");

        var converter = converters.FirstOrDefault(c => c.CanConvert(inputFormat, outputFormat)) 
            ?? throw new Exception($"No converter found for {inputFormat} → {outputFormat}");

        converter.Convert(request);
    }
}