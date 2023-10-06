using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace PZ20;

public partial class GameWindow : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_4;port=3306;User Id=user_01;password=user01pro";
    private List<Game> _games;
    private MySqlConnection _connection;
    public GameWindow()
    {
        InitializeComponent();
        string fullTable = "select GameID, GameName, Price, Developer from Game " +
                           "join pro1_4.Developer d on d.DeveloperID = Game.Developer;";
        ShowTable(fullTable);
    }
    public void ShowTable(string sql)
    {
        _games = new List<Game>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentGame = new Game()
            {
                GameID = reader.GetInt32("GameID"),
                GameName = reader.GetString("GameName"),
                Price = reader.GetDouble("Price"),
                Developer = reader.GetString("Developer")
            };
            _games.Add(currentGame);
        }

        _connection.Close();
        GameGrid.ItemsSource = _games;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }
}