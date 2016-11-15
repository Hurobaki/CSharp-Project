using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Graphic_Chat.Exceptions
{
    namespace PersException
    {
        class WrongPasswordException : AuthentificationException
        {
            public WrongPasswordException(String message, String log)
                : base(message, log)
            { }

            public void DisplayLogReportShow()
            {
                Console.WriteLine(this.Message + " " + this.login);
            }
        }
    }
}
