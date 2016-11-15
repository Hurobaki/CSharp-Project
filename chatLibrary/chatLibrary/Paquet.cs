using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace chatLibrary
{
    public enum TypePaquet
    {
        Authentification,
        Login
    }

    [Serializable()] // Pour que la classe soit sérialisable
    public class Paquet //Une superclasse pour les paquets
    {
        public TypePaquet Type { get; protected set; }

        public Paquet(TypePaquet Type)
        {
            this.Type = Type;
        }

        //Méthode statique pour l'envoi et la réception
        public static void Send(Paquet paquet, NetworkStream stream)
        {
            BinaryFormatter bf = new BinaryFormatter();

            bf.Serialize(stream, paquet);
            stream.Flush();
        }

        public static Paquet Receive(NetworkStream stream)
        {
            Paquet p = null;

            BinaryFormatter bf = new BinaryFormatter();
            p = (Paquet)bf.Deserialize(stream);

            return p;
        }
    }
}
