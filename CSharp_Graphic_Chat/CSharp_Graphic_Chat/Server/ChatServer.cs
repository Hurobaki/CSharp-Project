using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Project_CSharp.Client;
using System.Threading;
using Project_CSharp.Chat.Chat;
using System.Net;
using System.IO;

namespace Project_CSharp.Server
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
        TopicsManager tm = new TopicsManager();
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Chat);
            ctThread.Start();
        }

        public void run()
        {
            Console.WriteLine("Running server");
            while (true)
            {
                Console.WriteLine("In the while");
                Socket client = server.Accept();
                
                Console.WriteLine("New Client");
                HandleClient hclient = new HandleClient();
                hclient.startClient(client);
            }
        }
        public static void Main(String[] args)
        {
            ChatServer cs = new ChatServer();
            cs.startServer();
            cs.run();
        }
    }
    public class HandleClient
    {
        private Socket clientSocket;
        private byte[] bytesFrom = new byte[10000];

        public void startClient(Socket c)
        {
            clientSocket = c;
            Thread clientThread = new Thread(gereClient);
            clientThread.Start();
        }

        public void gereClient()
        {
         

            while ((true))
            {
                try
                {
                    clientSocket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000));
                    clientSocket.Listen(200);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    }
}
