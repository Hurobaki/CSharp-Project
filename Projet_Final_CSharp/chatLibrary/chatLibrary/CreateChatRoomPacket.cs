using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class CreateChatRoomPacket : Packet
    {
        public string chatRoom { get; private set; }

        public CreateChatRoomPacket(string e) : base(TypePacket.CreateChatRoom)
        {
            this.chatRoom = e;
        }
    }
}