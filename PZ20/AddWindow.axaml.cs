using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PZ20;

public partial class AddWindow : Window
{
    public AddWindow()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
       this.Hide();
       MainWindow mainWindow = new MainWindow();
       mainWindow.Show();
    }
}