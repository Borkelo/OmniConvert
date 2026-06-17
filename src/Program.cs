using OmniConvert.Core;
using OmniConvert.Converters;
using OmniConvert.Installation;
using System.Runtime.InteropServices;

internal class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
    private const uint MB_OK = 0x00000000;
    private const uint MB_ICONERROR = 0x00000010;

    private static void Main(string[] args)
    {
        try
        {
            var converters = new List<IConverter>
            {
                new ImageConverter()
            };

            var router = new ConverterRouter(converters);

            (string? input, string? output) = ParseArgs(args);

            if(input == null || output == null)
                return;

            #if !SILENT_BUILD
            Console.WriteLine("Processing file...");
            #endif
  
            if (!output!.Contains('.'))
            {
                output = Path.ChangeExtension(input!, output);
            }

            var request = new ConversionRequest(input!, output!);
            router.Route(request);

            #if !SILENT_BUILD
            Console.WriteLine($"File converted successfully at {output}");
            #endif

        }
        catch(Exception ex)
        {
            #if SILENT_BUILD
            _ = MessageBox(
                    IntPtr.Zero,
                    $"An error occurred during conversion:\n\n{ex.Message}",
                    "Conversion Error",
                    MB_OK | MB_ICONERROR
                );
            #else
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            #endif
        }
    }
    static (string? input, string? output) ParseArgs(string[] args)
    {
        string? input = null;
        string? output = null;

        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-i":
                case "--input":
                    input = args[++i];
                    break;

                case "-o":
                case "--output":
                    output = args[++i];
                    break;

                case "--install":
                    RegisterToContextMenu();
                    break;
                
                case "--uninstall":
                    RemoveFromContextMenu();
                    break;

                case "--reinstall":
                    RemoveFromContextMenu();
                    RegisterToContextMenu();
                    break; 
            }
        }

        return (input, output);
    }

    static void RegisterToContextMenu()
    {
        #if !SILENT_BUILD
        Console.WriteLine("Registering OmniConvert to the Windows context menu...");
        #endif

        ContextMenuInstaller.Install(GetContextMenuExecutablePath());

        #if !SILENT_BUILD
        Console.WriteLine("Context menu installed successfully");
        #endif
    }

    static void RemoveFromContextMenu()
    {
        #if !SILENT_BUILD
        Console.WriteLine("Removing OmniConvert from the Windows context menu...");
        #endif

        ContextMenuInstaller.Uninstall();

        #if !SILENT_BUILD
        Console.WriteLine("Context menu removed successfully");
        #endif
    }

    static string GetContextMenuExecutablePath()
    {
        string currentPath = Environment.ProcessPath!;

        if(currentPath.EndsWith("w.exe", StringComparison.OrdinalIgnoreCase))
        {
            return currentPath;
        }

        string silentPath = Path.ChangeExtension(currentPath, null) + "w.exe";

        if (!File.Exists(silentPath))
        {
            throw new FileNotFoundException($"Installation failed. Could not find the required silent companion executable at:\n{silentPath}");
        }

        return silentPath;
    }
}