using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//used to make connetion with local server
using System.Net;
using System.Net.Sockets;
using System.Threading;

//used by arduino
using System.Diagnostics;
using System.IO.Ports;

namespace ConsoleSimHub
{
    class zProgram
    {
        //global variables
        static string comPort = "";
        static bool startUpTrue = true;
        static bool comPortOpen = false;

        private static void Main()
        {

            string IP = "127.0.0.1";
            int port = 20777;

            #region startup
            Console.WriteLine("UserName");
            string userName = Console.ReadLine().ToLower();
            Console.WriteLine("please select you comport");
            comPort = "COM" + Console.ReadLine();
            if (userName == "ryan" || userName == "r" || userName == "maarten" || userName == "m")
            {
                Console.WriteLine("Welcome " + userName + ". Do you want to launch dirt 3");
                string answerLaunchDirt3 = Console.ReadLine().ToLower();
                if (answerLaunchDirt3 == "yes" || answerLaunchDirt3 == "y")
                {
                    Dirt3Launcher(userName);
                    Dirt3CrashLauncher(userName);
                }
                else if (answerLaunchDirt3 == "no" || answerLaunchDirt3 == "n")
                {
                    Console.WriteLine("You did not launch dirt 3");
                    Dirt3CrashLauncher(userName);
                }
                else
                {
                    Console.WriteLine("You dit not enter yes or no exiting startup");
                    Console.WriteLine("You will still be able to recive data from the game");
                    startUpTrue = false;
                }
            }
            else
            {
                Console.WriteLine("You did not enter a valid username");
                Credits();
            }
            #endregion

            #region receive
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
            #endregion
        }

        #region methods used in startup
        private static void Credits()
        {
            Console.WriteLine("\nHave fun with the program");
            Console.WriteLine("Credits to Maarten Jakobs & Max van den Boom & Ryan van den Broek");
            Console.WriteLine("CopyRight ROC terAA\n");
        }

