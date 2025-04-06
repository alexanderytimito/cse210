using static System.Console;
using MySql.Data.MySqlClient;

public class Connection
{
    private string connectionString =  @"server=localhost;
                                        database=myfinaldatabase;
                                        user=root;
                                        password=2619NorseH@MYSQL;";
    private MySqlConnection connection;
    public MySqlConnection OpenConnection()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
        return connection;
    }
    
}