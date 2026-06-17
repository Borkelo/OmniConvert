namespace OmniConvert.Core;
public interface IConverter
{
    public bool CanConvert(string inputFormat, string outputFormat);
    void Convert(ConversionRequest request);
}