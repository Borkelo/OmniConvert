namespace OmniConvert.Core;

public static class FormatHelper
{
    public static string GetExtensionNameFromPath(string path)
    {
        return  Path.GetExtension(path).ToLower().TrimStart('.');
    }
    
}