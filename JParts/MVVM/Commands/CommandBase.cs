using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace JParts.MVVM.Commands
{
    public abstract class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public abstract void Execute(object parameter);

        protected void OnCanExcectutechanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
