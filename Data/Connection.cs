using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.IO;
using System.Security;
using System.Runtime.InteropServices;

namespace GUI_Database_app.Data
{
    /// <summary>
    /// Logika połączenia z DB
    /// </summary>
    public class Connection
    { 
        public enum TypeOfQuerry
        { 
            defaultQuerry,
            ShowStruct,
            ShowData
        }

        private static MySqlConnection connection = new MySqlConnection();
        private string connectionString;
        private static string dbName, tbName;
   
        private ListBox actualizedDbListBox, actualizedTablesListBox;

        public string DbName {get => dbName; set => dbName = value;}
             
        public string TbName { get => tbName; set => tbName = value; }

        public bool VerifyCredentials(string Host, string Username, SecureString Password)
        {
            string pass;
            if (Password != null)
            {
                IntPtr unmanagedString = IntPtr.Zero;
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(Password);
                pass = Marshal.PtrToStringUni(unmanagedString);
            }
            else pass = null;
            connectionString = $"Server={Host};Uid={Username};Password={pass};";

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                return true;
                
            }
            catch (MySqlException)
            { 
                connectionString = null;
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        public void DisplayCurrentListBox(ListBox ListBox_in)
        {
            string query = null;

            if(ListBox_in.Name is "DatabasesListBox")
            {
                actualizedDbListBox = ListBox_in;
                query = "SHOW DATABASES;";
            }
            else
            {
                actualizedTablesListBox = ListBox_in;
                query = "SHOW TABLES;";
            } 
            try
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<string> content = new List<string>();

                    while (reader.Read())
                    {
                        string element = reader.GetString(0);
                        content.Add(element);
                    }

                    if(ListBox_in.Name is "DatabasesListBox") actualizedDbListBox.ItemsSource = content;
                    else  actualizedTablesListBox.ItemsSource = content;
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

        public void ExecuteAndCheckSQLQuerry(TypeOfQuerry type, string query, DataGrid QuerryResultDataGrid, TextBlock TextQuerryResultInfo = null, Border BorderQuerryResultInfo = null)
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

                    switch(type)
                    {
                        case TypeOfQuerry.defaultQuerry: // for executing querries from SQLControl

                            if (dataTable.Rows.Count != 0)
                            {
                                QuerryResultDataGrid.ItemsSource = dataTable.DefaultView; // Set DataGrid's ItemsSource
                                QuerryResultDataGrid.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                QuerryResultDataGrid.Visibility = Visibility.Hidden;
                                QuerryResultDataGrid.ItemsSource = null;
                            }

                            if (query.Trim().StartsWith("DROP DATABASE", StringComparison.OrdinalIgnoreCase) ||
                               query.Trim().StartsWith("CREATE DATABASE", StringComparison.OrdinalIgnoreCase))
                            {
                                connection.Close();
                                DisplayCurrentListBox(actualizedDbListBox);
                                connection.Open();
                            }

                            if (query.Trim().StartsWith("DROP TABLE", StringComparison.OrdinalIgnoreCase) ||
                               query.Trim().StartsWith("CREATE TABLE", StringComparison.OrdinalIgnoreCase))
                            {
                                connection.Close();
                                DisplayCurrentListBox(actualizedTablesListBox);
                                connection.Open();
                            }

                            TextQuerryResultInfo.Text = "Successfully executed the querry";
                            BorderQuerryResultInfo.Background = Brushes.Green;

                            break;

                        case TypeOfQuerry.ShowData: // in case when one of the buttons in StructureControl is pressed
                        case TypeOfQuerry.ShowStruct:

                            QuerryResultDataGrid.ItemsSource = dataTable.DefaultView; 
                            QuerryResultDataGrid.Visibility = Visibility.Visible;

                            break;
                    }
                }
                catch (MySqlException ex)
                {
                    if (type == TypeOfQuerry.defaultQuerry)
                    {
                        QuerryResultDataGrid.Visibility = Visibility.Hidden;

                        string[] Alllines = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                        TextQuerryResultInfo.Text = Alllines.FirstOrDefault();
                        BorderQuerryResultInfo.Background = Brushes.Red;
                    }
                    else
                        MessageBox.Show("Can't display content for table");
                }
                catch(Exception ex)
                {
                    string[] Alllines = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                    MessageBox.Show(Alllines.FirstOrDefault());
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

        public void ImportDB(string scriptPath)
        {
            try
            {
                connection.Open();

                string sqlScript = File.ReadAllText(scriptPath);

                using (MySqlCommand cmd = new MySqlCommand(sqlScript, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Database and tables created successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ExportDB(string chosenDB)
        {
            try
            {
                connection.Open();
                string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
                string exportPath = Path.Combine(downloadsFolder, $"{chosenDB}.sql");

                // Get the list of tables in the database
                DataTable tableList = new DataTable();
                using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES", connection))
                {
                    tableList.Load(cmd.ExecuteReader());
                }

                // Write the database schema to the export file
                using (StreamWriter writer = new StreamWriter(exportPath))
                {
                    writer.WriteLine("-- Database export for: " + dbName);
                    writer.WriteLine("-- Timestamp: " + DateTime.Now);
                    writer.WriteLine();

                    foreach (DataRow row in tableList.Rows)
                    {
                        string tableName = row[0].ToString();

                        // Retrieve table structure
                        using (MySqlCommand schemaCmd = new MySqlCommand($"SHOW CREATE TABLE {tableName}", connection))
                        {
                            using (MySqlDataReader schemaReader = schemaCmd.ExecuteReader())
                            {
                                if (schemaReader.Read())
                                {
                                    writer.WriteLine(schemaReader.GetString(1) + ";");
                                    writer.WriteLine();
                                }
                            }
                        }

                        // Retrieve and export table data
                        using (MySqlCommand dataCmd = new MySqlCommand($"SELECT * FROM {tableName}", connection))
                        {
                            using (MySqlDataReader dataReader = dataCmd.ExecuteReader())
                            {
                                writer.WriteLine($"-- Data for table: {tableName}");
                                writer.WriteLine();

                                while (dataReader.Read())
                                {
                                    writer.Write($"INSERT INTO `{tableName}` (");

                                    // Write column names
                                    for (int i = 0; i < dataReader.FieldCount; i++)
                                    {
                                        writer.Write($"`{dataReader.GetName(i)}`");
                                        if (i < dataReader.FieldCount - 1)
                                            writer.Write(", ");
                                    }
                                    writer.WriteLine(") VALUES (");

                                    // Write column values
                                    for (int i = 0; i < dataReader.FieldCount; i++)
                                    {
                                        writer.Write($"'{EscapeSqlString(dataReader[i].ToString())}'");
                                        if (i < dataReader.FieldCount - 1)
                                            writer.Write(", ");
                                    }
                                    writer.WriteLine(");");
                                }
                                writer.WriteLine();
                            }
                        }
                    }
                }
                MessageBox.Show($"Database exported successfully to: {exportPath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private string EscapeSqlString(string value)
        {
            return value.Replace("'", "''");
        }

        public void DisconnectUserFromServer()
        {
            dbName = null;
            tbName = null;      
            actualizedDbListBox = null;
            actualizedTablesListBox = null;
            connectionString = null;
            connection = null;
        }
    }
}
