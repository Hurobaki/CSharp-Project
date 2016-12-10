using chatLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_server.Chat
{
    namespace Chat
    {
        /* Classe représentant un utilisateur dans la chatroom (pas encore exploité) */
        public class Chatter
        {
            private string _alias;

            public Chatter(string alias)
            {
                _alias = alias;
            }

            public Chatter()
            { }

            public string alias
            {
                get{return _alias;}
                set{_alias=value;}
            }
        }
    }
}
