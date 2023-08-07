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
    class Connection
    {
        public static MySqlConnection connection = new MySqlConnection();
        static string serverIp, dbName, user, pass;
        
        public void Initialize(string serverIp_in, string dbName_in, string user_in, string pass_in)
        {
            serverIp = serverIp_in;
            dbName = dbName_in;
            user = user_in;
            pass = pass_in;
        }

        public static MySqlConnection dataSource()
        {
            return connection = new MySqlConnection($"server={serverIp}; database={dbName}; Uid={user}; password={pass};");
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
