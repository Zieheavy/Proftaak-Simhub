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
                    #region Gear
                    int posgear = 132;
                    string currentgear = "N";
                    float gearing = (data[posgear] & 0xff) + (data[posgear + 1] & 0xff) + (data[posgear + 2] & 0xff) + (data[posgear + 3] & 0xff);
                    if (gearing == 0)
                    {
                        currentgear = "N";
                    }
                    else if (gearing == 64)
                    {
                        currentgear = "2";
                    }
                    else if (gearing == 97)
                    {
                        currentgear = "R";
                    }
                    else if (gearing == 128)
                    {
                        currentgear = "3";
                    }
                    else if (gearing == 191)
                    {
                        currentgear = "1";
                    }
                    else if (gearing == 192)
                    {
                        currentgear = "4";
                    }
                    else if (gearing == 224)
                    {
                        currentgear = "5";
                    }
                    else
                    {
                        currentgear = Convert.ToString(gearing);
                    }
                    #endregion
                    #region Speed(28)
                    int posSpeed = 132;
                    float speed = (data[posSpeed] & 0xff) | (data[posSpeed + 1] & 0xff) | (data[posSpeed + 2] & 0xff) | (data[posSpeed + 3] & 0xff);
                    Console.WriteLine(">>" + speed);
                    #endregion
                    
                    #region Brake
                    int posBrake = 124;
                    bool braking = false;
                    float brakes = (data[posBrake] & 0xff) | ((data[posBrake + 1] & 0xff) << 8) | ((data[posBrake + 2] & 0xff) << 16) | ((data[posBrake + 3] & 0xff) << 24);
                    if (brakes != 0)
                    {
                       braking = true;
                    }
                    else
                    {
                        braking = false;
                    }
                    #endregion
                    Console.WriteLine("Speed: " + speed+ " KPH \n Gear: "+currentgear+" \n Braking: "+braking);
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.ToString());
                }
            }
        }

    }
}
