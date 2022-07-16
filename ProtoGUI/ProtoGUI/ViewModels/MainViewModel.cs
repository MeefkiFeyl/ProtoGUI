using ProtoGUI.Exceptions;
using ProtoGUI.Services;
using ProtoGUI.Services.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace ProtoGUI.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        private readonly ICommandPromptService _cmdService;
        private readonly IPathBrowserService _pathBrowseService;

        public virtual bool SelectFileIsEnable => !string.IsNullOrEmpty(FileLocation);
        public virtual bool SelectOutputDirIsEnable => !string.IsNullOrEmpty(File) && !string.IsNullOrEmpty(FileLocation);
        public virtual bool CompileIsEnable => !string.IsNullOrEmpty(FileLocation) && !string.IsNullOrEmpty(File) && !string.IsNullOrEmpty(OutputDir);

        public virtual string FileLocation { get; private set; }

        public virtual string File { get; private set; }
        public virtual string OutputDir { get; private set; }

        public MainViewModel()
        {
            _cmdService = CommandPromptService.Create();
            _pathBrowseService = PathBrowserService.Create();
        }

        #region Commands

        public ICommand SelectFileLocation => new DelegateCommand(obj =>
        {
            try
            {
                FileLocation = _pathBrowseService.SelectPath();
                RisePropertyChanged(new string[] { nameof(FileLocation), nameof(SelectFileIsEnable), nameof(SelectOutputDirIsEnable), nameof(CompileIsEnable) });
            }
            catch (UserNotChoosePathException) { }
        });

        public ICommand SelectFile => new DelegateCommand(obj =>
        {
            try
            {
                if (SelectFileIsEnable)
                {
                    File = _pathBrowseService.SelectFile(FileLocation);
                    RisePropertyChanged(new string[] { nameof(File), nameof(SelectOutputDirIsEnable), nameof(CompileIsEnable) });
                }
                else
                    throw new InvalidFileLocationException();
            }
            catch (InvalidFileLocationException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (UserNotChoosePathException) { }
            catch (Exception) { }
        });

        public ICommand SelectOutputDir => new DelegateCommand(obj =>
        {
            try
            {
                OutputDir = _pathBrowseService.SelectPath(FileLocation);
                RisePropertyChanged(new string[] { nameof(OutputDir), nameof(CompileIsEnable) });
            }
            catch (UserNotChoosePathException) { }
        });

        public ICommand ExecuteCommand => new DelegateCommand(obj =>
        {
            try
            {
                _cmdService.ExecuteCommand(FileLocation, File, OutputDir);
                MessageBox.Show("Compile finished succesfuly!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something was wrong!\n{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });

        #endregion

        private void RisePropertyChanged(params string[] properties)
        {
            foreach (string property in properties)
                OnPropertyChanged(property);
        }
    }
}
