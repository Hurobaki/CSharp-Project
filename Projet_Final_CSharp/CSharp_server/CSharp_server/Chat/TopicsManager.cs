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
        [Serializable]
        public class TopicsManager
        {

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

            public bool createTopic(string topic)
            {
                if (_topics.Contains(topic))
                    return false;
                else
                {
                    Chatroom ct = new Chatroom(topic);
                    _topics.Add(topic, ct);
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

            public void save(string path)
            {
                List<DataItem> tempdataitems = new List<DataItem>(topics.Count);
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