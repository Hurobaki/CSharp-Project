using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class MessagePacket : Packet
    {
        public string chatroom { get; private set; }
        public string message { get; private set; }

        public MessagePacket(string u, string c, string m) : base(TypePacket.CreateChatRoomValidation)
        {
            this.user = u;
            this.chatroom = c;
            this.message = m;
        }
    }
}