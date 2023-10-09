using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace PZ20;

public partial class UserWindow : Window
{
    private string _connString = "server=localhost;database=onlinegamestore;port=3306;User Id=root;password=IGraf123*";
    private List<User> _users;
    private MySqlConnection _connection;
    public UserWindow()
    {
        InitializeComponent();
        Icon = new WindowIcon("icons\\user.png");
        string fullTable = "select * from User";
        ShowTable(fullTable);
    }
    public void ShowTable(string sql)
    {
        _users = new List<User>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentUser = new User()
            {
                UserID = reader.GetInt32("UserID"),
                UserName = reader.GetString("UserName"),
                DateOfBirth = reader.GetDateTime("DateOfBirth"),
                RegistrationDate = reader.GetDateTime("RegistrationDate")
            };
            _users.Add(currentUser);
        }

        _connection.Close();
        UserGrid.ItemsSource = _users;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }
}