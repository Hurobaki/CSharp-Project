using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class WhisperMessagePacket : Packet
    {
        public string chatroom { get; private set; }
        public string message { get; private set; }
        public string target { get; private set; }

        public WhisperMessagePacket(string u, string c, string m, string t) : base(TypePacket.WhisperMessage)
        {
            this.user = u;
            this.chatroom = c;
            this.message = m;
            this.target = t;
        }
    }
}