namespace ProtoGUI.Services.Interfaces
{
    internal interface ICommandPromptService
    {
        void ExecuteCommand(string fileLocation, string file, string outputDir);
    }
}