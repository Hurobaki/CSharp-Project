using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class JoinChatRoomPacket : Packet
    {
        public string chatRoom { get; private set; }

        public JoinChatRoomPacket(string e) : base(TypePacket.JoinChatRoom)
        {
            this.chatRoom = e;
        }
    }
}