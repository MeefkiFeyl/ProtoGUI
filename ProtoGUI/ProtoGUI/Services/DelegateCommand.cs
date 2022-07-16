using System;
using System.Windows.Input;

namespace ProtoGUI.Services
{
    class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter != null && this._canExecute != null)
                return this._canExecute(parameter);

            return true;
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }
    }
}