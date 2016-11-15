using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using chatLibrary;
using CSharp_Graphic_Chat.Authentification.Authentification;

namespace CSharp_Graphic_Chat.Server
{
    class ChatServer
    {
        private List<User> connectedUsers = new List<User>();

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
            try
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
                        Console.WriteLine("Sending good paquet " + flag);
                        User u = new User(ap.login, ap.password);
                        ChatServer.addUser(u);
                        Packet.Send(bp, ns);
                    }
                    else
                    {
                        Console.WriteLine("Sending error paquet " + flag);
                        Packet.Send(bp, ns);
                        this.Auth();
                    }
                }
                if (packet is SubscribePacket)
                {
                    SubscribePacket sb = (SubscribePacket)packet;

                    SubscribeValidation sv = new SubscribeValidation(am.addUser(sb.login, sb.password));

                    Packet.Send(sv, ns);
                    
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
