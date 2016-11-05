using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CSharp_1._2.Exceptions.PersException;

namespace Project_CSharp_1._2.Authentification
{
    class AuthentificationManager
    {
        public void addUser(String login, String password)
        {
            if (true)//user exists
            {
                throw new UserExistsException("Message d'erreur", login);
            }
            else
            {
                //add
            }
        }
    }
}
