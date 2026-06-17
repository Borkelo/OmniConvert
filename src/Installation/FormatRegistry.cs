namespace OmniConvert.Installation;

public static class FormatRegistry
{
    public static readonly Dictionary<string, string[]> ContextMenuConversions = new()
    {
        //IMAGE FORMATS
        ["png"] = ["jpg", "webp"],
        ["jpg"] = ["png", "webp"],
        ["jpeg"] = ["png", "webp"],
        ["webp"] = ["png", "jpg"],
        ["svg"] = ["png", "jpg", "webp"],
    };
}