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
                Console.WriteLine(this._topic +" : "+ u.chatter.alias + " : " + msg);
                foreach (User user in this._chatters)
                {
                    Console.WriteLine("Sending message : " + msg + " from user : " + u.chatter.alias + " in chatroom : " + this._topic + " to user : " + user.chatter.alias);
                    MessageBroadcastPacket mbp = new MessageBroadcastPacket(u.login, msg, this._topic);
                    Packet.Send(mbp, user.ns);
                    Console.WriteLine(user.login + user.ns.ToString());
                }
            }

            public void quit(User c)
            {
                c.ns.Flush();
                _chatters.Remove(c);
                Console.WriteLine("(Message from Chatroom : " + _topic + ") " + c.chatter.alias + " left the chat");
                foreach (User u in _chatters)
                    Console.WriteLine(u.login);
            }

            public bool join(User c)
            {
                if (_chatters.Contains(c))
                {
                    Console.WriteLine("Chatter already in this chatroom");
                    return false;
                }
                else
                {
                    _chatters.Add(c);
                    Console.WriteLine("(Message from Chatroom : " + _topic + ") " + c.chatter.alias + " joined the chat");
                    foreach(User u in _chatters)
                        Console.WriteLine(u.login);
                    return true;
                }
            }
        }
    }
}
