using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace WPFToDoList
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Объявляем событие, отвечающее за изменение свойств
        public event PropertyChangedEventHandler PropertyChanged;

        // Инициализируем коллекцию, реализующую функционал Observer
        public ObservableCollection<TodoItem> Items { get; set; } = [];

        // Объявляем интерфейсы для команд
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        // Объявляем строки для задач
        private string _newTaskName;
        public string NewTaskName
        {
            get => _newTaskName;
            set { _newTaskName = value; }
        }

        // Объявляем задачи
        private TodoItem _selectedItem;
        public TodoItem SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; }
        }

        // Конструктор
        public MainViewModel()
        {
            AddCommand = new RelayCommand(_ => AddItem(), _ =>
            !string.IsNullOrWhiteSpace(NewTaskName));

            DeleteCommand = new RelayCommand(_ => DeleteItem(), _ =>
            SelectedItem != null);
        }

        // Метод для добавления Item в Items
        private void AddItem()
        {
            // Добавляем Item
            Items.Add(new TodoItem { TaskName = NewTaskName });

            // Сбрасываем значение NewTaskName
            NewTaskName = string.Empty;
        }

        private void DeleteItem() => Items.Remove(SelectedItem);

        // Метод, наблюдающий за тем, можем ли мы выполнить то или иное действие
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
