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
                client = new TcpClient("192.168.137.129", 25565);
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

            Thread.Sleep(100);
        }
    }
}
