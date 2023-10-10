using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace PZ20;

public partial class MainWindow : Window
{
    private string _connString = "server=10.10.1.24;database=pro1_4;port=3306;User Id=user_01;password=user01pro";
    private List<PersonalAccount> _personalAccount;
    private MySqlConnection _connection;
    public string fullTable =
        "select PersonalAccountID, UserName, GameName, Online from pro1_4.PersonalAccount " +
        "join pro1_4.User u on u.UserID = PersonalAccount.User " +
        "join pro1_4.Game g on g.GameID = PersonalAccount.Game;";

    public MainWindow()
    {
        InitializeComponent();
        Icon = new WindowIcon("icons/home.png");
        ShowTable(fullTable);
    }

    public void ShowTable(string sql)
    {
        _personalAccount = new List<PersonalAccount>(); //создается новый список
        _connection = new MySqlConnection(_connString); //создается объект соединения с БД
        _connection.Open(); //Открывается соединение с БД
        MySqlCommand command = new MySqlCommand(sql, _connection); //создается объект который будет выполнять запросы
        MySqlDataReader reader = command.ExecuteReader(); //выполняется запрос (ExecuteReader:выполняет sql-выражение и возвращает строки из таблицы)
        while (reader.Read() && reader.HasRows) //цикл
        {
            var currentAccount = new PersonalAccount() //создается новый объект PersonalAccount
            {
                PersonalAccountID = reader.GetInt32("PersonalAccountID"),
                GameName = reader.GetString("GameName"),
                Online = reader.GetString("Online"),
                UserName = reader.GetString("UserName")
            };
            _personalAccount.Add(currentAccount); //currentAccount добавляется в список
        }
        _connection.Close(); //закрывается соединение с БД
        OnlineGameStoreGrid.ItemsSource = _personalAccount; //datagrid использует данные из списка для отображения
    }
    private void TxtSearch_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        List<PersonalAccount> search = //создается список search который содержит данные списка PersonalAccount и хранит результы поиска
            _personalAccount.Where(x => x.GameName.ToLower().Contains(txtSearch.Text.ToLower())).ToList(); //проверяет содержит ли GameName введеную строку 
        OnlineGameStoreGrid.ItemsSource = search; //datagrid использует данные из списка для отображения
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
        using (MySqlConnection connection = new MySqlConnection(_connString)) //создается объект соединения с БД
        {
            connection.Open(); //Открывается соединение с БД
            string sql = "delete from pro1_4.PersonalAccount where PersonalAccountID = @PersonalAccountID"; //sql запрос на удаление данных
            MySqlCommand command = new MySqlCommand(sql, connection); //создается объект который будет выполнять запросы
            command.Parameters.AddWithValue("@PersonalAccountID", id); //в запрос добавляется параметр с значением id
            command.ExecuteNonQuery(); //ExecuteNonQuery() используется для выполнения SQL-запросов, которые не возвращают результаты, но изменяют данные в БД.
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