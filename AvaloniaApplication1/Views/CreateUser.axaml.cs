using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

public partial class CreateUser : Window
{
    public CreateUser()
    {
        InitializeComponent();
        (DataContext as CreateUserViewModel).SetCloseAction(Close);
    }
}