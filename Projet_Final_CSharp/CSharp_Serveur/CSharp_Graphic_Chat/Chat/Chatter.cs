using chatLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Graphic_Chat.Chat
{
    namespace Chat
    {
        public class Chatter
        {
            private String _alias;

            public Chatter(String alias)
            {
                _alias = alias;
            }

            public Chatter()
            { }

            public String alias
            {
                get{return _alias;}
                set{_alias=value;}
            }

            public void receiveAMessage(String msg, Chatter c)
            {
            }
        }
    }
}
