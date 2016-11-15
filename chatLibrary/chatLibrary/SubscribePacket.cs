using System;

namespace chatLibrary
{
    [Serializable()]
    public class SubscribePacket : Packet
    {
        public String login { get; private set; }
        public String password { get; private set; }

        public SubscribePacket(String Id, String MotDePasse) : base(TypePacket.Subscribe)
        {
            this.login = Id;
            this.password = MotDePasse;
        }
    }
}
