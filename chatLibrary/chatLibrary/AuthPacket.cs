using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace chatLibrary
{
    [Serializable()]
    public class AuthPacket : Packet
    {
        public String login { get; private set; }
        public String password { get; private set; }

        public AuthPacket(String Id, String MotDePasse) : base(TypePacket.Authentification)
        {
            this.login = Id;
            this.password = MotDePasse;
        }
    }
}