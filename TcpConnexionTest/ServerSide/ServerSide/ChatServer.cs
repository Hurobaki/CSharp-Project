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
            TopicsManager tm = new TopicsManager();
            ServerSocket.Start();
            Console.WriteLine("Server started");
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Console.WriteLine("New client");
                handleClient client = new handleClient();
                client.startClient(clientSocket, tm);
            }
        }
    }

    public class handleClient
    {
        TopicsManager tm;
        TcpClient clientSocket;
        public void startClient(TcpClient inClientSocket, TopicsManager tm)
        {
            this.tm = tm;
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Chat);
            ctThread.Start();
        }
        private void Chat()
        {
            try
            {
                while (true)
                {
                    BinaryReader reader = new BinaryReader(clientSocket.GetStream());
                    String line = reader.ReadString();
                    if (line.Equals("2"))
                    {
                        
                    }
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
