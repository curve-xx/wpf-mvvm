using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using TodoList.Helpers;
using TodoList.Models;

namespace TodoList.VIewModels;

public class TodoViewModel : ChangNotifier
{
    public string Title { get { return $"Todo({todoModels.Count(x => !x.IsDone)}) Completed({todoModels.Count(x => x.IsDone)})"; } }
    private ObservableCollection<TodoModel> todoModels = new ObservableCollection<TodoModel>();

    public ObservableCollection<TodoModel> TodoModels
    {
        get { return todoModels; }
        set { todoModels = value; }
    }  

    private string? _newTodo;

    public string? NewTodo
    {
        get { return _newTodo; }
        set
        {
            _newTodo = value;
            OnPropertyChanged(nameof(NewTodo));
        }
    }
    
    public ICommand AddCommand { get; set; }

    public TodoViewModel()
    {
        AddCommand = new GenericCommand(AddTodo);
    }

    internal void AddTodo(object? parameter)
    {
        if (!string.IsNullOrWhiteSpace(NewTodo))
        {
            TodoModel todoModel = new TodoModel { Title = NewTodo, IsDone = false };
            todoModel.PropertyChanged+=ToDo_PropertyChanged;
            TodoModels.Add(todoModel);
            NewTodo = string.Empty;
            OnPropertyChanged(nameof(Title)); 
        }
    }

    private void ToDo_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
         OnPropertyChanged(nameof(Title)); 
    }
}
