using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using chatLibrary;
using CSharp_Graphic_Chat.Authentification.Authentification;
using CSharp_Graphic_Chat.Chat.Chat;
using System.Net;

namespace CSharp_Graphic_Chat.Server
{
    class ChatServer
    {
        private List<User> connectedUsers = new List<User>();
        public static TcpListener ServerSocket = new TcpListener(IPAddress.Any, 1337);
        public static void StartServer()
        {
            ServerSocket.Start();
            Console.WriteLine("Server started");
        }


        public static void StartListening()
        {
            while (true)
            {
                
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Console.WriteLine("New client");
                handleClient client = new handleClient();
                client.startClient(clientSocket);
            }
        }

        public static void addUser(User u)
        {
            /*connectedUsers.Add(u);*/
        }
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
        NetworkStream ns;
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Auth);
            ctThread.Start();
        }
        private void Auth()
        {
            AuthentificationManager am = new AuthentificationManager();
            TopicsManager tm = new TopicsManager();

            try
            {
                while (true)
                {
                    ns = clientSocket.GetStream();
                    Packet packet = Packet.Receive(ns);

                    if (packet is AuthPacket)
                    {
                        AuthPacket ap = (AuthPacket)packet;

                        Console.WriteLine(ap.login);
                        Console.WriteLine(ap.password);
                        int flag = am.authentify(ap.login, ap.password);
                        LoginPacket bp = new LoginPacket(flag);
                        if (flag == 1)
                        {
                            Console.WriteLine("Connecting user");
                            User u = new User(ap.login, ap.password);
                            ChatServer.addUser(u);
                            Packet.Send(bp, ns);

                            TopicsPacket tp = new TopicsPacket(tm.getRooms());
                            Packet.Send(tp, ns);
                        }
                        else
                        {
                            Console.WriteLine("Error, unable to connect the user, error code : " + flag);
                            Packet.Send(bp, ns);
                            this.Auth();
                        }
                    }
                    if (packet is SubscribePacket)
                    {
                        SubscribePacket sb = (SubscribePacket)packet;

                        SubscribeValidation sv = new SubscribeValidation(am.addUser(sb.login, sb.password));
                        Console.WriteLine("Sending subscribe validation packet");
                        Packet.Send(sv, ns);

                    }
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("Un client a déconnecté");
                ChatServer.StartListening();
            }
        }
    }
}
