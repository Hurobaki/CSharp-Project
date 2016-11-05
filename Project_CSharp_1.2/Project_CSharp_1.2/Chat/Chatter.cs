using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Chat_CSharp.Chat
{
    namespace Chat
    {
        class Chatter
        {
            private String _alias;

            public Chatter(String alias)
            {
                _alias = alias;
            }

            public String alias
            {
                get{return _alias;}
                set{_alias=value;}
            }

            public void ReceiveAMessage(String msg, Chatter c)
            {
            }

            
        }
    }
}
