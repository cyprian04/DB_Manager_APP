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
        private static string dbName, username, password, hostName;

        public string DbName {get => dbName; set => dbName = value;}
              
        public string Username {get => username; set => username = value;}

        public string Password {get => password; set => password = value; }
              
        public string ServerIp {get => hostName; set => hostName = value;}
    
        public void Initialize(string serverIp_in, string user_in, string pass_in)
        {
            hostName = serverIp_in;
            username = user_in;
            password = pass_in;
        }

        public bool VerifyCredentials()
        {
            string connectionString = $"Server={hostName};Uid={username};Password={password};";

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
                username = "";
                password = "";
                hostName = "";

                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public static MySqlConnection ConnectionWithDb()
        {                                               
            return connection = new MySqlConnection($"server={hostName}; database={dbName}; Uid={username}; password={password};");
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
