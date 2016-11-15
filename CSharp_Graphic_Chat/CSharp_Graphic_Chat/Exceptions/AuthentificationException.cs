using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Graphic_Chat.Exceptions
{
    namespace PersException
    {
        class AuthentificationException : Exception
        {
            private String _login;

            public AuthentificationException(String message, String log)
                : base(message)
            {
                _login = log;
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


        }
    }
}
