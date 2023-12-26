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
        private MySqlConnection mySqlConn = new MySqlConnection();
        private string connectionString;
        private static string dbName, tbName;
   
        public string DbName {get => dbName; set => dbName = value;}
        public string TbName { get => tbName; set => tbName = value; }
        public MySqlConnection MySqlConn { get => mySqlConn; set => mySqlConn = value; }

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
                MySqlConn = new MySqlConnection(connectionString);
                MySqlConn.Open();
                return true;
            }
            catch (MySqlException)
            { 
                connectionString = null;
                return false;
            }
            finally
            {
                MySqlConn.Close();
            }
        }

        public void OpenConn()
        {
            MySqlConn.Open();
        }

        public void CloseConn()
        {
            MySqlConn.Close();
        }

        public void ConnectionWithDb(string DbName_in)
        {
            if (DbName_in != null)
            {
                try
                {
                    MySqlConn.Open();
                    MySqlConn.ChangeDatabase(DbName_in);
                    DbName = DbName_in;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    MySqlConn.Close();
                }
            }
        }

        public void DisconnectUserFromServer()
        {
            dbName = null;
            tbName = null;
            connectionString = null;
            MySqlConn = null;
        }
    }
}
