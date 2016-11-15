using System;
using System.IO;
using CSharp_Graphic_Chat.Chat.Chat;
using CSharp_Graphic_Chat.Authentification.Authentification;
using CSharp_Graphic_Chat.Exceptions.PersException;
using System.Net.Sockets;
using System.Net;
using CSharp_Graphic_Chat.Server;

namespace CSharp_Graphic_Chat
{
    static class Program
    {
        private const string PATH = "Test.xml";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            */
            AuthentificationManager test = new AuthentificationManager();

            try
            {
                test.addUser("Manuelle", "Léna");
                test.addUser("Théo", "123");
                test.addUser("Alexandre", "456");

                test.save("Test.xml");
            }
            catch (UserExistsException e)
            {
                e.DisplayLogReportShow();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine();
            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 1337);
            ServerSocket.Start();
            Console.WriteLine("Server started");
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Console.WriteLine("New client");
                handleClient client = new handleClient();
                client.startClient(clientSocket);
            }
            TopicsManager tm = new TopicsManager();

            /*Chatter Manuelle = new Chatter("Manuelle"), Léna = new Chatter("Léna");
            tm.createTopic("Mon corps tout nu");
            tm.createTopic("La pls, mon histoire");
            Console.WriteLine("The openned topics are : ");

            
            foreach (String s in tm.topics.Keys)
            {
                Console.WriteLine(s);
            }

            Chatroom cr = tm.joinTopic("La pls, mon histoire");
            cr.join(Manuelle);
            cr.join(Léna);
            cr.post("Coucou ma chérie, comment s'est passé ta journée ? Je veux tout savoir ;) <3 <3 <3", Manuelle);
            Console.ReadKey();
            cr.post("J'ai passé le balai toute la journée et toi mon coeur ? :$ <3 <3", Léna);
            Console.ReadKey();
            cr.post("Oh trop bien ! Moi j'ai passé l'aspirateur, j'ai retrouvé une chips derrière mon pc (encore) :@ ", Manuelle);
            Console.ReadKey();
            cr.post("C'est encore tes connards de potes ! De toute façon je peux pas les blairer, surtout Théo ! Il est méchant mais le pire c'est qu'il a raison ! :(", Léna);
            Console.ReadKey();
            cr.quit(Manuelle);
            cr.post("Oh non je suis toute seule ! Manu mon amour reviens moi ! Bon tant pis je vais allé voir un de ses potes comme j'ai l'habitude ! Je suis une coquine hihihi", Léna);
            Console.ReadLine();*/



            /*
            test.checkRegistredUsers();

            try
            {
                test.removeUser("Théo");
            }
            catch (UserUnknowException e)
            {
                e.DisplayLogReportShow();
            }

            test.checkRegistredUsers();

            try
            {
                test.authentify("Alexandre", "456");
            }
            catch (WrongPasswordException e)
            {
                e.DisplayLogReportShow();
            }
            catch (UserUnknowException e)
            {
                e.DisplayLogReportShow();
            }

            AuthentificationManager aut = null;

            if (File.Exists(PATH))
            {
                aut = AuthentificationManager.load(PATH);
                aut.checkRegistredUsers();
            }
            else
            {
                aut = new AuthentificationManager();
            }


            Console.ReadLine();*/
        }
    }
}