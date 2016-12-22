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
        /* Classe regroupant les fonctionnalités des chatroom */
        public class Chatroom
        {
            // Nom de la chatroom
            private string _topic;
            // Liste des User connectés à cette chatroom
            private List<User> _chatters = new List<User>();

            public Chatroom(string topic)
            {
                this._topic = topic;
            }

            public string topic
            {
                get { return _topic; }
                set { this.topic = value; }
            }
            public List<User> chatters
            {
                get { return _chatters; }
                set { this._chatters=value; }
            }

            public User getChatter(User s)
            {
                foreach (User u in _chatters)
                {
                    if (u.Equals(s))
                        return u;
                }
                return null;
            }
            public List<string> getChatters()
            {
                List<string> chatters = new List<string>();
                foreach (User u in _chatters)
                    chatters.Add(u.login);
                return chatters;
            }

            /* Fonction d'envoie de message à tous les utilisateurs connectés à cette chatroom */
            public void post(string msg, User u)
            {
                foreach (User user in this._chatters)
                {
                    MessageBroadcastPacket mbp = new MessageBroadcastPacket(u.login, msg, this._topic);
                    Packet.Send(mbp, user.ns);
                }
            }

            /* Fonction de retrait d'un utilisateur de la chatroom */
            public void quit(User c)
            {
                c.ns.Flush();
                _chatters.Remove(c);
                Console.WriteLine("[CHATROOM]" + c.chatter.alias + " has left the chat " + this.topic);
            }

            /* Fonction d'ajout d'un utilisateur de la chatroom */
            public bool join(User c)
            {
                if (_chatters.Contains(c))
                    return false;
                else
                {
                    _chatters.Add(c);
                    Console.WriteLine("[CHATROOM]" + c.chatter + " joined the chat " + this.topic);
                    return true;
                }
            }
        }
    }
}
