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
            String _login;
            String _password;
            // à implémenter
            Chatter _chatter;
            NetworkStream _ns;
            
            public User() { }

            public User(String login, String password, NetworkStream ns)
            {
                _login = login;
                _password = password;
                _ns = ns;
            }

            public User(String login, String password)
            {
                _login = login;
                _password = password;
            }

            public String login
            {
                get
                {
                    return _login;
                }

                set
                {
                    _login = value;
                }
            }

            public String password
            {
                get
                {
                    return _password;
                }

                set
                {
                    _password = value;
                }
            }

            public NetworkStream ns
            {
                get
                {
                    return _ns;
                }

                set
                {
                    _ns = value;
                }
            }

            public Chatter chatter
            {
                get
                {
                    return _chatter;
                }

                set
                {
                    _chatter = value;
                }
            }

            public override string ToString()
            {
                return "Login = " + this.login + ".";
            }
        }
    }
}
