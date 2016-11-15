using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ClientSide
{
    class ChatClient
    {
        static void Main(string[] args)
        {
            
                TcpClient clientSocket = new TcpClient("192.168.0.15", 25565);
                NetworkStream ns = clientSocket.GetStream();
            try
            {
                while (true)
                {
                    string str = Console.ReadLine();
                    BinaryWriter writer = new BinaryWriter(clientSocket.GetStream());
                    writer.Write(str);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Le server distant est déconnecté");
            }
                
                
             
        }
    }
}
