using System.Windows.Controls;
using TodoList.VIewModels;

namespace TodoList.Views;

// The class name must match the x:Class in the XAML
public partial class TodoView : UserControl
{
    public TodoView()
    {
        InitializeComponent();
        DataContext = new TodoViewModel();
    }
}
