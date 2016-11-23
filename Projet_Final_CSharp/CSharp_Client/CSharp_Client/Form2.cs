using System;
using System.Windows.Forms;
using System.Threading;

using chatLibrary;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

namespace CSharp_Client
{
    public partial class Form2 : Form
    {
        public static HashSet<Form3> _forms = new HashSet<Form3>();

        public Form2()
        {
            InitializeComponent();

            

            Packet paquet = Packet.ReceiveList(Form1.stream);

            if (paquet is TopicsPacket)
            {
                TopicsPacket bp = (TopicsPacket)paquet;

                foreach (string s in bp.topics)
                {
                    comboBox1.Items.Add(s);
                }
            }

            CreateTopicWorker.RunWorkerAsync();
        }

        public delegate void ran(string user, string message);

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label1.Text = comboBox1.SelectedItem.ToString();
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JoinChatRoomPacket jc = new JoinChatRoomPacket(label1.Text.ToString(), Form1.login);
            Packet.Send(jc, Form1.stream);

            Thread.Sleep(100);

            Form3 f3 = new Form3();
            f3.Text = label1.Text.ToString();
            _forms.Add(f3);
            f3.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
                Application.Exit();
        }

        private void CreateTopicWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            while(true)
            {
                Packet paquet = Packet.Receive(Form1.stream);

                if (paquet is CreateChatRoomValidationPacket)
                {
                    CreateChatRoomValidationPacket cc = (CreateChatRoomValidationPacket)paquet;

                    if(cc.value)
                    {
                        MessageBox.Show("Topic created", "Success", MessageBoxButtons.OK);
                        comboBox1.Items.Add(cc.chatRoom);
                    }
                    else
                    {
                        MessageBox.Show("Error", "Error", MessageBoxButtons.OK);
                    }
                }

                if (paquet is MessageBroadcastPacket)
                {
                    Debug.WriteLine("flag");
                    MessageBroadcastPacket mb = (MessageBroadcastPacket)paquet;
                    Debug.WriteLine(mb.message);
                    foreach(Form3 f3 in _forms)
                    {
                        Debug.WriteLine("flag2");
                        if (f3.Text.Equals(mb.chatroom))
                        {
                            f3.displayMessage(mb.user, mb.message);
                        }
                    }
                }
            }
        }

        private void createTopic_button_Click(object sender, EventArgs e)
        {
            CreateChatRoomPacket cc = new CreateChatRoomPacket(createTopic_textbox.Text);
            Packet.Send(cc, Form1.stream);
            Thread.Sleep(100);

            createTopic_textbox.Text = "";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
