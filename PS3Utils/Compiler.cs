using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Drawing;
using System.IO;

namespace PS3Utils
{
    public class Compiler
    {
        public static void CompileWinExe(string src, string icon, string filename)
        {
                var opt = new CompilerParameters();
                opt.GenerateInMemory = false;
                opt.GenerateExecutable = true;
                opt.OutputAssembly = filename;
                opt.ReferencedAssemblies.Add("System.dll");
                opt.ReferencedAssemblies.Add("System.Diagnostics.Process.dll");
                opt.CompilerOptions = @"/target:winexe /win32icon:" + icon;
                CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(opt, src);
        }

        internal static string EscapeCString(string input)
        {
                using (var writer = new StringWriter())
                {
                    using (var provider = CodeDomProvider.CreateProvider("CSharp"))
                    {
                        provider.GenerateCodeFromExpression(new CodePrimitiveExpression(input), writer, null);
                        return writer.ToString();
                    }
                }
        }
        public static void CompileLauncherExe(string outPath, string iconPath, string appPath, string workingPath, string args)
        {

            var src = @"
public class App {
    public static void Main() {
        var info = new System.Diagnostics.ProcessStartInfo(" + EscapeCString(appPath) + @", "+ EscapeCString(args) +@");
        info.WorkingDirectory = " + EscapeCString(workingPath) + @";
        var f = System.Diagnostics.Process.Start(info);
        f.WaitForExit();
    }
}";
            CompileWinExe(src, iconPath, outPath);
        }

        public static void CompileLauncherExe(ApplicationTarget settings)
        {
            var iconPath = Path.GetTempFileName();
            using (var iconWriter = new FileStream(iconPath, FileMode.Create))
            {
                settings.AppIcon.Save(iconWriter);
            }
            CompileLauncherExe(settings.OutName, iconPath, settings.Binary, settings.WorkingDirectory, settings.Arguments);
            try
            {
                File.Delete(iconPath);
            }
            catch (Exception) { }
        }
    }
    public class ApplicationTarget
    {
        public string OutName;
        public string Binary;
        public string WorkingDirectory;
        public string Arguments;
        public Icon AppIcon;
    }
}
