using System;
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
    public string fullTable =
        "select PersonalAccountID, UserName, GameName, Online from onlinegamestore.PersonalAccount " +
        "join onlinegamestore.User u on u.UserID = PersonalAccount.User " +
        "join onlinegamestore.Game g on g.GameID = PersonalAccount.Game;";

    public MainWindow()
    {
        InitializeComponent();
        Icon = new WindowIcon("icons\\home.png");
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
                Online = reader.GetString("Online"),
                UserName = reader.GetString("UserName")
            };
            _personalAccount.Add(currentAccount);
        }

        _connection.Close();
        OnlineGameStoreGrid.ItemsSource = _personalAccount;
    }
    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<PersonalAccount> search =
            _personalAccount.Where(x => x.GameName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
        OnlineGameStoreGrid.ItemsSource = search;
    }

    private void MenuItem_OnClickAdd(object? sender, RoutedEventArgs e)
    {
        /*this.Hide();
            AddWindow addWindow = new AddWindow();
            addWindow.Show();*/
        AddWindow addWindow = new AddWindow();
        addWindow.ShowDialog(this);
        ShowTable(fullTable);
    }
    private void MenuItem_OnClickEdit(object? sender, RoutedEventArgs e)
    {
        PersonalAccount selectedAccount = OnlineGameStoreGrid.SelectedItem as PersonalAccount;
        if (selectedAccount != null)
        {
            EditWindow editWindow = new EditWindow(selectedAccount);
            editWindow.Show();
            ShowTable(fullTable);
        }
        else
        {
            Console.WriteLine("Выберите строку для редактирования!!!");
        }
    }

    public void Delete(int id)
    {
        using (MySqlConnection connection = new MySqlConnection(_connString))
        {
            connection.Open();
            string sql = "delete from onlinegamestore.PersonalAccount where PersonalAccountID = @PersonalAccountID";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@PersonalAccountID", id);
            command.ExecuteNonQuery();
        }
    }

    private void MenuItem_OnClickDelete(object? sender, RoutedEventArgs e)
    {
        PersonalAccount selectedAccount = OnlineGameStoreGrid.SelectedItem as PersonalAccount;
        if (selectedAccount != null)
        {
            Delete(selectedAccount.PersonalAccountID);
            ShowTable(fullTable);
        }
        else
        {
            Console.WriteLine("Выберите строку для удаления!!!");
        }
    }

    private void Button_OnClickUserWindow(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        UserWindow userWindow = new UserWindow();
        userWindow.Show();
    }

    private void Button_OnClickGameWindow(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        GameWindow gameWindow = new GameWindow();
        gameWindow.Show();
    }
    
}