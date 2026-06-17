using OmniConvert.Core;
using OmniConvert.Converters;
using OmniConvert.Installation;

internal class Program
{

    private static void Main(string[] args)
    {
        var converters = new List<IConverter>
        {
            new ImageConverter()
        };

        var router = new ConverterRouter(converters);

       (string? input, string? output) = ParseArgs(args);

       if(input == null || output == null)
        return;

        if (!output!.Contains('.'))
        {
            output = $"{FormatHelper.GetPathWithoutExtension(input!)}.{output}";
        }

       var request = new ConversionRequest(input!, output!);
       router.Route(request);

       Console.WriteLine("Conversion completed successfully!");
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
                    ContextMenuInstaller.Install(Environment.ProcessPath!);
                    break;
                
                case "--uninstall":
                    ContextMenuInstaller.Uninstall();
                    break;

                case "--reinstall":
                    ContextMenuInstaller.Uninstall();
                    ContextMenuInstaller.Install(Environment.ProcessPath!);
                    break; 
            }
        }

        return (input, output);
    }
}