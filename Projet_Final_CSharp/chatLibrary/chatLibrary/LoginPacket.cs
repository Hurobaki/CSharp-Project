using System;

namespace chatLibrary
{
    [Serializable()]
    public class LoginPacket : Packet
    {
        public int value { get; private set; }

        public LoginPacket(int e) : base(TypePacket.Login)
        {
            this.value = e;
        }
    }
}
