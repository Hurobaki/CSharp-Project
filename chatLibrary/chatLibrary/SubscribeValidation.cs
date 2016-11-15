using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class SubscribeValidation : Packet
    {
        public bool value { get; private set; }

        public SubscribeValidation(bool e) : base(TypePacket.SubscribeValidation)
        {
            this.value = e;
        }
    }
}
