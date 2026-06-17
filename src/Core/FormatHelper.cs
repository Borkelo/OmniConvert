namespace OmniConvert.Core;

public static class FormatHelper
{
    public static string GetExtensionFromPath(string path)
    {
        return  Path.GetExtension(path).ToLower().TrimStart('.');
    }

    public static string GetPathWithoutExtension(string path)
    {
        return Path.Combine(Path.GetDirectoryName(path)!, Path.GetFileNameWithoutExtension(path));
    }
    
}