using System;
using System.Windows.Input;

namespace WPFToDoList
{
    public class RelayCommand : ICommand
    {
        // Для реализации параметров из ICommand
        private readonly Action<object> _execute;       // Делегат, возвращающий void
        private readonly Predicate<object> _canExecute; // Делегат, возвращающий bool

        // Конструктор для команд, которые могут всегда выполняться
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Метод, который WPF вызывает для проверки, может ли команда выполниться
        // в данный момент.
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        // Метод, который WPF вызывает при непосредственном выполнении
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        // Событие, уведомляющее систему о том, что изменилась возможность выполнения.
        // Например, CanExecute мог вернуть false, а теперь стал возвращать true.
        // Вызов CommandManager.RequerySuggested() заставляет WPF "переспросить"
        // у всех команд их состояние CanExecute.

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}
