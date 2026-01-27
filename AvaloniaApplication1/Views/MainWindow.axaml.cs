using Avalonia.Controls;
using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        Opened += (sender, args) =>
        {

            (DataContext as MainWindowViewModel).SetCloseAction(Close);
        };
    }
}