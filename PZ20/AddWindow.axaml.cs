using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace PZ20;

public partial class AddWindow : Window
{
    private PersonalAccount _personalAccount;
    public AddWindow(/*PersonalAccount personalAccount*/)
    {
        InitializeComponent();
        /*_personalAccount = personalAccount;
        UsernameTextBox.Text = _personalAccount.UserName;
        GameNameTextBox.Text = _personalAccount.GameName;*/
    }

    
    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
       this.Hide();
       MainWindow mainWindow = new MainWindow();
       mainWindow.Show();
    }
    /*private void Button_OnClick1(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }*/
}