using ProtoGUI.Exceptions;
using ProtoGUI.Services.Interfaces;
using System;
using System.IO;
using System.Windows.Forms;

namespace ProtoGUI.Services
{
    internal class PathBrowserService : IPathBrowserService
    {
        private PathBrowserService() { }
        internal static PathBrowserService Create() => new PathBrowserService();

        public string SelectFile(string fileLocation)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "proto file|*.proto";
            fileDialog.Multiselect = false;
            fileDialog.CheckFileExists = true;
            fileDialog.AddExtension = true;

            if (!string.IsNullOrEmpty(fileLocation))
                fileDialog.InitialDirectory = fileLocation;
            else
                throw new InvalidFileLocationException();

            var res = fileDialog.ShowDialog();

            if (res == DialogResult.Cancel || res == DialogResult.No)
                throw new UserNotChoosePathException();

            return fileDialog.FileName;
        }

        public string SelectPath(string path = null)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (!string.IsNullOrEmpty(path))
            {
                string outDir = $"{path}\\OUT";

                Directory.CreateDirectory(outDir);
                folderBrowserDialog.SelectedPath = outDir;    
            }   
            else
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop; //TODO: add a better path
            var res = folderBrowserDialog.ShowDialog();

            if (res == DialogResult.Cancel || res == DialogResult.No)
                throw new UserNotChoosePathException();

            return folderBrowserDialog.SelectedPath;
        }
    }
}