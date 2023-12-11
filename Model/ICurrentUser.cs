using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Database_app.Model
{
    public interface ICurrentUser
    {
        string Username { get; set; }
        string Password { get; set; }
        string Host { get; set; }
    }
}
