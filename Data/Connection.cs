using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;

namespace GUI_Database_app.Data
{
    /// <summary>
    /// Logika połączenia z DB
    /// </summary>
    public class Connection
    {
        public static MySqlConnection connection = new MySqlConnection();
        private string connectionString;
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

        public void DisplayAvaliableDatabases(ListBox databaseComboBox)
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

        public bool ExecuteAndCheckSQLQuerry(string query, DataGrid QuerryresultDataGrid)
        {
            bool success = false;
            if (!string.IsNullOrWhiteSpace(query)) 
            { 
                try
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    DataTable dataTable = new DataTable(); // special object from System.Data that stores data
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        dataTable.Load(reader); // Load data into DataTable from reader
                    }

                    QuerryresultDataGrid.ItemsSource = dataTable.DefaultView; // Set DataGrid's ItemsSource
                    success = true;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                    success = false;
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("Type in SQL querry first!");
                success = false;
            }

            return success;
        }

        public void DisconnectUserFromServer()
        {
            username = null;
            dbName = null;
            password = null;
            hostName = null;
            connectionString = null;
            connection = null;
        }
    }
}
