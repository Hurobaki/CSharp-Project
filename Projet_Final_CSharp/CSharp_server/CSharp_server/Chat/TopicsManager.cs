using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_server.Authentification.Authentification;
using System.Xml.Serialization;
using System.IO;

namespace CSharp_server.Chat
{
    namespace Chat
    {
        /* Classe de gestion des utilisateurs/chatroom */
        [Serializable]
        public class TopicsManager
        {
            private static string path = "Topics.xml";
            // Liste des chatrooms
            private static Hashtable _topics = new Hashtable();

            public Hashtable topics
            {
                get
                {
                    return _topics;
                }
                set
                {
                    _topics = value;
                }
            }

            /* Fonction d'ajout d'un utilisateur dans une chatroom */
            public bool joinTopic(string topic, User u)
            {
                if (_topics.Contains(topic))
                {
                    Chatroom t = (Chatroom)_topics[topic];
                    return t.join(u);
                }
                else
                    return false;
            }

            /* Fonction de création d'une noouvelle chatroom */
            public bool createTopic(string topic)
            {
                if (_topics.Contains(topic))
                    return false;
                else
                {
                    Chatroom ct = new Chatroom(topic);
                    _topics.Add(topic, ct);
                    this.save(path);
                }
                return true;
            }

            public List<string> getRooms()
            {
                List<string> res = new List<string>();
                foreach (string s in _topics.Keys)
                {
                    res.Add(s);
                }
                return res;
            }

            public Chatroom getRoom(string s)
            {
                if (_topics.ContainsKey(s))
                {
                    Chatroom cr = (Chatroom)_topics[s];
                    return cr;
                }
                else
                {
                    return null;
                }
            }

            /* Fonction de deserialization du fichier xml de chatrooms */
            public static Hashtable load(string path)
            {
                Hashtable tops = new Hashtable();


                XmlSerializer deserializer = new XmlSerializer(typeof(List<DataItem>));
                StreamReader reader = new StreamReader(path);
                List<DataItem> templist = (List<DataItem>)deserializer.Deserialize(reader);

                foreach (DataItem di in templist)
                    tops.Add(di.Key, new Chatroom(di.Key));

                reader.Close();
                return tops;
            }

            /* Fonction de serialization des chatrooms dans un fichier xml */
            public void save(string path)
            {
                List<DataItem> tempdataitems = new List<DataItem>();
                foreach (string key in topics.Keys)
                {
                    tempdataitems.Add(new DataItem(key));
                }

                XmlSerializer serializer = new XmlSerializer(typeof(List<DataItem>));
                StreamWriter writer = new StreamWriter(path);
                serializer.Serialize(writer, tempdataitems);
                writer.Close();
            }
        }
    }

    /* Classe anonyme pour la serialization  */
    public class DataItem
    {
        public string Key;

        public DataItem() { }
        public DataItem(string key)
        {
            Key = key;
        }
    }
}