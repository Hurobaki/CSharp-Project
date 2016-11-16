using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using chatLibrary;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace CSharp_Client
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();

            Packet paquet = Packet.ReceiveList(Form1.stream);

            if(paquet is TopicsPacket)
             {
                 TopicsPacket bp = (TopicsPacket)paquet;

                 foreach (string s in bp.topics)
                 {
                    comboBox1.Items.Add(s);
                 }
             }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            JoinChatRoomPacket jc = new JoinChatRoomPacket(comboBox1.SelectedItem.ToString(), Form1.login);
            Packet.Send(jc, Form1.stream);

            Thread.Sleep(100);
        }
    }
}
