using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;

namespace CSharp_server.Authentification
{
    namespace Authentification
    {
        public class AuthentificationManager
        {
                     
            private static List<User> _users = new List<User>();
            private const string PATH = "SaveUsers.xml";

            [XmlArray("Users_Registered")]
            public List<User> Users
            {
                get
                {
                    return _users;
                }
            }
            
            public bool addUser(String login, String password)
            {
                    foreach (User u in _users)
                        if (u.login.Equals(login) && u.password.Equals(password) || u.login.Equals(login))
                           return false;

                    User temp = new User(login, password);
                    _users.Add(temp);

                    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                    Console.WriteLine("[AUTHENTIFICATION]{1} has been added !", temp.login, ti.ToTitleCase(temp.login));
                this.save(PATH);
                return true;
            }

            public void removeUser(String login)
            {
                bool flag = false;

                for(int i = 0; i < _users.Count; i++)
                {
                    if (_users[i].login.Equals(login))
                    {

                        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                        Console.WriteLine("[AUTHENTIFICATION]{1} has been removed !", _users[i].login, ti.ToTitleCase(_users[i].login));

                        _users.Remove(_users[i]);
                        this.save(PATH);
                        flag = true; 
                    }
                }

                if(!flag)
                {
                    Console.WriteLine("[AUTHENTIFICATION]Unknown User");
                }
            }

            public void checkRegistredUsers()
            {
                Console.WriteLine("[AUTHENTIFICATION]Users already registrered : ");

                foreach (User u in _users)
                    Console.WriteLine(u);
            }

            public int authentify(String login, String password)
            {
                // throw new UserExistsException("Authentify method flag, UserExistsException raised", login);
                // Si l'utilisateur est déjà connecté ??
                foreach(User u in _users)
                {
                    if(u.login.Equals(login))
                    {
                        if(u.password.Equals(password))
                        {
                            Console.Write(" ...");
                            return 1;
                        }
                        else
                        {
                            Console.WriteLine("[AUTHENTIFICATION]User not authentified : Wrong password");
                            return 2;
                        }
                    }           
                }
                Console.WriteLine("[AUTHENTIFICATION]User not found");
                return 3;
            }

            public static AuthentificationManager load(string path)
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(AuthentificationManager));
                StreamReader reader = new StreamReader(path);
                AuthentificationManager aut = (AuthentificationManager)deserializer.Deserialize(reader);
                reader.Close();

                return aut;
            }

            public void save(string path)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AuthentificationManager));
                StreamWriter writer = new StreamWriter(path);
                serializer.Serialize(writer, this);
                writer.Close();
            }
        }
    }
}
