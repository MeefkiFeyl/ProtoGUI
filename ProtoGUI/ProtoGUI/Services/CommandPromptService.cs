using ProtoGUI.Services.Interfaces;
using System.Diagnostics;
using System.Windows;

namespace ProtoGUI.Services
{
    internal class CommandPromptService : ICommandPromptService
    {
        private CommandPromptService() { }
        internal static CommandPromptService Create() => new CommandPromptService();

        public void ExecuteCommand(string fileLocation, string file, string outputDir)
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.CreateNoWindow = false;
            cmd.StartInfo.UseShellExecute = false;

            cmd.Start();

            cmd.StandardInput.AutoFlush = true;
            cmd.StandardInput.WriteLine($"protoc {file} --csharp_out={outputDir} --proto_path={fileLocation}");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            cmd.Dispose();
        }
    }
}