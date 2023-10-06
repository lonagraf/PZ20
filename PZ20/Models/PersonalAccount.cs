using System;
using System.Runtime.InteropServices.JavaScript;
using MySql.Data.MySqlClient;

namespace PZ20;

public class PersonalAccount
{
    private string _connString = "server=localhost;database=pz18;port=3306;User Id=root;password=IGraf123*";
    public int PersonalAccountID { get; set; }
    public string UserName { get; set; }
    public string GameName { get; set; }
    public DateTime Online { get; set; }
    public void Update(string newUserName, string newGameName, DateTime newOnline)
    {
        UserName = newUserName;
        GameName = newGameName;
        Online = newOnline;
        using (MySqlConnection connection = new MySqlConnection(_connString))
        {
            connection.Open();
            //string sql = "UPDATE pro1_4.students SET surname = @Surname, name = @Name, group_id = @GroupName WHERE id = @Id";
            string sql = "insert into onlinegamestore.personal_account SET UserName = @UserName, GameName = @GameName, Online = @Online WHERE PersonalAccountId = @Id";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UserName", newUserName);
            command.Parameters.AddWithValue("@GameName", newGameName);
            command.Parameters.AddWithValue("@Online", newOnline);
            command.Parameters.AddWithValue("@Id", PersonalAccountID);
            command.ExecuteNonQuery();
        }
    }
}