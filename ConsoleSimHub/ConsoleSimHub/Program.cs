using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//newely added
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleSimHub
{
    class Program
    {
        private static void Main()
        {
            string IP = "127.0.0.1";
            int port = 20777;

            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);

            Thread receiveThread = new Thread(ReceiveData);
            receiveThread.IsBackground = true;
            receiveThread.Start();

            UdpClient client = new UdpClient();

            try
            {
                string text;
                do
                {
                    text = Console.ReadLine();

                    if (text.Length != 0)
                    {
                        byte[] data = Encoding.UTF8.GetBytes(text);
                        client.Send(data, data.Length, remoteEndPoint);
                    }
                } while (text.Length != 0);
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            finally
            {
                client.Close();
            }
        }

        private static void ReceiveData()
        {
            UdpClient client = new UdpClient(20777);
            while (true)
            {
                try
                {
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 20777);
                    byte[] data = client.Receive(ref anyIP);
                    //#region Gear
                    //int posGear = 132;
                    //float tTime = (data[posGear] & 0xff) + (data[posGear + 1] & 0xff) + (data[posGear + 2] & 0xff) + (data[posGear + 3] & 0xff);
                    //if (tTime == 0)
                    //{
                    //    Console.WriteLine("Gear: N");
                    //}
                    //else if (tTime == 64)
                    //{
                    //    Console.WriteLine("Gear: 2");
                    //}
                    //else if (tTime == 97)
                    //{
                    //    Console.WriteLine("Gear: R");
                    //}
                    //else if (tTime == 128)
                    //{
                    //    Console.WriteLine("Gear: 3");
                    //}
                    //else if (tTime == 191)
                    //{
                    //    Console.WriteLine("Gear: 1");
                    //}
                    //else if (tTime == 192)
                    //{
                    //    Console.WriteLine("Gear: 4");
                    //}
                    //else if (tTime == 224)
                    //{
                    //    Console.WriteLine("Gear: 5");
                    //}
                    //else
                    //{
                    //    Console.WriteLine(tTime);
                    //}
                    //#endregion
                    #region 28
                    int posSpeed = 132;
                    float speed = (data[posSpeed] & 0xff) + (data[posSpeed + 1] & 0xff) + (data[posSpeed + 2] & 0xff) + (data[posSpeed + 3] & 0xff);
                    Console.WriteLine(">>" +speed);
                    #endregion
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.ToString());
                }
            }
        }

    }
}
