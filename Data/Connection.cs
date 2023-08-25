using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GUI_Database_app.Data
{
    /// <summary>
    /// Logika połączenia z DB
    /// </summary>
    public class Connection
    {
        public static MySqlConnection connection = new MySqlConnection();
        private static string dbName, username, password, serverIp;

        public static string DbName {get => dbName; set => dbName = value;}

        public static string User {get => username; set => username = value;}

        public static string Password {get => password; set => password = value; }

        public static string ServerIp {get => serverIp; set => serverIp = value;}
    
        public void Initialize(string serverIp_in, string user_in, string pass_in)
        {
            serverIp = serverIp_in;
            username = user_in;
            password = pass_in;
        }

        public bool VerifyCredentials()
        {
            string connectionString = $"Server={serverIp};Uid={username};Password={password};";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    return true;
                }
            }
            catch (MySqlException)
            {
                return false;
            }
        }

        public static MySqlConnection dataSource()
        {                                               
            return connection = new MySqlConnection($"server={serverIp}; database={dbName}; Uid={username}; password={password};");
        }
        public void connOpen()
        {
            connection.Open();
        }
        public void connClose()
        {
           connection.Close();
        }
    }
}
