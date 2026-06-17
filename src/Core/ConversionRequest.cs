namespace OmniConvert.Core;
public struct ConversionRequest
{
    public string InputPath {get;}
    public string OutputPath {get;}

    public ConversionRequest(string inputPath,  string outputPath)
    {
        InputPath = inputPath;
        OutputPath = outputPath;
    }
}
