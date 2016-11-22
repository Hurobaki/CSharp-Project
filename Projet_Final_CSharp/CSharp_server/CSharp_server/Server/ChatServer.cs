using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using chatLibrary;
using CSharp_server.Authentification.Authentification;
using CSharp_server.Chat.Chat;
using System.Net;
using System.Collections;
using System.Runtime.Serialization;

namespace CSharp_server.Server
{
    class ChatServer
    {
        public static List<User> chattingUsers = new List<User>();
        public static TcpListener ServerSocket = new TcpListener(IPAddress.Any, 25565);
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
        public static void removeUser(User u)
        {
            if (chattingUsers.Contains(u))
                chattingUsers.Remove(u);
            else
                Console.WriteLine("Not in there");
        }
        public static User getUser(string login)
        {
            foreach(User u in chattingUsers)
            {
                if (u.login.Equals(login))
                    return u;
            }
            return null;
        }

        public static void addUser(User u)
        {
            if (!chattingUsers.Contains(u))
                chattingUsers.Add(u);
            else
                Console.WriteLine("Already logged");
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
            ns = clientSocket.GetStream();
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
                    Packet packet = null;
                    try
                    {
                        packet = Packet.Receive(ns);
                    }
                    catch(SerializationException e)
                    {
                        Console.WriteLine(e);
                    }

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
                            User u = new User(ap.login, ap.password, ns);
                            u.chatter = new Chatter(ap.login);
                            ChatServer.addUser(u);
                            Packet.Send(bp, ns);
                            /*Displaying topics to user*/
                            Console.Write("[");
                            foreach (String s in tm.getRooms())
                                Console.Write(s + ", ");
                            Console.Write("]");
                            Console.WriteLine("Sending topics ... ");
                            TopicsPacket tp = new TopicsPacket(tm.getRooms());
                            Packet.Send(tp, ns);
                        }
                        else
                        {
                            Console.WriteLine("Error, unable to connect the user " + ap.login + ", error code : " + flag);
                            Packet.Send(bp, ns);
                            this.Auth();
                        }
                    }
                    if (packet is SubscribePacket)
                    {
                        /* sending the subscribe packet */
                        SubscribePacket sb = (SubscribePacket)packet;
                        Console.WriteLine("Trying to create user " + sb.login + " subscribe validation packet ...");
                        SubscribeValidation sv = new SubscribeValidation(am.addUser(sb.login, sb.password));
                        Console.WriteLine("Sending subscribe validation packet : " + sv.value);
                        Packet.Send(sv, ns);
                    }
                    if (packet is JoinChatRoomPacket)
                    {
                        JoinChatRoomPacket jcp = (JoinChatRoomPacket)packet;
                        if (tm.getRooms().Contains(jcp.chatRoom))
                        {
                            bool flag = tm.joinTopic(jcp.chatRoom, ChatServer.getUser(jcp.user));
                            if (flag)
                            {
                                Console.WriteLine("User " + jcp.user + " joined chatroom : " + jcp.chatRoom);
                            }
                            else
                                Console.WriteLine("Error, user " + jcp.user + " already in the chatroom : " + jcp.chatRoom);
                            JoinChatRoomValidationPacket jcvp = new JoinChatRoomValidationPacket(flag);
                        }
                    }

                    if (packet is CreateChatRoomPacket)
                    {
                        CreateChatRoomPacket ccp = (CreateChatRoomPacket)packet;
                        if (tm.topics.ContainsKey(ccp.chatRoom))
                        {
                            bool flag = tm.createTopic(ccp.chatRoom);
                            if (flag)
                                Console.WriteLine("User " + ccp.user + " created chatroom : " + ccp.chatRoom);
                            /* Broadcast la création de room avec affichage de message */
                            else
                                Console.WriteLine("Error, chatroom :" + ccp.chatRoom + " already exists");
                            CreateChatRoomValidationPacket ccvp = new CreateChatRoomValidationPacket(flag);
                        }
                    }
                    if (packet is MessagePacket)
                    {
                        MessagePacket mp = (MessagePacket)packet;
                        Console.WriteLine("Posting message : [" +mp.message+ "] in chatroom "+ mp.chatroom);
                        Chatroom cible = (Chatroom)tm.topics[mp.chatroom];
                        cible.post(mp.message, ChatServer.getUser(mp.user));
                    }

                    if (packet is LeaveChatRoomPacket)
                    {
                        Object thisLock = new Object();
                        LeaveChatRoomPacket lcrp = (LeaveChatRoomPacket) packet;
                        Chatroom cible = (Chatroom)tm.topics[lcrp.chatRoom];
                        Console.WriteLine("User : " + lcrp.user + "is leaving chatroom : " + lcrp.chatRoom);
                        lock(thisLock)
                            cible.quit(ChatServer.getUser(lcrp.user));
                        //Verif si dans aucune chatrrom => quitte l'application ? ou lors d'une erreur de IOE verifier si déco ou pas et enlever de la iste chatterUsers
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
