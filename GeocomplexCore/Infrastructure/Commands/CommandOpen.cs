using GeocomplexCore.ViewsModel.WindowsVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GeocomplexCore.Infrastructure.Commands
{
    abstract class CommandOpen : ICommand
    {
        protected MainWindowViewModel _mainWindowViewModel;

        public CommandOpen(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);
    }
}
