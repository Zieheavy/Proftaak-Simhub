using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimHub2
{
    public partial class Form1 : Form
    {
        // UDP Socket for the connection
        private UdpClient udpClient;

        private bool isSending;
        public Form1()
        {
            InitializeComponent(); isSending = false;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            var packet = new TelemetryPacket();
            var byteMessage = PacketUtilities.ConvertPacketToByteArray(packet);

            udpClient.Send(byteMessage, byteMessage.Length);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!isSending)
            {
                udpClient = new UdpClient();
                var ipAddress = IPAddress.Parse(label1.Text);
                const int port = 20777;
                udpClient.Connect(ipAddress, port);

                isSending = true;

                button1.Text = "Stop Sending";
                timer1.Interval = 100;
                timer1.Start();

            }
            else
            {
                isSending = false;
                timer1.Stop();
                udpClient.Close();
                button1.Text = "Start Sending";
            }     
        }
    }
}
