﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Project_CSharp.Exceptions.PersException;
using Project_CSharp.Authentification.Authentification;

namespace Project_CSharp.Chat
{
    namespace Chat
    {
        class TestChat
        {
            private const string PATH = "Test.xml";


            /*static void Main(string[] args)
            {
                Chatter Manuelle = new Chatter("Manuelle"),Léna = new Chatter("Léna");
                TopicsManager tm = new TopicsManager();
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
                Console.ReadLine();


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
                catch(WrongPasswordException e)
                {
                    e.DisplayLogReportShow();
                }
                catch(UserUnknowException e)
                {
                    e.DisplayLogReportShow();
                }
                
                AuthentificationManager aut = null;

                if(File.Exists(PATH))
                {
                    aut = AuthentificationManager.load(PATH);
                    aut.checkRegistredUsers();
                }
                else
                {
                    aut = new AuthentificationManager();
                }


                Console.ReadLine();
            }*/

        }
    }
}
