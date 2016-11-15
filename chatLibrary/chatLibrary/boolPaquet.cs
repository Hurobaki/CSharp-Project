using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class boolPaquet : Paquet
    {
        public bool value { get; private set; }

        public boolPaquet(bool e) : base(TypePaquet.Bool)
        {
            this.value = e;
        }
    }
}
