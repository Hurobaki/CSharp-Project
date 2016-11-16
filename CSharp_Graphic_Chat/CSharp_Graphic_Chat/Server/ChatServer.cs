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
        private static List<User> connectedUsers = new List<User>();
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
            connectedUsers.Add(u);
        }
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
                    /*
                     * PENSER A FAIRE UN PACKET UNIQUE AVEC UN ATTRIBUT DE TYPE (TROP SALE/PAS ASSEZ CLAIRE ?)
                     */
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
                            /*User logged in*/
                            Console.WriteLine("Connecting user");
                            User u = new User(ap.login, ap.password);
                            ChatServer.addUser(u);
                            Packet.Send(bp, ns);
                            /*Displaying topics to user*/
                            Console.Write("[");
                            foreach (String s in tm.getRooms())
                                Console.Write(s+", ");
                            Console.Write("]");
                            Console.WriteLine("Sending topics ... ");
                            TopicsPacket tp = new TopicsPacket(tm.getRooms());
                            Packet.Send(tp, ns);
                        }
                        else
                        {
                            Console.WriteLine("Error, unable to connect the user "+ap.login+", error code : " + flag);
                            Packet.Send(bp, ns);
                            this.Auth();
                        }
                    }
                    if (packet is SubscribePacket)
                    {
                        /* sending the subscribe packet */
                        SubscribePacket sb = (SubscribePacket)packet;
                        Console.WriteLine("Trying to create user "+sb.login+" subscribe validation packet ...");
                        SubscribeValidation sv = new SubscribeValidation(am.addUser(sb.login, sb.password));
                        Console.WriteLine("Sending subscribe validation packet : " +sv.value);
                        Packet.Send(sv, ns);
                    }
                    if (packet is JoinChatRoomPacket)
                    {
                        JoinChatRoomPacket jcp = (JoinChatRoomPacket)packet;
                        if (tm.getRooms().Contains(jcp.chatRoom))
                        {
                            /* Besoin d'un username ou alias dans le packet */
                            bool flag = tm.joinTopic(jcp.chatRoom, new Chatter("oups"));
                            /* Stockage du chatter dans le user ? */
                            if (flag)
                                Console.WriteLine("User " + jcp.user + " joined chatroom : " + jcp.chatRoom);
                            else
                                Console.WriteLine("Error, user " + jcp.user + " already in the chatroom : " + jcp.chatRoom);
                            JoinChatRoomValidationPacket jcvp = new JoinChatRoomValidationPacket(flag);
                        }
                    }

                    if(packet is CreateChatRoomPacket)
                    {
                        CreateChatRoomPacket ccp = (CreateChatRoomPacket)packet;
                        if (tm.topics.ContainsKey(ccp.chatRoom))
                        {
                            bool flag = tm.createTopic(ccp.chatRoom);
                            if (flag)
                                Console.WriteLine("User "+ ccp.user +" created chatroom : " + ccp.chatRoom);
                            /* Broadcast la création de room avec affichage de message */
                            else
                                Console.WriteLine("Error, chatroom :" + ccp.chatRoom +" already exists");
                            CreateChatRoomValidationPacket ccvp = new CreateChatRoomValidationPacket(flag);
                        }
                    }
                    if(packet is MessagePacket)
                    {
                        MessagePacket mp = (MessagePacket)packet;
                        /* Voir chatroom / topicsManager*/
                        /*foreach(Chatter c in (Chatroom)tm.topics[mp.chatroom])
                        {

                        }*/
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
