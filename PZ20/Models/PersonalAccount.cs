using System;
using System.Runtime.InteropServices.JavaScript;
using MySql.Data.MySqlClient;

namespace PZ20;

public class PersonalAccount
{
    private string _connString = "server=localhost;database=onlinegamestore;port=3306;User Id=root;password=IGraf123*";
    public int PersonalAccountID { get; set; }
    public string UserName { get; set; }
    public string GameName { get; set; }
    public string Online { get; set; }
}