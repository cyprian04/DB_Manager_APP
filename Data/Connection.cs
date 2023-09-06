﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Media;
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
        private ListBox actualizedDbListBox, actualizedTablesListBox;

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

        public void DisplayAvaliableDatabases(ListBox databaseListBox)
        {
            actualizedDbListBox = databaseListBox;

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

                    actualizedDbListBox.ItemsSource = AvailableDatabases;
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

        public void DisplayCurrentDbTables(ListBox tablesListBox)
        {
            actualizedTablesListBox = tablesListBox;

            try
            {
                connection.Open();
                string query = "SHOW TABLES;";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<string> AvailableTables = new List<string>();

                    while (reader.Read())
                    {
                        string tableName = reader.GetString(0);
                        AvailableTables.Add(tableName);
                    }

                    actualizedTablesListBox.ItemsSource = AvailableTables;
                }
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

        public void ExecuteAndCheckSQLQuerry(string query, DataGrid QuerryResultDataGrid, TextBlock TextQuerryResultInfo, Border BorderQuerryResultInfo)
        {
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

                    if(dataTable.Rows.Count != 0 )
                    {
                        QuerryResultDataGrid.ItemsSource = dataTable.DefaultView; // Set DataGrid's ItemsSource
                        QuerryResultDataGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        QuerryResultDataGrid.Visibility = Visibility.Hidden;
                        QuerryResultDataGrid.ItemsSource = null;
                    }

                    if(query.Trim().StartsWith("DROP DATABASE", StringComparison.OrdinalIgnoreCase) || 
                       query.Trim().StartsWith("CREATE DATABASE", StringComparison.OrdinalIgnoreCase))
                    {
                        connection.Close();
                        DisplayAvaliableDatabases(actualizedDbListBox);
                        connection.Open();
                    }

                    TextQuerryResultInfo.Text = "Successfully executed the querry";
                    BorderQuerryResultInfo.Background = Brushes.Green;
                }
                catch (MySqlException ex)
                {
                    QuerryResultDataGrid.Visibility = Visibility.Hidden;

                    string[] Alllines = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    string firstLine = Alllines.FirstOrDefault();

                    TextQuerryResultInfo.Text = firstLine;
                    BorderQuerryResultInfo.Background = Brushes.Red;
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                TextQuerryResultInfo.Text = "Type in SQL querry first!";
                BorderQuerryResultInfo.Background = Brushes.Red;
            }
        }

        public void DisconnectUserFromServer()
        {
            username = null;
            dbName = null;
            password = null;
            hostName = null;
            actualizedDbListBox = null;
            actualizedTablesListBox = null;
            connectionString = null;
            connection = null;
        }
    }
}
