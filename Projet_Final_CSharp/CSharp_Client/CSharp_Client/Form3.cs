using System;
using System.ComponentModel;
using System.Windows.Forms;

using chatLibrary;
using System.Threading;

namespace CSharp_Client
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerAsync();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            LeaveChatRoomPacket lc = new LeaveChatRoomPacket(this.Text, Form1.login);
            Packet.Send(lc, Form1.stream);

            Thread.Sleep(100);
        }

        private void Send_Click(object sender, EventArgs e)
        {
            MessagePacket message = new MessagePacket(Form1.login, this.Text, input_text.Text);

            Packet.Send(message, Form1.stream);

            Thread.Sleep(100);


            /*output_text.Text += "\r\n" + Form1.login + " : " + input_text.Text;*/

            input_text.Text = "";

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Packet paquet = Packet.Receive(Form1.stream);

                if (paquet is MessageBroadcastPacket)
                {
                    MessageBroadcastPacket mb = (MessageBroadcastPacket)paquet;
                    output_text.Text += mb.user + " says : " + mb.message + "\r\n";
                }
            }
        }
    }
}
