using System;
using System.Windows.Input;
using TodoList.VIewModels;

namespace TodoList.Helpers;

public class GenericCommand : ICommand
{
    private readonly Action<object?> executeAction;
    public event EventHandler? CanExecuteChanged;

    public GenericCommand(Action<object?> executeAction)
    {
        this.executeAction = executeAction;
    }

    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    {
        executeAction?.Invoke(parameter);
    }
}
