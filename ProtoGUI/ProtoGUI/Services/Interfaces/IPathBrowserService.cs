namespace ProtoGUI.Services.Interfaces
{
    internal interface IPathBrowserService
    {
        string SelectPath(string path = null);
        string SelectFile(string fileLocation);
    }
}