using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project_Chat_CSharp.Chat
{
    namespace Chat
    {
        class TopicsManager
        {
            private Hashtable _topics = new Hashtable();

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

            public Chatroom joinTopic(String topic)
            {
                if (_topics.Contains(topic))
                    return (Chatroom)_topics[topic];
                else
                    return null;
            }

            public void createTopic(String topic)
            {
                if (_topics.Contains(topic))
                    return;
                else
                    _topics.Add(topic,new Chatroom(topic));
            }
        }
    }
}
