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

namespace Project_CSharp.Server
{
    class ChatServer
    {
        Socket server;

        public void startServer()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.WriteLine("Starting server");
            server.Connect(ipep);
            
        }

        public void stopServer()
        {
            Console.WriteLine("Stopping server");
            server.Dispose();
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
       /* public static void Main(String[] args)
        {
            ChatServer cs = new ChatServer();
            cs.startServer();
            cs.run();
        }*/
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
