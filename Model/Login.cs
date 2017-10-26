using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Login
    {
        public Login(string username,string password,string id)
        {
            Username = username;
            Password = password;
            Id = id;
        }

        private string _username;
        private string _password;
        private string _id;

        public string Username { get { return this._username; }set { this._username = value; } }
        public string Password { get { return this._password; } set { this._password = value; } }
        public string Id { get { return this._id; } set { this._id = value; } }
    }
}
