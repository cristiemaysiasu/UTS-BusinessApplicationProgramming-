using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTS.Model
{
    class Admin
    {
        private string username;
        private string password;

        public Admin()
        {
            this.username = null;
            this.password = null;
        }

        public Admin(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public void setUsername(string username)
        {
            this.username = username;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public string getUsername()
        {
            return this.username;
        }

        public string getPassword()
        {
            return this.password;
        }
    }
}
