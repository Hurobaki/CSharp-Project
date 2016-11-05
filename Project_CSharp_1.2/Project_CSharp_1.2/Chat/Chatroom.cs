using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Chat_CSharp.Chat
{
    namespace Chat
    {
        class Chatroom
        {
            private String _topic;
            private List<Chatter> _chatters = new List<Chatter>();

            public Chatroom(String topic)
            {
                _topic = topic;
            }

            public String topic
            {
                get;
                set;
            }

            public void post(String msg, Chatter c)
            {
                Console.WriteLine(c.alias+" : "+msg);
            }

            public void quit(Chatter c)
            {
                _chatters.Remove(c);
                Console.WriteLine("(Message from Chatroom : " + _topic + ") " + c.alias + " left the chat");
            }

            public void join(Chatter c)
            {
                _chatters.Add(c);
                Console.WriteLine("(Message from Chatroom : " + _topic + ") " + c.alias + " joined the chat");
            }

           
        }
    }
}
