using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class MessageBroadcastPacket : Packet
    {
        public string chatroom { get; private set; }
        public string message { get; private set; }

        public MessageBroadcastPacket(string u, string m, string c) : base(TypePacket.MessageBroadcast)
        {
            this.user = u;
            this.chatroom = c;
            this.message = m;

        }
    }
}