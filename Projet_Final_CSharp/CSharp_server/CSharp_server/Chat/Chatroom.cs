using chatLibrary;
using CSharp_server.Authentification.Authentification;
using CSharp_server.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_server.Chat
{
    namespace Chat
    {
        class Chatroom
        {
            private string _topic;
            private List<User> _chatters = new List<User>();

            public Chatroom(String topic)
            {
                _topic = topic;
            }

            public String topic
            {
                get;
                set;
            }
            public List<User> chatters
            {
                get;
                set;
            }
            public void post(String msg, User u)
            {
                //Console.WriteLine(this._topic +" : "+ u.chatter.alias + " : " + msg);
                foreach (User user in this._chatters)
                {
                    //Console.WriteLine("Sending message : " + msg + " from user : " + u.chatter.alias + " in chatroom : " + this._topic + " to user : " + user.chatter.alias);
                    MessageBroadcastPacket mbp = new MessageBroadcastPacket(u.login, msg, this._topic);
                    Packet.Send(mbp, user.ns);
                }
            }

            public void quit(User c)
            {
                c.ns.Flush();
                _chatters.Remove(c);
                Console.WriteLine("[CHATROOM]" + c.chatter + " has left the chat" + this.topic);
            }

            public bool join(User c)
            {
                if (_chatters.Contains(c))
                    return false;
                else
                {
                    _chatters.Add(c);
                    Console.WriteLine("[CHATROOM]" + c.chatter + " joined the chat" + this.topic);
                    return true;
                }
            }
        }
    }
}
