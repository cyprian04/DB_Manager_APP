using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Database_app.Data
{
    class CurrentUser
    {
        private static string dbName, username, password, hostName;
        private object currentUserConn;

        public string DbName { get => dbName; set => dbName = value; }

        public string Username { get => username; set => username = value; }

        public string Password { get => password; set => password = value; }

        public string ServerIp { get => hostName; set => hostName = value; }

        public CurrentUser(Data.Connection currentUserConn_in)
        {
            currentUserConn = currentUserConn_in;
            Username = currentUserConn_in.Username;
            Password = currentUserConn_in.Password;
            ServerIp = currentUserConn_in.ServerIp;
        }
    }
}
