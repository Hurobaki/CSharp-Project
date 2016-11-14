using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ServerSide
{
    class ChatServer
    {
        static void Main(string[] args)
        {
            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 25565);
            ServerSocket.Start();
            Console.WriteLine("Server started");
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Console.WriteLine("New client");
                handleClient client = new handleClient();
                client.startClient(clientSocket);
            }
        }
    }

    public class handleClient
    {
        TcpClient clientSocket;
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Chat);
            ctThread.Start();
        }
        private void Chat()
        {
            try
            {
                byte[] buffer = new byte[10];
                while (true)
                {
                    BinaryReader reader = new BinaryReader(clientSocket.GetStream());
                    Console.WriteLine(reader.ReadString());
                }
            }
            catch(IOException e)
            {
                Console.WriteLine("Un client a déconnecté");
            }
            
        }
    }
}
