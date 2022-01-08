using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;

namespace Rpcs3GenerateStubs
{
    public class App
    {
        public static void Main(string[] args)
        {
            bool error = false;
            string sRpcs3 = null;
            string sOut = null;
            bool? sNoGui = null;

            string rpcs3Path = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "Rpcs3Path", null);
            string shortcutsFolder = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "ShortcutsPath", null);
            string hideGui = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Rpcs3GenerateStubs", "HideGui", null);
            if (rpcs3Path != null && File.Exists(rpcs3Path))
                sRpcs3 = Path.GetDirectoryName(rpcs3Path);
            if (shortcutsFolder != null && Directory.Exists(shortcutsFolder))
                sOut = shortcutsFolder;
            if (hideGui != null && (hideGui=="TRUE" || hideGui=="FALSE"))
                sNoGui = hideGui=="TRUE";

            var lArgs = args.ToList();
            var rpcsArgIndex = lArgs.IndexOf("--rpcs3");
            var outArgIndex = lArgs.IndexOf("--out");
            var noGuiArgIndex = lArgs.IndexOf("--nogui");

            if (rpcsArgIndex>=0 && rpcsArgIndex >= lArgs.Count - 1)
            {
                Console.WriteLine("--rpcs3 argument given, but no value");
                error = true;
            }
            if (outArgIndex >= 0 && outArgIndex >= lArgs.Count - 1)
            {
                Console.WriteLine("--out argument given, but no value");
                error = true;
            }
            if (noGuiArgIndex >= 0 && noGuiArgIndex >= lArgs.Count - 1)
            {
                Console.WriteLine("--nogui argument given, but no value");
                error = true;
            }

            if (rpcsArgIndex >= 0 && rpcsArgIndex < lArgs.Count - 1)
            {
                if (!Directory.Exists(lArgs[rpcsArgIndex + 1]))
                {
                    Console.WriteLine("--rpcs3 directory not valid");
                    error = true;
                }
                else
                    sRpcs3 = lArgs[rpcsArgIndex + 1];
            }
            if (outArgIndex >= 0 && outArgIndex < lArgs.Count - 1)
            {
                if (!Directory.Exists(lArgs[outArgIndex + 1]))
                {
                    Console.WriteLine("--out directory not valid");
                    error = true;
                }
                else
                    sOut = lArgs[outArgIndex + 1];
            }
            if (noGuiArgIndex >= 0 && noGuiArgIndex < lArgs.Count - 1)
            {
                if (lArgs[noGuiArgIndex + 1] != "1" && lArgs[noGuiArgIndex + 1] != "0") { 
                    Console.WriteLine("--nogui has value that is not 1 or 0");
                    error = true;
                }else
                    sNoGui = lArgs[noGuiArgIndex + 1] == "1";
            }

            if (sOut == null || sRpcs3 == null)
                error = true;

            if (error)
            {
                Console.WriteLine("Rpcs3GenerateStubs.exe [--rpcs3 X:\\Path\\To\\Rpcs3\\] [--out X:\\Path\\To\\Place\\Shortcuts\\] [--nogui 0]");
                Console.WriteLine("If values are not configured in Rpcs3GenerateStubsGui --rpcs3 and --out are required.");
                Console.WriteLine("Arguments given override GUI values.");
                Console.WriteLine("nogui defaults to 1 if not given. Use 0 to override enabled in GUI.");
            }
            else
            {
                var rpcs3 = new PS3Utils.RPCS3(sRpcs3);
                var games = rpcs3.GetAllGames();
                PS3Utils.PS3Game currentGame = null;
                try { 
                    foreach (var game in games)
                    {
                        currentGame = game;
                        var settings = rpcs3.GetApplicationTarget(game, sOut, sNoGui.HasValue ? sNoGui.Value : true);
                        PS3Utils.Compiler.CompileLauncherExe(settings);
                    }
                }
                catch (Exception ex)
                {
                    if (currentGame == null)
                        Console.WriteLine("There was an issue creating shortcuts.\r\n" + ex);
                    else
                        Console.WriteLine("There was an issue processing the game " + currentGame.GamePath + "\r\n" + ex);
                }
            }
        }
    }
}
