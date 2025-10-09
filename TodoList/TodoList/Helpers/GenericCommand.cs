using System;
using System.Windows.Input;
using TodoList.VIewModels;

namespace TodoList.Helpers;

public class AddCommand : ICommand
{
    private readonly TodoViewModel todoViewModel;
    public event EventHandler? CanExecuteChanged;

    public AddCommand(TodoViewModel todoViewModel)
    {
        this.todoViewModel = todoViewModel;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        todoViewModel.AddTodo(parameter);
    }
}
