using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        UdpClient receivingUdpClient = new UdpClient(11000);

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, System.EventArgs e)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {

                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);

                string returnData = Encoding.ASCII.GetString(receiveBytes);

                label1.Text = ("This is the message you received " +
                                            returnData.ToString());
                Console.WriteLine("This message was sent from " +
                                            RemoteIpEndPoint.Address.ToString() +
                                            " on their port number " +
                                            RemoteIpEndPoint.Port.ToString());
            }
            catch (Exception f)
            {
                Console.WriteLine(f.ToString());
            }
        }
    }
}