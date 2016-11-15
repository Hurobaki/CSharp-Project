using CSharp_Graphic_Chat.Exceptions.PersException;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CSharp_Graphic_Chat.Authentification
{
    namespace Authentification
    {
        [Serializable()]
        public class AuthentificationManager
        {
            // Si on créé une classe User, il suffit de passer en paramètre un User
            // Du coup est-ce qu'il faut créer une classe User ?
            //Ou du coup on créé une instance de User ici et ensuite on fait une liste d'User ? 

            
            private static List<User> _users = new List<User>();


            [XmlArray("Users_Registered")]
            public List<User> Users
            {
                get
                {
                    return _users;
                }
            }



            
            public void addUser(String login, String password)
            {
                    foreach (User u in _users)
                    {
                    // Pas deux utilisateurs avec le même pseudo ??? 
                    // Si on passe ensuite par des Chatter avec alias on peut laisser deux users avec le meme login
                    // Mais pas deux fois le meme alias lorsqu'il sera dans le chat 

                        if (u.login.Equals(login) && u.password.Equals(password) || u.login.Equals(login))
                        {
                            throw new UserExistsException("already exists !", login);
                        }
                    }

                    User temp = new User(login, password);
                    _users.Add(temp);

                    TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                    Console.WriteLine("{1} has been added !", temp.login, ti.ToTitleCase(temp.login));
            }

            public void removeUser(String login)
            {
                //Du coup si on laisse deux Users avoir le meme login
                //Faut supprimer en sachant le mdp ?
                //Je pense qu'il faut pas leur laisser le même login du coup

                bool flag = false;

                for(int i = 0; i < _users.Count; i++)
                {
                    if (_users[i].login.Equals(login))
                    {

                        TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
                        Console.WriteLine("{1} has been removed !", _users[i].login, ti.ToTitleCase(_users[i].login));

                        _users.Remove(_users[i]);
                        flag = true; 
                    }
                }

                if(!flag)
                {
                    throw new UserUnknowException("was not found.", login);
                }
            }

            public void checkRegistredUsers()
            {
                Console.WriteLine("Users already registrered : ");

                foreach (User u in _users)
                {
                    Console.WriteLine(u);
                }
            }

            public int authentify(String login, String password)
            {

                // throw new UserExistsException("Authentify method flag, UserExistsException raised", login);
                // Si l'utilisateur est déjà connecté ??

                bool flag = false;
                
                foreach(User u in _users)
                {
                    if(u.login.Equals(login))
                    {
                        flag = true;

                        if(u.password.Equals(password))
                        {
                            Console.WriteLine("Authentification OK !");
                            return 1;
                        }
                        else
                        {
                            Console.WriteLine("Wrong password");
                            return 2;
                        }
                    }           
                }
                Console.WriteLine("User not found");
                return 3;
            }

            public static AuthentificationManager load(String path)
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(AuthentificationManager));
                StreamReader reader = new StreamReader(path);
                AuthentificationManager aut = (AuthentificationManager)deserializer.Deserialize(reader);
                reader.Close();

                return aut;
            }

            public void save(String path)
            {
                // Utiliser la sérialisation
                // En binaire ou en XML ? 
                //Binaire mieux pour du réseau car moins de paquets

                XmlSerializer serializer = new XmlSerializer(typeof(AuthentificationManager));
                StreamWriter writer = new StreamWriter(path);
                serializer.Serialize(writer, this);
                writer.Close();
            }
        }
    }
}
