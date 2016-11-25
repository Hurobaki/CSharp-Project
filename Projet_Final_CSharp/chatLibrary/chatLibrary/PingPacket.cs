using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class PingPacket : Packet
    {
        public PingPacket(string u) : base(TypePacket.Ping)
        {
            this.user = u;
        }
    }
}
