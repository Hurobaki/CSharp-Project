using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

using chatLibrary;
using System.Threading;
using System.Diagnostics;

namespace CSharp_Client
{
    public partial class Form1 : Form
    {

        public NetworkStream stream;
        public TcpClient client;

        public Form1()
        {
            InitializeComponent();   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient("192.168.43.95", 1337);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Le serveur distant n'est pas connecté");
                Console.ReadLine();
            }
            

            stream = client.GetStream();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            AuthPaquet ap = new AuthPaquet(textBox1.Text, textBox2.Text);

            Paquet.Send(ap, stream);

            //Thread.Sleep(100);

            Paquet paquet = Paquet.Receive(stream);

            if(paquet is loginPaquet)
            {
                loginPaquet bp = (loginPaquet)paquet;

                if (bp.value == 1)
                    MessageBox.Show("Welcome "+textBox1.Text, "Connexion successful !", MessageBoxButtons.OK);
                else if(bp.value == 2)
                    MessageBox.Show("Wrong password ", "Connexion failed !", MessageBoxButtons.OK);
                else
                    MessageBox.Show("USer Unknown", "Connexion failed !", MessageBoxButtons.OK);
            }

            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