        private static void Dirt3Launcher(string userName)
        {
            if (userName == "maarten" || userName == "m")
            {
                System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Steam\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
            }
            else if (userName == "ryan" || userName == "r")
            {
                System.Diagnostics.Process.Start(@"D:\STEAM\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
            }
        }

        private static void Dirt3CrashLauncher(string userName)
        {
            Console.WriteLine("Do you want to launch the crash fix");
            string answerDirt3CrashFix = Console.ReadLine().ToLower();
            if (answerDirt3CrashFix == "yes" || answerDirt3CrashFix == "y")
            {
                if (userName == "maarten" || userName == "m")
                {
                    System.Diagnostics.Process.Start(@"C:\Users\Maarten Jakobs\Documents\School\Proftaak\Proftaak-Simhub\DiRTTelemetryErrorFix_Release\DiRTTelemetryErrorFix.exe");
                }
                else if (userName == "ryan" || userName == "r")
                {
                    System.Diagnostics.Process.Start(@"D:\proftaak\DiRTTelemetryErrorFix_Release\DiRTTelemetryErrorFix.exe");
                }
                Credits();                
            }
            else
            {
                Console.WriteLine("You did not launch dirt 3 error fix");
                Credits();
            }
        }
        #endregion

        private static void ReceiveData()
        {
            //initiallize connection with arduino
            SerialPort serialPortArduinoConnection = new SerialPort();
            if (startUpTrue == true)
            {
                try
                {
                    //checks if the comport is open
                    serialPortArduinoConnection.PortName = comPort;
                    serialPortArduinoConnection.Open();
                    comPortOpen = true;
                }
                catch
                {
                    Console.WriteLine("Comport is not avalible");
                }
            }
            string breakingChar = "";
            string currentGear = "";

            UdpClient client = new UdpClient(20777);
            while (true)
            {
                try
                {
                    //makes a connection with a local server to recive data from the game
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 20777);
                    byte[] dataGame = client.Receive(ref anyIP);

                    #region Round
                    float round = BitConverter.ToSingle(dataGame, 144) + 1;
                    string roundString = Convert.ToString(round);
                    #endregion

                    #region Gear
                    float gearing = BitConverter.ToSingle(dataGame, 132);
                    if (gearing == 0)
                    {
                        currentGear = "N";
                    }
                    else if (gearing == 64)
                    {
                        currentGear = "2";
                    }
                    else if (gearing == 10)
                    {
                        currentGear = "R";
                    }
                    else if (gearing == 128)
                    {
                        currentGear = "3";
                    }
                    else if (gearing == 191)
                    {
                        currentGear = "1";
                    }
                    else if (gearing == 192)
                    {
                        currentGear = "4";
                    }
                    else if (gearing == 224)
                    {
                        currentGear = "5";
                    }
                    else if (gearing == 256)
                    {
                        currentGear = "6";
                    }
                    else
                    {
                        currentGear = Convert.ToString(gearing);
                    }
                    #endregion

                    #region Speed
                    float speed = BitConverter.ToSingle(dataGame, 28);
                    string speedString = Convert.ToString(Math.Round(speed * 3.6, 0));
                    #endregion

                    #region RPM
                    float RPM = BitConverter.ToSingle(dataGame, 148);
                    string RPMString = Convert.ToString(Math.Round(RPM * 10));
                    #endregion

                    #region Brake
                    bool braking = false;
                    float brakes = BitConverter.ToSingle(dataGame, 124);
                    if (brakes != 0)
                    {
                        braking = true;
                        breakingChar = "A";
                    }
                    else
                    {
                        braking = false;
                        breakingChar = "B";
                    }
                    #endregion

                    #region Total Time
                    float totalTime = BitConverter.ToSingle(dataGame, 0);
                    TimeSpan resultTotalTime = TimeSpan.FromSeconds(totalTime - 13);
                    string totalTimeString = resultTotalTime.ToString("mm':'ss");
                    #endregion

                    #region Lap Time
                    float lapTime = BitConverter.ToSingle(dataGame, 4);
                    TimeSpan resultLapTime = TimeSpan.FromSeconds(totalTime - 13);
                    string lapTimeString = resultLapTime.ToString("mm':'ss");
                    #endregion

                    #region Position
                    float Position = BitConverter.ToSingle(dataGame, 144);
                    string PositionString = Convert.ToString(Position + 1);
                    #endregion

                    //writes the converted data you get from the game in the console
                    //Console.WriteLine(" Lap Time: " + lapTimeString + " \n Lap: " + round + " \n Total Time: " + totalTimeString + " \n Pos: " + (Math.Round(Position + 1)) + " \n Speed: " + (Math.Round(speed * 3.6, 0)) + " KPH \n RMP: " + Math.Round(RPM * 10, 0) + "\n Gear: " + currentGear + " \n Braking: " + braking + "\n");

                    //if you have decided to enter startup and the comport is open it will start sending data to arduino
                    if (startUpTrue == true)
                    {
                        var speedArray = speedString.ToCharArray();
                        var RPMArray = RPMString.ToCharArray();
                        var timeArray = totalTimeString.ToCharArray();
                        var roundTimeArray = lapTimeString.ToCharArray();

                        char[] dataToSend = new char[21];

                        if (comPortOpen == true)
                        {
                            #region garbage filter
                            dataToSend[0] = Convert.ToChar("a");
                            dataToSend[1] = Convert.ToChar("b");
                            #endregion

                            #region Gear
                            if (currentGear != "")
                            {
                                dataToSend[2] = Convert.ToChar(currentGear);
                            }
                            #endregion

                            #region Brakes
                            if (breakingChar != "")
                            {
                                dataToSend[10] = Convert.ToChar(breakingChar);
                            }
                            #endregion

                            #region Speed
                            if (speedArray.Count() > 2)
                            {
                                dataToSend[3] = speedArray[0];
                                dataToSend[4] = speedArray[1];
                                dataToSend[5] = speedArray[2];
                            }
                            else if (speedArray.Count() > 1)
                            {
                                dataToSend[3] = Convert.ToChar("0");
                                dataToSend[4] = speedArray[0];
                                dataToSend[5] = speedArray[1];
                            }
                            else
                            {
                                dataToSend[3] = Convert.ToChar("0");
                                dataToSend[4] = Convert.ToChar("0");
                                dataToSend[5] = Convert.ToChar("0");
                            }
                            #endregion

                            #region RPM
                            if (Convert.ToInt16(RPMString) > 1000)
                            {
                                dataToSend[6] = RPMArray[0];
                                dataToSend[7] = RPMArray[1];
                                dataToSend[8] = RPMArray[2];
                                dataToSend[9] = RPMArray[3];
                            }
                            else
                            {
                                dataToSend[6] = Convert.ToChar(0);
                                dataToSend[7] = Convert.ToChar(0);
                                dataToSend[8] = Convert.ToChar(0);
                                dataToSend[9] = Convert.ToChar(0);
                            }
                            #endregion



                            #region Total Time
                            if (timeArray.Count() > 1)
                            {
                                dataToSend[11] = timeArray[0];
                                dataToSend[12] = timeArray[1];
                                dataToSend[13] = timeArray[3];
                                dataToSend[14] = timeArray[4];
                            }
                            #endregion

                            #region Round Time
                            if (roundTimeArray.Count() > 1)
                            {
                                dataToSend[15] = roundTimeArray[0];
                                dataToSend[16] = roundTimeArray[1];
                                dataToSend[17] = roundTimeArray[3];
                                dataToSend[18] = roundTimeArray[4];
                            }
                            #endregion

                            #region Position
                            dataToSend[19] = Convert.ToChar(PositionString);
                            #endregion

                            #region Round
                            dataToSend[20] = Convert.ToChar(roundString);
                            #endregion

                            Console.WriteLine(dataToSend[2]);

                            serialPortArduinoConnection.Write(dataToSend, 0, 8);
                        }
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
