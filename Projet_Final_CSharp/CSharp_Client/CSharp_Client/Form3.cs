using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;

using chatLibrary;

namespace CSharp_Client
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            output_text.BackColor = System.Drawing.Color.White;
            chatters.BackColor = System.Drawing.Color.White;

            checkBox1.Checked = true;
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

        delegate void SetTextCallback(string user, string msg);

        public void displayMessage(String user, String msg)
        {
            if (this.output_text.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(displayMessage);
                this.Invoke(d, new string[] { user, msg });
            }
            else
            {
                output_text.Text += user + " says : " + msg + "\r\n";
            }
        }

        delegate void UpdateCallBack(List<string> c);

        public void updateChatters(List<string> c)
        {
            if (this.output_text.InvokeRequired)
            {
                UpdateCallBack d = new UpdateCallBack(updateChatters);
                this.Invoke(d, new object[] { c });
            }
            else
            {
                chatters.Text = "";
                foreach (string s in c)
                {
                    chatters.Text += s + "\r\n";
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
            }
        }
    }
}
