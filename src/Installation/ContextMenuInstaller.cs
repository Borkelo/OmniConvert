using System;
using Microsoft.Win32;

namespace OmniConvert.Installation;

public static class ContextMenuInstaller
{
    public static void Install(string exePath)
    {
        if (!OperatingSystem.IsWindows())
        {
            #if !SILENT_BUILD
            Console.WriteLine("Context menu installation is only supported on Windows.");
            #endif
            return;
        }

        foreach (var input in FormatRegistry.ContextMenuConversions)
        {
            string inputExt = input.Key;

            string basePath = $@"Software\Classes\SystemFileAssociations\.{inputExt}\shell\OmniConvert";
            
            using (RegistryKey baseKey = Registry.CurrentUser.CreateSubKey(basePath))
            {
                if (baseKey != null)
                {
                    baseKey.SetValue("MUIVerb", "OmniConvert");
                    baseKey.SetValue("SubCommands", "");
                }
            }

            foreach (var output in input.Value)
            {
                string actionPath = $@"{basePath}\shell\{output}";
                
                using (RegistryKey actionKey = Registry.CurrentUser.CreateSubKey(actionPath))
                {
                    actionKey?.SetValue("", $"Convert to {output.ToUpper()}");
                }

                string commandPath = $@"{actionPath}\command";

                using RegistryKey commandKey = Registry.CurrentUser.CreateSubKey(commandPath);
                if (commandKey != null)
                {
                    string command = $"\"{exePath}\" -i \"%1\" -o {output}";

                    #if !SILENT_BUILD
                    Console.WriteLine($"Registering: {command}");
                    #endif
                    commandKey.SetValue("", command);
                }
            }
        }    
    }   


    public static void Uninstall()
    {
        if (!OperatingSystem.IsWindows())
        {
            #if !SILENT_BUILD
            Console.WriteLine("Context menu installation is only supported on Windows.");
            #endif
            return;
        }

        foreach (var input in FormatRegistry.ContextMenuConversions)
        {
            string inputExt = input.Key;
            string basePath = $@"Software\Classes\SystemFileAssociations\.{inputExt}\shell\OmniConvert";
            
            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(basePath, false);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to remove registry key for .{inputExt}: {ex.Message}");
            }
        }      
    }
}