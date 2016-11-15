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
            try
            {
                ns = clientSocket.GetStream();
                Paquet paquet = Paquet.Receive(ns);

                if (paquet is AuthPaquet)
                {
                    AuthPaquet ap = (AuthPaquet)paquet;

                    Console.WriteLine(ap.Id);
                    Console.WriteLine(ap.MotDePasse);
                    AuthentificationManager am = new AuthentificationManager();
                    int flag = am.authentify(ap.Id, ap.MotDePasse);
                    if (flag < 4 && flag > 0)
                        this.Auth();
                    else
                    {
                        User u = new User(ap.Id, ap.MotDePasse);
                        ChatServer.addUser(u);
                        boolPaquet bp = new boolPaquet(true);
                        Paquet.Send(bp, ns);
                    } 
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
