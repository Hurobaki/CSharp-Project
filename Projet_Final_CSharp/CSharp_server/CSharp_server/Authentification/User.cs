using CSharp_server.Chat.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_server.Authentification
{
    namespace Authentification
    {
        public class User
        {
            private string _login;
            private string _password;
            private Chatter _chatter;
            private NetworkStream _ns;
            
            public string login
            {
                get { return _login; }
                set { _login=value; }
            }

            public string password
            {
                get { return _password; }
                set { _password = value; }
            }

            public Chatter chatter
            {
                get { return _chatter; }
                set { _chatter = value; }
            }

            public NetworkStream ns
            {
                get { return _ns; }
                set { _ns = value; }
            }

            public User() { }

            public User(string login, string password, NetworkStream ns)
            {
                _login = login;
                _password = password;
                _ns = ns;
            }

            public User(string login, string password)
            {
                _login = login;
                _password = password;
            }

            public override string ToString()
            {
                return "Login = " + this.login + ".";
            }
        }
    }
}
