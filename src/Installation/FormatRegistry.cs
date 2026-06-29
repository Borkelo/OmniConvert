namespace OmniConvert.Installation;

public static class FormatRegistry
{
    public static readonly Dictionary<string, string[]> ContextMenuConversions = new()
    {
        // IMAGE
        ["png"] = ["jpg", "webp"],
        ["jpg"] = ["png", "webp"],
        ["jpeg"] = ["png", "webp"],
        ["webp"] = ["png", "jpg"],
        ["svg"] = ["png", "jpg", "webp"],
        ["bmp"] = ["png", "jpg", "webp"],
        ["gif"] = ["png", "jpg", "webp"],
        ["avif"] = ["png", "jpg", "webp"],

        // VIDEO AND AUDIO
        ["mp4"] = ["mp3", "wav"],
        ["mp3"] = ["wav"],
        ["wav"] = ["mp3"],
        ["mts"] = ["mp4", "mp3", "wav"],
        ["ogg"] = ["mp3", "wav"],
    };
}