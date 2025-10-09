using System;
using System.ComponentModel;
using TodoList.Helpers;

namespace TodoList.Models;

public class TodoModel : ChangNotifier
{
    private string? _title;
    public string? Title
    {
        get { return _title; }
        set {
            _title = value; 
            OnPropertyChanged(nameof(Title));}
    }

    private bool _isDone;

    public bool IsDone
    {
        get { return _isDone; }
        set {
            _isDone = value; 
            OnPropertyChanged(nameof(IsDone));}
    }    
}
