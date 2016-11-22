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
            OutputDisplay.RunWorkerAsync();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            LeaveChatRoomPacket lc = new LeaveChatRoomPacket(this.Text, Form1.login);

            try
            {
                Packet.Send(lc, Form1.stream);
            }
            catch(Exception ex)
            {
                DialogResult result = MessageBox.Show("Server is disconnected, application is going to close", "Error", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
            }

            Thread.Sleep(100);
        }

        private void Send_Click(object sender, EventArgs e)
        {
            MessagePacket message = new MessagePacket(Form1.login, this.Text, input_text.Text);

            Packet.Send(message, Form1.stream);

            Thread.Sleep(100);

            input_text.Text = "";

        }

        private void OutputDisplay_DoWork(object sender, DoWorkEventArgs e)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                this.KeyPreview = true;
            }
            else
            {
                this.KeyPreview = false;
            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    Send.PerformClick();
                    break;
                case (Keys.Shift | Keys.Enter):
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break; 
            }
        }

        
    }
}
