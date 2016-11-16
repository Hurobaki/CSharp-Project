using System;
using System.Collections.Generic;
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
        CreateChatRoomValidation
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
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, paquet);
            stream.Flush();
        }

        public static Packet Receive(NetworkStream stream)
        {
            Packet p = null;

            BinaryFormatter bf = new BinaryFormatter();
            p = (Packet)bf.Deserialize(stream);
            

            return p;
        }
        public static Packet ReceiveList(NetworkStream stream)
        {
            Packet p = null;

            BinaryFormatter bf = new BinaryFormatter();
            p = (Packet)bf.Deserialize(stream);
            TopicsPacket tp = (TopicsPacket) p;
            List<string> objects = tp.topics as List<string>;

            return tp;
        }

    }
}