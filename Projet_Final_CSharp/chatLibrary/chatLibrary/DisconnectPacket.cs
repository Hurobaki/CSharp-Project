using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class DisconnectPacket : Packet
    {
        public DisconnectPacket(string u) : base(TypePacket.Disconnect)
        {
            this.user = u;
        }
    }
}
