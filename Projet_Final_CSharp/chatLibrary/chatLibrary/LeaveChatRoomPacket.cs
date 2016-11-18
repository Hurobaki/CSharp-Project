using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class LeaveChatRoomPacket : Packet
    {
        public string chatRoom { get; private set; }

        public LeaveChatRoomPacket(string c, string u) : base(TypePacket.QuitChatRoom)
        {
            this.chatRoom = c;
            this.user = u;
        }
    }
}
