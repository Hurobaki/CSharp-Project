using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class LeaveChatRoomValidationPacket : Packet
    {
        public bool flag { get; private set; }

        public LeaveChatRoomValidationPacket(bool u) : base(TypePacket.QuitChatRoomValidation)
        {
            this.flag = u;
        }
    }
}
