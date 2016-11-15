using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    [Serializable()]
    public class loginPaquet : Paquet
    {
        public int value { get; private set; }

        public loginPaquet(int e) : base(TypePaquet.Login)
        {
            this.value = e;
        }
    }
}
