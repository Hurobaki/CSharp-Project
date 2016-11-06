using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Project_CSharp.Client
{
    class ChatClient
    {
        NetworkStream serverStream;
        TcpClient _commSocket;

        public ChatClient(TcpClient s)
        {
            _commSocket = s;
            serverStream = _commSocket.GetStream();
            byte[] outStream = System.Text.Encoding.ASCII.GetBytes("Message from Client$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int)_commSocket.ReceiveBufferSize);
            string returndata = System.Text.Encoding.ASCII.GetString(inStream);
            msg("Data from Server : " + returndata);
        }
        public void msg (string s)
        {
            Console.WriteLine(s);
        }
    }
}
