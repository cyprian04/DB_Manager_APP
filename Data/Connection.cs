using System;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Security;
using System.Runtime.InteropServices;

namespace GUI_Database_app.Data
{
    public class Connection
    { 
        private static MySqlConnection connection = new MySqlConnection();
        private string connectionString;
        private static string dbName, tbName;
   
        public string DbName {get => dbName; set => dbName = value;}
             
        public string TbName { get => tbName; set => tbName = value; }

        public bool VerifyCredentials(string Host, string Username, SecureString Password)
        {
            string pass;
            if (Password != null)
            {
                IntPtr unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(Password);
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

        public void ConnectionWithDb(string DbName_in)
        {
            if (DbName != DbName_in)
            {
                DbName = DbName_in;

                try
                {
                    connection.Open();
                    connection.ChangeDatabase(DbName);
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

        public void DisconnectUserFromServer()
        {
            dbName = null;
            tbName = null;
            connectionString = null;
            connection = null;
        }
    }
}
