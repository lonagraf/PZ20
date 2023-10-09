﻿using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace PZ20;

public partial class AddWindow : MainWindow
{
    private string _connString = "server=localhost;database=onlinegamestore;port=3306;User Id=root;password=IGraf123*";
    public AddWindow()
    {
        InitializeComponent();
        Icon = new WindowIcon("icons\\square-plus.png");
    }

    private void BackBtnOnClick(object? sender, RoutedEventArgs e)
    {
       this.Hide();
       MainWindow mainWindow = new MainWindow();
       mainWindow.Show();
    }

    private void AddBtnOnClick(object? sender, RoutedEventArgs e)
    {
        string userName = UsernameTextBox.Text;
        string gameName = GameNameTextBox.Text;
        string online = OnlineTextBox.Text;
        using (MySqlConnection connection = new MySqlConnection(_connString))
        {
            connection.Open();
            string sql =
                "INSERT INTO personalaccount (User, Game, Online) VALUES (@User, @Game, @Online)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@User", userName);
            command.Parameters.AddWithValue("@Game", gameName);
            command.Parameters.AddWithValue("@Online", online);
            command.ExecuteNonQuery();
        }
        this.Close(true);
    }
}