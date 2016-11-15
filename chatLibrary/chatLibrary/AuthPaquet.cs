using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace chatLibrary
{
    [Serializable()]
    public class AuthPaquet : Paquet
    {
        public String Id { get; private set; }
        public String MotDePasse { get; private set; }

        public AuthPaquet(String Id, String MotDePasse) : base(TypePaquet.Authentification)
        {
            this.Id = Id;
            this.MotDePasse = MotDePasse;
        }
    }
}