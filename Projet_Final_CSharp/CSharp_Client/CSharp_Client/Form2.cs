using System;
using System.Windows.Forms;
using System.Threading;

using chatLibrary;


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
                    //comboBox1.Items.Add(cc.chatroom);
                }
            }
        }

        private void createTopic_button_Click(object sender, EventArgs e)
        {
            CreateChatRoomPacket cc = new CreateChatRoomPacket(createTopic_textbox.Text);
            Packet.Send(cc, Form1.stream);

            Thread.Sleep(100);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
