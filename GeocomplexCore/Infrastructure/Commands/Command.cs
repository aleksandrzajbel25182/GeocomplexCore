using System;
using System.Windows.Input;

namespace GeocomplexCore.Infrastructure.Commands
{
    internal abstract class Command : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        // Функция которая возвращает истину/ложь 
        public abstract bool CanExecute(object parameter);

        // То что должно быть выполнено самой командой
        public abstract void Execute(object parameter);
    }
}
