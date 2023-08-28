using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;

namespace GUI_Database_app.Data
{
    /// <summary>
    /// Logika połączenia z DB
    /// </summary>
    public class Connection
    {
        public static MySqlConnection connection = new MySqlConnection();
        string connectionString;
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
            connectionString = $"Server={hostName};Uid={username};Password={password};";

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                return true;
                
            }
            catch (MySqlException)
            {
                username = "";
                password = "";
                hostName = "";
                connectionString = "";

                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public void DisplayAvaliableDatabases(System.Windows.Controls.ComboBox databaseComboBox)
        {
            try
            {
                connection.Open();
                string query = "SHOW DATABASES;";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<string> AvailableDatabases = new List<string>();

                    while (reader.Read())
                    {
                        string databaseName = reader.GetString(0);
                        AvailableDatabases.Add(databaseName);
                    }

                    databaseComboBox.ItemsSource = AvailableDatabases;
                }
            }
            catch(MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        public void ConnectionWithDb(string DbName_in)
        {
            if (DbName != DbName_in)
            {
                DbName = DbName_in;

                try
                {
                    connection.Open();
                    connection.ChangeDatabase(DbName); // changes db
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
            }
            else
                MessageBox.Show("Already connected to this database");
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
