using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class ListChatterPacket : Packet
    {
        public List<string> chatters { get; private set; }
        public string chatRoom { get; private set; }

        public ListChatterPacket(List<string> e, string c) : base(TypePacket.ChatterList)
        {
            this.chatters = e;
            this.chatRoom = c;
        }
    }
}