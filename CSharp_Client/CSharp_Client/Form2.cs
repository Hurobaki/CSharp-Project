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
                     Debug.WriteLine(s);
                    comboBox1.Items.Add(s);
                 }
             }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Exit();
        }
    }
}
