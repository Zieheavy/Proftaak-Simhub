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
                    //int posgear = 132;
                    //float gearing = (data[posgear] & 0xff) + (data[posgear + 1] & 0xff) + (data[posgear + 2] & 0xff) + (data[posgear + 3] & 0xff);
                    //console.writeline(gearing);
                    //if (ttime == 0)
                    //{
                    //    console.writeline("gear: n");
                    //}
                    //else if (ttime == 64)
                    //{
                    //    console.writeline("gear: 2");
                    //}
                    //else if (ttime == 97)
                    //{
                    //    console.writeline("gear: r");
                    //}
                    //else if (ttime == 128)
                    //{
                    //    console.writeline("gear: 3");
                    //}
                    //else if (ttime == 191)
                    //{
                    //    console.writeline("gear: 1");
                    //}
                    //else if (ttime == 192)
                    //{
                    //    console.writeline("gear: 4");
                    //}
                    //else if (ttime == 224)
                    //{
                    //    console.writeline("gear: 5");
                    //}
                    //else
                    //{
                    //    console.writeline(ttime);
                    //}
                    //#endregion
                    //#region Speed(28)
                    //int posSpeed = 132;
                    //float speed = (data[posSpeed] & 0xff) | (data[posSpeed + 1] & 0xff) | (data[posSpeed + 2] & 0xff) | (data[posSpeed + 3] & 0xff);
                    //Console.WriteLine(">>" + speed);
                    //#endregion
                    #region Brake
                    int posBrake = 124;
                    float brakes = (data[posBrake] & 0xff) | ((data[posBrake + 1] & 0xff) << 8) | ((data[posBrake + 2] & 0xff) << 16) | ((data[posBrake + 3] & 0xff) << 24);
                    if (brakes != 0)
                        Console.WriteLine(brakes);
                    {
                        Console.WriteLine("Braking");
                    }
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
