using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CSharp.Exceptions.PersException;

namespace Project_CSharp.Authentification
{
    namespace Authentification
    {
        class AuthentificationManager
        {
            // Si on créé une classe User, il suffit de passer en paramètre un User
            // Du coup est-ce qu'il faut créer une classe User ?
            public void addUser(String login, String password)
            {
                if (true)//user exists
                {
                    throw new UserExistsException("Add User method flag, UserExistsException raised", login);
                }
                else
                {
                    //add
                }
            }

            public void removeUser(String login)
            {
                if (true)
                {
                    throw new UserUnknowException("Remove User method flag, UserUnknownException raised", login);
                }
                else
                {
                    //remove
                }
            }

            public void authentify(String login, String password)
            {
                if(true)
                {
                    throw new UserExistsException("Authentify method flag, UserExistsException raised", login);
                    throw new WrongPasswordException("Authentify method flag, WrongPasswordException", login);
                }
                else
                {
                    //Authentify
                }
            }

            public static AuthentificationManager load(String path)
            {
                if(true)
                {
                    throw new IOException();
                }
                else
                {
                    //On charge
                }

            }

            public void save(String path)
            {
                if(true)
                {
                    throw new IOException();
                }
                else
                {
                    //Save inside txt file
                }
            }
        }
    }
}
