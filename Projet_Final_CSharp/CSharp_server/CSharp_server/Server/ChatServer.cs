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
            Console.WriteLine("[SERVER]Server started");
        }

        public static void StartListening()
        {
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Console.WriteLine("[SERVER]A user is logging in ...");
                handleClient client = new handleClient();
                client.startClient(clientSocket);
            }
        }

        public static void removeUser(User u)
        {
            if (chattingUsers.Contains(u))
                chattingUsers.Remove(u);
            else
                Console.WriteLine("[SERVER]User " + u.login + " is not registered");
        }
        public static User getUser(string login)
        {
            foreach (User u in chattingUsers)
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
                Console.WriteLine("[SERVER]User " + u.login + " is already logged");
        }
    }

    public class handleClient
    {
        TcpClient clientSocket;
        NetworkStream ns;
        Thread ctThread;

        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            ctThread = new Thread(Auth);
            ns = clientSocket.GetStream();
            ctThread.Start();
            ctThread.Join();
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

                    PingPacket pp = new PingPacket();
                    Packet.Send(pp, ns);
                    if (!(clientSocket.Connected))
                    {
                        return;
                    }

                    if (!(clientSocket.Connected))
                    { return; }

                    Packet packet = null;
                    try
                    {
                        packet = Packet.Receive(ns);
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine(e);
                    }

                    if (packet is AuthPacket)
                    {
                        AuthPacket ap = (AuthPacket)packet;
                        Console.WriteLine("[AUTHENTIFICATION]User " + ap.login + " attempting to connect");
                        int flag = am.authentify(ap.login, ap.password);
                        LoginPacket bp = new LoginPacket(flag);
                        if (flag == 1)
                        {
                            /*User logged in*/
                            Console.WriteLine("[AUTHENTIFICATION]User " + ap.login + " connected");
                            User u = new User(ap.login, ap.password, ns);
                            u.chatter = new Chatter(ap.login);
                            ChatServer.addUser(u);
                            Packet.Send(bp, ns);

                            /*Displaying topics to user*/
                            Console.Write("[SERVER]Sending topics : ");
                            Console.Write("[");
                            // à modifier pour virer la vigule
                            for (int i = 0; i < tm.getRooms().Count; ++i)
                            {
                                if (i == tm.getRooms().Count - 1)
                                    Console.Write(tm.getRooms()[i]);
                                else
                                    Console.Write(tm.getRooms()[i] + ", ");
                            }
                            Console.WriteLine("]");

                            TopicsPacket tp = new TopicsPacket(tm.getRooms());
                            Packet.Send(tp, ns);
                        }
                        else
                        {
                            Console.WriteLine("[AUTHENTIFICATION]Error, unable to connect the user " + ap.login + ", error code : " + flag);
                            Packet.Send(bp, ns);
                            this.Auth();
                        }
                    }
                    if (packet is SubscribePacket)
                    {
                        /* sending the subscribe packet */
                        SubscribePacket sb = (SubscribePacket)packet;
                        SubscribeValidation sv = new SubscribeValidation(am.addUser(sb.login, sb.password));
                        if (sv.value)
                            Console.WriteLine("[REGISTER]User " + sb.login + " successfully created");
                        else
                            Console.WriteLine("[REGISTER]Error during " + sb.login + " account creation");
                        Packet.Send(sv, ns);
                    }
                    if (packet is JoinChatRoomPacket)
                    {
                        JoinChatRoomPacket jcp = (JoinChatRoomPacket)packet;

                        if (tm.getRooms().Contains(jcp.chatRoom))
                        {
                            bool flag = tm.joinTopic(jcp.chatRoom, ChatServer.getUser(jcp.user));
                            if (flag)
                                Console.WriteLine("[TOPICS]User " + jcp.user + " joined chatroom : " + jcp.chatRoom);
                            else
                                Console.WriteLine("[TOPICS]Cannont connect user to the chatroom, user : " + jcp.user + " is already in the chatroom : " + jcp.chatRoom);
                            Chatroom ct = (Chatroom)tm.getRoom(jcp.chatRoom);
                            ListChatterPacket lcp = new ListChatterPacket(ct.getChatters(), jcp.chatRoom);
                            Thread.Sleep(500);
                            foreach (User u in ct.chatters)
                            {
                                Console.WriteLine("[TOPICS]Sending user list from " + jcp.chatRoom + " to user " + u.login);
                                Packet.Send(lcp, u.ns);
                            }
                        }
                    }

                    if (packet is CreateChatRoomPacket)
                    {
                        bool flag;
                        CreateChatRoomValidationPacket ccvp;
                        CreateChatRoomPacket ccp = (CreateChatRoomPacket)packet;
                        if (!tm.topics.ContainsKey(ccp.chatRoom))
                        {
                            flag = tm.createTopic(ccp.chatRoom);
                            if (flag)
                            {
                                Console.WriteLine("[TOPICS]User " + ccp.user + " created chatroom : " + ccp.chatRoom);
                                TopicsPacket tp = new TopicsPacket(tm.getRooms());
                                foreach (User u in ChatServer.chattingUsers)
                                    Packet.Send(tp, u.ns);
                            }
                            /* Broadcast la création de room avec affichage de message */

                            ccvp = new CreateChatRoomValidationPacket(flag, ccp.chatRoom);
                        }
                        else
                        {
                            flag = false;
                            Console.WriteLine("[TOPICS]Chatroom creation failed, chatroom : " + ccp.chatRoom + " already exists");
                            ccvp = new CreateChatRoomValidationPacket(flag, ccp.chatRoom);
                        }
                        Packet.Send(ccvp, ns);
                    }
                    if (packet is MessagePacket)
                    {
                        MessagePacket mp = (MessagePacket)packet;
                        Console.WriteLine("[CHATROOM]Posting message : [" + mp.message + "] in chatroom :" + mp.chatroom + " by user : " + mp.user);
                        Chatroom cible = (Chatroom)tm.topics[mp.chatroom];
                        cible.post(mp.message, ChatServer.getUser(mp.user));
                    }

                    if (packet is LeaveChatRoomPacket)
                    {
                        Object thisLock = new Object();
                        LeaveChatRoomPacket lcrp = (LeaveChatRoomPacket)packet;
                        Chatroom cible = (Chatroom)tm.topics[lcrp.chatRoom];
                        Console.WriteLine("[CHATROOM]User : " + lcrp.user + " is leaving chatroom : " + lcrp.chatRoom);
                        lock (thisLock)
                        {
                            cible.quit(ChatServer.getUser(lcrp.user));
                            Chatroom ct = (Chatroom)tm.getRoom(lcrp.chatRoom);
                            ListChatterPacket lcp = new ListChatterPacket(ct.getChatters(), lcrp.chatRoom);
                            Thread.Sleep(1000);
                            foreach (User u in ct.chatters)
                            {
                                Console.WriteLine("[TOPICS]Sending user list from " + lcrp.chatRoom + " to user " + u.login);
                                Packet.Send(lcp, u.ns);
                            }
                            //ChatServer.removeUser(ChatServer.getUser(lcrp.user));
                        }
                    }
                    if (packet is WhisperMessagePacket)
                    {
                        WhisperMessagePacket wmp = (WhisperMessagePacket)packet;
                        Console.WriteLine("[CHATROOM]Whipering message : [" + wmp.message + "] from user " + wmp.user + " to user " + wmp.target + " on chatroom " + wmp.chatroom);
                        Chatroom cible = (Chatroom)tm.topics[wmp.chatroom];
                        cible.whisper(wmp.message, ChatServer.getUser(wmp.user), ChatServer.getUser(wmp.target));
                    }

                    if (packet is DisconnectPacket)
                    {
                        DisconnectPacket dp = (DisconnectPacket)packet;
                        Thread.Sleep(100);
                        User u = ChatServer.getUser(dp.user);
                        u.ns.Close();
                        ChatServer.removeUser(u);
                        this.clientSocket.Close();
                        Console.WriteLine("[SERVER]A user disconnected properly");
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("[SERVER]A user crashed");
                ctThread.Join();

            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("[TOPICS]Impossible de retirer le client, celui-ci n'existe pas dans la chatroom");
                Console.WriteLine(ex);
            }
        }
    }
}

