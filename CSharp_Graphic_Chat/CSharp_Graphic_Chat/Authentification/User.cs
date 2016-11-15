using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Graphic_Chat.Authentification
{
    namespace Authentification
    {
        public class User
        {
            String _login;
            String _password;
            
            public User() { }

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


            public override string ToString()
            {
                return "Login = " + this.login + ".";
            }
        }
    }
}
