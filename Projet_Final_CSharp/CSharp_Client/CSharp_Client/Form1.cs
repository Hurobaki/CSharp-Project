using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Windows.Forms;

using chatLibrary;
using System.Threading;
using System.Diagnostics;
using System.Drawing;

namespace CSharp_Client
{
    public partial class Form1 : Form
    {
        private static NetworkStream _stream;
        private static TcpClient _client;
        private static string _login;

        public Form1()
        {
            InitializeComponent();   
        }

        public static NetworkStream stream
        {
            get
            {
                return _stream;
            }

            set
            {
                _stream = value;
            }
        }

        public static TcpClient tcpclient
        {
            get
            {
                return _client;
            }

            set
            {
                _client = value;
            }
        }

        public static string login
        {
            get
            {
                return _login;
            }
            
            set
            {
                _login = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                /* Alex IPv4 */
                tcpclient = new TcpClient("192.168.43.95", 25565);
                /* Théo IPv4 */
                //client = new TcpClient("192.168.1.23", 25565);
                //client = new TcpClient("192.168.137.129", 25565);
                stream = tcpclient.GetStream();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Le serveur distant n'est pas connecté. \n\rVeuillez réessayer dans quelques instants.","Error", MessageBoxButtons.OK);
                Application.Exit();
            }

            this.KeyPreview = true;
           
        }

        private void connect_Click(object sender, EventArgs e)
        {

            AuthPacket ap = new AuthPacket(login_input.Text, password_input.Text);

            Packet.Send(ap, stream);

            Thread.Sleep(100);

            Packet paquet = Packet.Receive(stream);

            bool flag = false;

            if(paquet is LoginPacket)
            {
                LoginPacket bp = (LoginPacket)paquet;

                if (bp.value == 1)
                {
                    MessageBox.Show("Welcome " + login_input.Text, "Connexion successful !", MessageBoxButtons.OK);
                    login = login_input.Text;
                    flag = true;
                }
                else if(bp.value == 2)
                {
                    label_log.ForeColor = Color.Red;
                    label_log.Text = "Wrong password, try again.";
                } 
                else
                {
                    label_log.ForeColor = Color.Red;
                    label_log.Text = "User not found, try again.";
                }
                    
            }

            if(flag)
            {
                Form2 f2 = new Form2();
                f2.Text = "Connected as " + login;
                f2.Show();
                this.Hide();
            }
        }

        private void register_Click(object sender, EventArgs e)
        {
            SubscribePacket sb = new SubscribePacket(login_input.Text, password_input.Text);
            Packet.Send(sb, stream);

            Thread.Sleep(100);

            Packet paquet = Packet.Receive(stream);

            if(paquet is SubscribeValidation)
            {
                SubscribeValidation sv = (SubscribeValidation)paquet;

                if(sv.value)
                    MessageBox.Show("New user created !", "Registration completed", MessageBoxButtons.OK);
                else
                    MessageBox.Show("Wrong informations", "Registration failed", MessageBoxButtons.OK);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.Enter:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    connect.PerformClick();
                    break;
            }
        }
    }
}
