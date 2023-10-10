using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace PZ20;

public partial class EditWindow : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_4;port=3306;User Id=user_01;password=user01pro";
    private PersonalAccount _personalAccount;
    public EditWindow(PersonalAccount personalAccount)
    {
        _personalAccount = personalAccount;
        InitializeComponent();
        Icon = new WindowIcon("icons/file-edit.png");
        UsernameTextBox.Text = _personalAccount.UserName;
        GameNameTextBox.Text = _personalAccount.GameName;
        OnlineTextBox.Text = _personalAccount.Online;
    }
    private void BackBtnOnClick(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }
    private void EditBtnOnClick(object? sender, RoutedEventArgs e)
    {
        string userName = UsernameTextBox.Text;
        string gameName = GameNameTextBox.Text;
        string online = OnlineTextBox.Text;
        /*string userName = _personalAccount.UserName;
        string gameName = _personalAccount.GameName;
        string online = _personalAccount.Online;*/
        int id = _personalAccount.PersonalAccountID;
        using (MySqlConnection connection = new MySqlConnection(_connString))
        {
            connection.Open();
            string sql = "UPDATE pro1_4.PersonalAccount SET `User` = @User, Game = @Game, Online = @Online WHERE PersonalAccountID = @Id";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@User", userName);
            command.Parameters.AddWithValue("@Game", gameName);
            command.Parameters.AddWithValue("@Online", online);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
        this.Close(true);
    }
}