using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;

namespace GUI_Database_app.Data
{
    class DBServerContent
    {
        private Connection connection;
        public enum TypeOfQuerry
        {
            defaultQuerry,
            ShowStruct,
            ShowData
        }

        public DBServerContent(Connection connection)
        {
            this.connection = connection;
        }
        //private ListBox actualizedDbListBox, actualizedTablesListBox;

        public void DisplayCurrentListBox(ObservableCollection<string> collection, string CurrentContext)
        {
            string query = CurrentContext == "Databases" ? "SHOW DATABASES;" : "SHOW TABLES;";

            try
            {
                connection.OpenConn();
                MySqlCommand cmd = new MySqlCommand(query, connection.MySqlConn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<string> content = new List<string>();

                    while (reader.Read())
                    {
                        string element = reader.GetString(0);
                        content.Add(element);
                    }

                    // Update the content of the passed ObservableCollection
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        collection.Clear();
                        foreach (var item in content)
                        {
                            collection.Add(item);
                        }
                    });
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                connection.CloseConn();
            }
        }

        public void ExecuteAndCheckSQLQuerry(TypeOfQuerry type, string query, DataGrid QuerryResultDataGrid, TextBlock TextQuerryResultInfo = null, Border BorderQuerryResultInfo = null)
       {
           if (!string.IsNullOrWhiteSpace(query))
           {
               try
               {
                   connection.OpenConn();
                   MySqlCommand cmd = new MySqlCommand(query, connection.MySqlConn);
      
                   DataTable dataTable = new DataTable(); // special object from System.Data that stores data
                   using (MySqlDataReader reader = cmd.ExecuteReader())
                   {
                       dataTable.Load(reader); // Load data into DataTable from reader
                   }
      
                   switch (type)
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
                               connection.CloseConn();
                               //DisplayCurrentListBox(actualizedDbListBox);
                               connection.OpenConn();
                           }
      
                           if (query.Trim().StartsWith("DROP TABLE", StringComparison.OrdinalIgnoreCase) ||
                              query.Trim().StartsWith("CREATE TABLE", StringComparison.OrdinalIgnoreCase))
                           {
                               connection.CloseConn();
                               //DisplayCurrentListBox(actualizedTablesListBox);
                               connection.OpenConn();
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
               catch (Exception ex)
               {
                   string[] Alllines = ex.Message.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                   MessageBox.Show(Alllines.FirstOrDefault());
               }
               finally
               {
                   connection.CloseConn();
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
               connection.OpenConn();
      
               string sqlScript = File.ReadAllText(scriptPath);
      
               using (MySqlCommand cmd = new MySqlCommand(sqlScript, connection.MySqlConn))
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
               connection.CloseConn();
           }
       }
      
       public void ExportDB(string chosenDB)
       {
           try
           {
               connection.OpenConn();
               string downloadsFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
               string exportPath = Path.Combine(downloadsFolder, $"{chosenDB}.sql");
      
               // Get the list of tables in the database
               DataTable tableList = new DataTable();
               using (MySqlCommand cmd = new MySqlCommand("SHOW TABLES", connection.MySqlConn))
               {
                   tableList.Load(cmd.ExecuteReader());
               }
      
               // Write the database schema to the export file
               using (StreamWriter writer = new StreamWriter(exportPath))
               {
                   writer.WriteLine("-- Database export for: " + connection.DbName);
                   writer.WriteLine("-- Timestamp: " + DateTime.Now);
                   writer.WriteLine();
      
                   foreach (DataRow row in tableList.Rows)
                   {
                       string tableName = row[0].ToString();
      
                       // Retrieve table structure
                       using (MySqlCommand schemaCmd = new MySqlCommand($"SHOW CREATE TABLE {tableName}", connection.MySqlConn))
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
                       using (MySqlCommand dataCmd = new MySqlCommand($"SELECT * FROM {tableName}", connection.MySqlConn))
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
               connection.CloseConn();
           }
       }
      
       private string EscapeSqlString(string value)
       {
           return value.Replace("'", "''");
       }
    }
}
