using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CSharp.Exceptions
{
    namespace PersException
    {
        class UserUnknowException : AuthentificationException
        {
            public UserUnknowException(String message, String log)
                : base(message, log)
            { }

            public void DisplayLogReportShow()
            {
                Console.WriteLine(this.login + " " + this.Message);
            }
        }
    }
}
