using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharp_server.Authentification.Authentification;

namespace CSharp_server.Chat
{
    namespace Chat
    {
        class TopicsManager
        {
            private static Hashtable _topics = new Hashtable();

            public TopicsManager()
            {
            }

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
                    Console.WriteLine(ct.topic);
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
                    Console.WriteLine(cr.topic);
                    return cr;
                }
                else
                {
                    Console.WriteLine("Not in there");
                    Console.Write(s + ": [");
                    foreach (string str in _topics.Keys)
                        Console.Write(str+" ");
                    Console.Write("]");
                    return null;
                }
            }
        }
    }
}
