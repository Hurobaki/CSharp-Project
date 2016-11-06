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
        private TcpListener server = new TcpListener(25565);

        public void startServer()
        {
            Console.WriteLine("Starting server");
            server.Start();
        }

        public void stopServer()
        {
            Console.WriteLine("Starting server");
            server.Stop();
        }

        public void run()
        {
            Console.WriteLine("Running server");
            while (true)
            {
                Console.WriteLine("in the while");
                TcpClient newClient = server.AcceptTcpClient();
                Console.WriteLine("New Client");
                HandleClient hclient = new HandleClient();
                hclient.startClient(newClient);
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
        private TcpClient clientSocket;
        private byte[] bytesFrom = new byte[10000];

        public void startClient(TcpClient c)
        {
            clientSocket = c;
            Thread clientThread = new Thread(gereClient);
            clientThread.Start();
        }

        public void gereClient()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> " + "From client-"  + dataFromClient);

                    rCount = Convert.ToString(requestCount);
                    serverResponse = "Server to clinet("  + ") " + rCount;
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    }
}
