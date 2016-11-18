using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class TopicsPacket : Packet
    {
        public List<string> topics { get; private set; }

        public TopicsPacket(List<string> t) : base(TypePacket.Topics)
        {
            this.topics = t;
        }
    }
}
