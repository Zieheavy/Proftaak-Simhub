using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//newely added
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.IO.Ports;

namespace ConsoleSimHub
{
    class zProgram
    {
        static string ComPort = "";
        static bool StartUpTrue = true;

        private static void Main()
        {

            string IP = "127.0.0.1";
            int port = 20777;

            #region boot up sequence
            Console.WriteLine("Do you want to enter startup");
            if (Console.ReadLine().ToLower() == "yes")
            {

                Console.WriteLine("Program starting up");
                Console.WriteLine("please enter what comport your arduino is on");
                ComPort = "COM" + Console.ReadLine();
                Console.WriteLine("Your arduino is on: " + ComPort);
                Console.WriteLine("Do you want to launch dirt 3");
                if (Console.ReadLine().ToLower() == "yes")
                {
                    Console.WriteLine("Maarten?");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        //System.Diagnostics.Process.Start(@"D:\STEAM\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
                        try
                        {
                            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Steam\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
                            Console.WriteLine("launching Dirt3");
                        }
                        catch
                        {
                            Console.WriteLine("Not the right Location");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ryan?");
                        if (Console.ReadLine().ToLower() == "ryan")
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(@"D:\STEAM\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
                                Console.WriteLine("launching Dirt3");
                            }
                            catch
                            {
                                Console.WriteLine("Not the right Location");
                            }
                        }
                        else
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(@"D:\STEAM\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
                                Console.WriteLine("launching Dirt3");
                            }
                            catch
                            {
                                Console.WriteLine("Not the right Location");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You decided not to launch dirt3");
                }
                Console.WriteLine("Do you want to launch DiRTTelemetryErrorFix");
                if (Console.ReadLine().ToLower() == "yes")
                {
                    Console.WriteLine("Maarten?");
                    if (Console.ReadLine().ToLower() == "yes")
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(@"C:\Users\Maarten Jakobs\Documents\School\Proftaak\Proftaak-Simhub\DiRTTelemetryErrorFix_Release\DiRTTelemetryErrorFix.exe");
                            Console.WriteLine("launching DiRTTelemetryErrorFix");
                        }
                        catch
                        {
                            Console.WriteLine("Not the right Location");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ryan?");
                        if (Console.ReadLine().ToLower() == "yes")
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(@"D:\proftaak\DiRTTelemetryErrorFix_Release\DiRTTelemetryErrorFix.exe");
                                Console.WriteLine("launching DiRTTelemetryErrorFix");
                            }
                            catch
                            {
                                Console.WriteLine("Not the right Location");
                            }
                        }
                        else
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(@"D:\STEAM\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
                                Console.WriteLine("launching Dirt3");
                            }
                            catch
                            {
                                Console.WriteLine("Not the right Location");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("You decided not to launch DiRTTelemetryErrorFix");
                }
            }
            else
            {
                StartUpTrue = false;
                Console.WriteLine("Exiting start up");
            }
            #endregion




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
            SerialPort serialPortArduinoConnection = new SerialPort();
            if (StartUpTrue == true)
            {
                try
                {
                    serialPortArduinoConnection.PortName = ComPort;
                    serialPortArduinoConnection.Open();
                }
                catch
                {
                    Console.WriteLine("Comport is not avalible");
                }
            }
            string BreakingChar = "";
            string GearChar = "";
            UdpClient client = new UdpClient(20777);
            while (true)
            {
                try
                {

                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 20777);
                    byte[] data = client.Receive(ref anyIP);

                    #region Round
                    int posRound = 144;
                    float Round = BitConverter.ToSingle(data, posRound) + 1;
                    #endregion

                    #region Gear
                    int posgear = 132;
                    string currentgear = "N";
                    float gearing = (data[posgear] & 0xff) + (data[posgear + 1] & 0xff) + (data[posgear + 2] & 0xff) + (data[posgear + 3] & 0xff);
                    if (gearing == 0)
                    {
                        currentgear = "N";
                        GearChar = "!";
                    }
                    else if (gearing == 64)
                    {
                        currentgear = "2";
                        GearChar = "\"";
                    }
                    else if (gearing == 97)
                    {
                        currentgear = "R";
                        GearChar = "#";
                    }
                    else if (gearing == 128)
                    {
                        currentgear = "3";

                        GearChar = "$";
                    }
                    else if (gearing == 191)
                    {
                        currentgear = "1";
                        GearChar = "%";
                    }
                    else if (gearing == 192)
                    {
                        currentgear = "4";
                        GearChar = "&";
                    }
                    else if (gearing == 224)
                    {
                        currentgear = "5";

                        GearChar = "\'";
                    }
                    else if (gearing == 256)
                    {
                        currentgear = "6";
                        GearChar = "(";
                    }
                    else
                    {
                        currentgear = Convert.ToString(gearing);
                    }
                    #endregion

                    #region Speed
                    int posSpeed = 28;
                    float speed = BitConverter.ToSingle(data, posSpeed);
                    string SpeedString = Convert.ToString(Math.Round(speed * 3.6, 0));
                    #endregion

                    #region RPM
                    int posRPM = 148;
                    float RPM = BitConverter.ToSingle(data, posRPM);
                    string RPMString = Convert.ToString(Math.Round(RPM * 10));
                    #endregion

                    #region Brake
                    int posBrake = 124;
                    bool braking = false;
                    float brakes = BitConverter.ToSingle(data, posBrake);
                    if (brakes != 0)
                    {
                        braking = true;
                        BreakingChar = "A";
                    }
                    else
                    {
                        braking = false;
                        BreakingChar = "B";
                    }
                    #endregion

                    #region Total Time
                    int posTTime = 0;
                    float TTime = BitConverter.ToSingle(data, posTTime);
                    TimeSpan result = TimeSpan.FromSeconds(TTime - 13);
                    string TotalTime = result.ToString("mm':'ss");
                    #endregion

                    #region Lap Time
                    int posLTime = 4;
                    float LTime = BitConverter.ToSingle(data, posLTime);
                    TimeSpan resultL = TimeSpan.FromSeconds(TTime - 13);
                    string LapTime = resultL.ToString("mm':'ss");
                    #endregion

                    #region Pos
                    int posPos = 144;
                    float pos = BitConverter.ToSingle(data, posPos);
                    #endregion

                    //Console.WriteLine(" Lap Time: " + LapTime + " \n Lap: " + Round + " \n Total Time: " + TotalTime + " \n Lap: " + (Math.Round(pos + 1)) + " \n Speed: " + (Math.Round(speed * 3.6, 0)) + " KPH \n RMP: " + Math.Round(RPM * 10, 0) + "\n Gear: " + currentgear + " \n Braking: " + braking + "\n");
                    if (StartUpTrue == true)
                    {
                        var SpeedArray = SpeedString.ToCharArray();
                        var RPMArray = RPMString.ToCharArray();

                        char[] DataToSend = new char[8];

                        #region brakes
                        if (BreakingChar != "")
                        {
                            DataToSend[0] = Convert.ToChar(BreakingChar);
                            Console.WriteLine(BreakingChar);
                        }
                        #endregion

                        #region speed
                        if (SpeedArray.Count() > 2)
                        {
                            DataToSend[1] = SpeedArray[0];
                            DataToSend[2] = SpeedArray[1];
                            DataToSend[3] = SpeedArray[2];
                        }
                        else if (SpeedArray.Count() > 1)
                        {
                            DataToSend[1] = Convert.ToChar("0");
                            DataToSend[2] = SpeedArray[0];
                            DataToSend[3] = SpeedArray[1];
                        }
                        #endregion

                        #region RPM
                        DataToSend[5] = RPMArray[0];
                        DataToSend[6] = RPMArray[1];
                        DataToSend[7] = RPMArray[2];
                        DataToSend[8] = RPMArray[3];
                        #endregion

                        #region gear

                        #endregion

                        serialPortArduinoConnection.Write(DataToSend, 0, 8);
                    }
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.ToString());
                }
            }
        }
    }
}
