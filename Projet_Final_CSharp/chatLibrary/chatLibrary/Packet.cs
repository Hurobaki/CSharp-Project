﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace chatLibrary
{
    public enum TypePacket
    {
        Authentification,
        Login,
        Subscribe,
        SubscribeValidation,
        Topics,
        JoinChatRoom,
        JoinChatRoomValidation,
        CreateChatRoom,
        CreateChatRoomValidation,
        Message,
        MessageBroadcast,
        ChatterList,
        QuitChatRoom,
        QuitChatRoomValidation,
        Disconnect
    }

    [Serializable()] // Pour que la classe soit sérialisable
    public class Packet //Une superclasse pour les paquets
    {
        public TypePacket Type { get; protected set; }
        public string user { get; protected set; }

        public Packet(TypePacket Type)
        {
            this.Type = Type;
        }

        //Méthode statique pour l'envoi et la réception
        public static void Send(Packet paquet, NetworkStream stream)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(stream, paquet);
                stream.Flush();
            }
            catch(Exception e)
            {
                Debug.WriteLine("Serveur distant non connecté");
            }
            
        }

        public static Packet Receive(NetworkStream stream)
        {
            Packet p = null;
            BinaryFormatter bf = new BinaryFormatter();
            p = (Packet)bf.Deserialize(stream);

            if (p is TopicsPacket)
            {
                TopicsPacket tp = (TopicsPacket)p;
                List<string> objects = tp.topics as List<string>;
                return tp;
            }
            if(p is ListChatterPacket)
            {
                ListChatterPacket lcp = (ListChatterPacket)p;
                List<string> objects = lcp.chatters as List<string>;
                return lcp;
            }
            return p;
        }
    }
}