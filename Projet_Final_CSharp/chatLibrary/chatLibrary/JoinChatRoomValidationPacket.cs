using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class JoinChatRoomValidationPacket : Packet
    {
        public bool value { get; private set; }

        public JoinChatRoomValidationPacket(bool e) : base(TypePacket.JoinChatRoomValidation)
        {
            this.value = e;
        }
    }
}