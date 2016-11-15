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
using chatLibrary;

namespace Project_CSharp.Server
{
    class ChatServer
    {
        /*
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
        */
    }

    public class handleClient
    {
        TcpClient clientSocket;
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Auth);
            ctThread.Start();
        }
        private void Auth()
        {
            try
            {
                
                Paquet paquet = Paquet.Receive(clientSocket.GetStream());

                if (paquet is AuthPaquet)
                {
                    AuthPaquet ap = (AuthPaquet)paquet;

                    Console.WriteLine(ap.Id);
                    Console.WriteLine(ap.MotDePasse);
                }

                /*while (true)
                {
                    BinaryReader reader = new BinaryReader(clientSocket.GetStream());
                    String line = reader.ReadString();
                    if (line.Equals("2"))
                    {

                    }
                    Console.WriteLine(reader.ReadString());
                }*/
            }
            catch (IOException e)
            {
                Console.WriteLine("Un client a déconnecté");
            }

        }
    }
}
