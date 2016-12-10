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
    public class ChatServer
    {
        // Liste des utilisateurs connectés à notre application
        private static List<User> _chattingUsers = new List<User>();
        private static TcpListener _ServerSocket = new TcpListener(IPAddress.Any, 25565);

        public static List<User> chattingUsers
        {
            get { return _chattingUsers; }
            set { _chattingUsers = value; }
        }

        public static TcpListener ServerSocket
        {
            get { return _ServerSocket; }
            set { _ServerSocket = value; }
        }

        public static void StartServer()
        {
            ServerSocket.Start();
            Console.WriteLine("[SERVER]Server started");
        }

        /* Fonction d'écoute de nouvelles connexions */
        public static void StartListening()
        {
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Console.WriteLine("[SERVER]A user is logging in ...");
                handleClient client = new handleClient();
                // Fonction de démarrage du thread client
                client.startClient(clientSocket);
            }
        }

        /* Fonction de retrait d'utilisateur dans la liste d'utilisateur connectés */
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

        /* Fonction d'ajout d'utilisateur dans la liste d'utilisateur */
        public static void addUser(User u)
        {
            if (!chattingUsers.Contains(u))
                chattingUsers.Add(u);
            else
                Console.WriteLine("[SERVER]User " + u.login + " is already logged");
        }
    }

    /* Classe de gestion du client */
    public class handleClient
    {
        TcpClient clientSocket;
        NetworkStream ns;
        Thread ctThread;

        /* Fonction de démarrage de la thread  */
        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            ns = clientSocket.GetStream();
            ctThread = new Thread(Auth);
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
                    /* Vérification de la connexion avec le client */
                    if (!(clientSocket.Connected))
                        ctThread.Abort();

                    /* Nous avons créé chatLibrary.dll qui nous apporte les différents paquets utilisables par notre application*/
                    Packet packet = null;
                    try
                    {
                        packet = Packet.Receive(ns);
                    }
                    catch (SerializationException e)
                    {
                        Console.WriteLine(e);
                    }

                    /* Packet de login */
                    if (packet is AuthPacket)
                    {
                        AuthPacket ap = (AuthPacket)packet;
                        Console.Write("[AUTHENTIFICATION]User " + ap.login + " attempting to connect");
                        // flag contient le code d'erreur 
                        int flag = am.authentify(ap.login, ap.password);
                        // packet de callback
                        LoginPacket bp = new LoginPacket(flag);
                        if (flag == 1)
                        {
                            /* User logged in */
                            Console.WriteLine(" SUCCESS");
                            User u = new User(ap.login, ap.password, ns);
                            u.chatter = new Chatter(ap.login);
                            ChatServer.addUser(u);
                            Packet.Send(bp, ns);

                            /* Displaying topics to user */
                            Console.Write("[SERVER]Sending topics : ");
                            Console.Write("[");
                            for (int i = 0; i < tm.getRooms().Count; ++i)
                            {
                                if (i == tm.getRooms().Count - 1)
                                    Console.Write(tm.getRooms()[i]);
                                else
                                    Console.Write(tm.getRooms()[i] + ", ");
                            }
                            Console.WriteLine("]");
                            // Envoie des chatrooms disponibles
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

                    /* Paquet d'inscription d'un nouvel utilisateur */
                    if (packet is SubscribePacket)
                    {
                        SubscribePacket sb = (SubscribePacket)packet;
                        SubscribeValidation sv = new SubscribeValidation(am.addUser(sb.login, sb.password));
                        if (sv.value)
                            Console.WriteLine("[REGISTER]User " + sb.login + " successfully created");
                        else
                            Console.WriteLine("[REGISTER]Error during " + sb.login + " account creation");
                        Packet.Send(sv, ns);
                    }

                    /* Paquet pour rejoindre une chatroom */
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

                    /* Paquet pour créer une chatroom */
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

                    /* Paquet d'envoie de message */
                    if (packet is MessagePacket)
                    {
                        MessagePacket mp = (MessagePacket)packet;
                        Console.WriteLine("[CHATROOM]Posting message : [" + mp.message + "] in chatroom :" + mp.chatroom + " by user : " + mp.user);
                        Chatroom cible = (Chatroom)tm.topics[mp.chatroom];
                        cible.post(mp.message, ChatServer.getUser(mp.user));
                    }

                    /* Paquet pour quitter chatroom */
                    if (packet is LeaveChatRoomPacket)
                    {
                        Object thisLock = new Object();
                        LeaveChatRoomPacket lcrp = (LeaveChatRoomPacket)packet;
                        Chatroom cible = (Chatroom)tm.topics[lcrp.chatRoom];
                        Console.WriteLine("[CHATROOM]User : " + lcrp.user + " is leaving chatroom : " + lcrp.chatRoom);
                        /* Il faut lock ici pour éviter un problème dans la liste des utilisateurs
                           Nous evitons ainsi d'envoyer un message à un utilisateur déjà déconnecté */
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
                        }
                    }

                    /* PAS IMPLÉMENTÉ CÔTÉ CLIENT */
                    if (packet is WhisperMessagePacket)
                    {
                        WhisperMessagePacket wmp = (WhisperMessagePacket)packet;
                        Console.WriteLine("[CHATROOM]Whipering message : [" + wmp.message + "] from user " + wmp.user + " to user " + wmp.target + " on chatroom " + wmp.chatroom);
                        Chatroom cible = (Chatroom)tm.topics[wmp.chatroom];
                        cible.whisper(wmp.message, ChatServer.getUser(wmp.user), ChatServer.getUser(wmp.target));
                    }

                    /* Paquet de déconnexion d'un utilisateur */
                    if (packet is DisconnectPacket)
                    {
                        DisconnectPacket dp = (DisconnectPacket)packet;
                        Thread.Sleep(200);
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