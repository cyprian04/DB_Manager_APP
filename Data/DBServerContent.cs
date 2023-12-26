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

        public void ChoosenDB(string db)
        {
            connection.ConnectionWithDb(db);
        }

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

        public DataTable ExecuteAndCheckSQLQuerry(TypeOfQuerry type, string querry)
        {
            DataTable dataTable;
            try
            {
                connection.OpenConn();
                MySqlCommand cmd = new MySqlCommand(querry, connection.MySqlConn);
                dataTable = new DataTable();
                using (MySqlDataReader reader = cmd.ExecuteReader()){ dataTable.Load(reader); }

                switch (type)
                {
                    case TypeOfQuerry.defaultQuerry:

                        if (querry.Trim().StartsWith("DROP DATABASE", StringComparison.OrdinalIgnoreCase) ||
                            querry.Trim().StartsWith("CREATE DATABASE", StringComparison.OrdinalIgnoreCase))
                        {
                            connection.CloseConn();
                            connection.OpenConn();
                            //DisplayCurrentListBox(actualizedTablesListBox);
                        }

                        if (querry.Trim().StartsWith("DROP TABLE", StringComparison.OrdinalIgnoreCase) ||
                            querry.Trim().StartsWith("CREATE TABLE", StringComparison.OrdinalIgnoreCase))
                        {
                            connection.CloseConn();
                            connection.OpenConn();                            
                            //DisplayCurrentListBox(actualizedTablesListBox);
                        }

                        if (dataTable.Rows.Count != 0)
                        {
                            return dataTable;
                        }

                    break;
                 
                    case TypeOfQuerry.ShowData: 
                    case TypeOfQuerry.ShowStruct:

                        return dataTable;
                }
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
            return null;
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
