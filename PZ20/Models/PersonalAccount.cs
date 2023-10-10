using System;
using System.Runtime.InteropServices.JavaScript;
using MySql.Data.MySqlClient;

namespace PZ20;

public class PersonalAccount
{
    
    public int PersonalAccountID { get; set; }
    public string UserName { get; set; }
    public string GameName { get; set; }
    public string Online { get; set; }
}