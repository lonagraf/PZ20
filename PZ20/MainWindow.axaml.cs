using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace PZ20;

public partial class MainWindow : Window
{
    private string _connString = "server=localhost;database=onlinegamestore;port=3306;User Id=root;password=IGraf123*";
    private List<PersonalAccount> _personalAccount;
    private MySqlConnection _connection;

    public MainWindow()
    {
        InitializeComponent();
        string fullTable =
            "select PersonalAccountID, UserName, GameName, Online from onlinegamestore.personal_account " +
            "join onlinegamestore.user u on u.UserID = personal_account.User " +
            "join onlinegamestore.game g on g.GameID = personal_account.Game;";
        ShowTable(fullTable);
    }

    public void ShowTable(string sql)
    {
        _personalAccount = new List<PersonalAccount>();
        _connection = new MySqlConnection(_connString);
        _connection.Open();
        MySqlCommand command = new MySqlCommand(sql, _connection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentAccount = new PersonalAccount()
            {
                PersonalAccountID = reader.GetInt32("PersonalAccountID"),
                GameName = reader.GetString("GameName"),
                Online = reader.GetDateTime("Online"),
                UserName = reader.GetString("UserName")
            };
            _personalAccount.Add(currentAccount);
        }

        _connection.Close();
        OnlineGameStoreGrid.ItemsSource = _personalAccount;
    }

    private void MenuItem_OnClickUser(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        UserWindow userWindow = new UserWindow();
        userWindow.Show();
    }

    private void MenuItem_OnClickGame(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        GameWindow gameWindow = new GameWindow();
        gameWindow.Show();
    }

    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<PersonalAccount> search =
            _personalAccount.Where(x => x.GameName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
        OnlineGameStoreGrid.ItemsSource = search;
    }

    private void MenuItem_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        AddWindow addWindow = new AddWindow();
        addWindow.Show();
    }

}