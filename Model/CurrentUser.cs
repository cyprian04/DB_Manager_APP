﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Database_app.Model
{
    class CurrentUser : ICurrentUser
    {
        public string Username { get; set; }
        public SecureString Password { get; set; }
        public string Host { get; set; }

    }
}

