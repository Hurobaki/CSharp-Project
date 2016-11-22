using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class CreateChatRoomValidationPacket : Packet
    {
        public bool value { get; private set; }
        public string chatRoom { get; private set; }

        public CreateChatRoomValidationPacket(bool e, string c) : base(TypePacket.CreateChatRoomValidation)
        {
            this.value = e;
            this.chatRoom = c;
        }
    }
}