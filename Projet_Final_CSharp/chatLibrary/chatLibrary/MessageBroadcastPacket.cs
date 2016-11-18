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
        public string user { get; private set; }
        public string message { get; private set; }

        public MessageBroadcastPacket(string u, string m) : base(TypePacket.MessageBroadcast)
        {
            this.user = u;
            this.message = m;
        }
    }
}