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
        //this will get filled with the comports in use
        static string comPort = "";
        static string comPort2 = "";

        //this will get set to true if the comport is availible
        static bool comPortOpen = false;
        static bool comPort2Open = false;

        private static void Main()
        {
            string IP = "127.0.0.1";
            int port = 20777;

            #region startup
            //aks for username so that i can change startup location from dirt 3
            Console.WriteLine("UserName");
            string userName = Console.ReadLine().ToLower();

            //ask for the 2 comports in use
            Console.WriteLine("please select you comport");
            comPort = "COM" + Console.ReadLine();
            Console.WriteLine("please select you second comport");
            comPort2 = "COM" + Console.ReadLine();

            if (userName == "r" || userName == "m" || userName == "s")
            {
                if (userName == "r")
                {
                    userName = "ryan";
                }
                else if (userName == "m")
                {
                    userName = "maarten";
                }
                else
                {
                    userName = "simhub";
                }
            }
            if (userName == "ryan" ||  userName == "maarten" ||  userName == "simhub" )
            {
                //welcomes the user logged in
                Console.ForegroundColor = ConsoleColor.Green;
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
                        //gets the bytes from the game and puts them in this array
                        byte[] data = Encoding.UTF8.GetBytes(text);
                        client.Send(data, data.Length, remoteEndPoint);
                    }
                }
                while (text.Length != 0);
            }
            catch (Exception err)
            {
                //writes the error to the console
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
            Console.WriteLine("\nCredits to Maarten Jakobs & Max van den Boom & Ryan van den Broek");
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
            else if (userName == "simhub" || userName == "s")
            {
                System.Diagnostics.Process.Start(@"C:\Users\steam\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
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
                else if (userName == "simhub" || userName == "s")
                {
                    System.Diagnostics.Process.Start(@"C:\Users\SimHub\Desktop\Proftaak\DiRTTelemetryErrorFix_Release\DiRTTelemetryErrorFix.exe");
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
            #region initiallize connection with arduino
            SerialPort serialPortArduinoConnection = new SerialPort();
            try
            {
                //checks if the comport is open
                serialPortArduinoConnection.PortName = comPort;
                serialPortArduinoConnection.Open();
                comPortOpen = true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Comport is not avalible");
            }
            #endregion

            #region initiallize second connection with arduino
            SerialPort serialPortArduinoConnection2 = new SerialPort();
            try
            {
                //checks if the comport is open
                serialPortArduinoConnection2.PortName = comPort2;
                serialPortArduinoConnection2.Open();
                comPort2Open = true;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Comport 2 is not avalible");
            }
            #endregion

            //used in sending data to arduino
            string breakingChar = "";
            string currentGear = "";

            UdpClient client = new UdpClient(20777);
            //this loop will make sure it continuously takes data from the game and also continuously sends data to arduino
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Starting debug information");
            Console.ResetColor();
            while (true)
            {
                try
                {
                    //makes a connection with a local server to recive data from the game
                    IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 20777);
                    byte[] dataGame = client.Receive(ref anyIP);

                    #region get data out of game
                    //this gets the round data out of the game and puts it in a variable
                    #region Round
                    float round = BitConverter.ToSingle(dataGame, 144) + 1;
                    string roundString = Convert.ToString(round);
                    #endregion

                    //this gets the gear data out of the game and puts it in a variable
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

                    //this gets the speed data out of the game and puts it in a variable
                    #region Speed
                    float speed = BitConverter.ToSingle(dataGame, 28);
                    string speedString = ((int)Math.Round(speed * 3.6, 0)).ToString("000");
                    #endregion

                    //this gets the RPM data out of the game and puts it in a variable
                    #region RPM
                    float RPM = BitConverter.ToSingle(dataGame, 148);
                    string RPMString = ((int)(Math.Round(RPM * 10))).ToString("0000");
                    #endregion

                    //this gets the brakes data out of the game and puts it in a variable
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

                    //this gets the total time data out of the game and puts it in a variable
                    #region Total Time
                    float totalTime = BitConverter.ToSingle(dataGame, 0);
                    TimeSpan resultTotalTime = TimeSpan.FromSeconds(totalTime - 3);
                    string totalTimeString = resultTotalTime.ToString("mm':'ss");
                    #endregion

                    //this gets the lap time data out of the game and puts it in a variable
                    #region Lap Time
                    float lapTime = BitConverter.ToSingle(dataGame, 4);
                    TimeSpan resultLapTime = TimeSpan.FromSeconds(lapTime);
                    string lapTimeString = resultLapTime.ToString("mm':'ss");
                    #endregion
                    #endregion

                    //writes the converted data you get from the game in the console
                    Console.WriteLine(" Lap Time: " + lapTimeString +
                                      " \n Lap: " + round +
                                      " \n Total Time: " + totalTimeString +
                                      " \n Speed: " + (Math.Round(speed * 3.6, 0)) +
                                      " KPH \n RMP: " + Math.Round(RPM * 10, 0) +
                                      "\n Gear: " + currentGear +
                                      " \n Braking: " + braking + "\n");


                    //if you have decided to enter startup and the comport is open it will start sending data to arduino
                    //turns the string values in seperated numbers or letters
                    var speedArray = speedString.ToCharArray();
                    var RPMArray = RPMString.ToCharArray();
                    var timeArray = totalTimeString.ToCharArray();
                    var roundTimeArray = lapTimeString.ToCharArray();

                    //this is the array that will be filled and send to the arduino
                    char[] dataToSend = new char[12];
                    char[] dataToSend2 = new char[12];

                    //will only happend if the comports is availible
                    if (comPortOpen == true)
                    {
                        #region data to send to arduino
                        //this will make sure it starts reading from this line of code and that static will not be received
                        #region garbage filter
                        dataToSend[0] = Convert.ToChar("a");
                        dataToSend[1] = Convert.ToChar("b");
                        #endregion

                        //this will send the data from the gear to arduino in asqii
                        #region Gear
                        if (currentGear != "")
                        {
                            dataToSend[2] = Convert.ToChar(currentGear);
                        }
                        #endregion

                        //this will send the data from the RPM to arduino in asqii
                        #region RPM
                        dataToSend[3] = RPMArray[0];
                        dataToSend[4] = RPMArray[1];
                        dataToSend[5] = RPMArray[2];
                        #endregion

                        ////this will send the data from the roundtime to arduino in asqii
                        #region Round Time
                        if (roundTimeArray.Count() > 1)
                        {
                            dataToSend[7] = roundTimeArray[0];
                            dataToSend[8] = roundTimeArray[1];
                            dataToSend[9] = roundTimeArray[3];
                            dataToSend[10] = roundTimeArray[4];
                        }
                        #endregion

                        //this will send the data from the round to arduino in asqii
                        #region Round
                        dataToSend[11] = Convert.ToChar(roundString);
                        #endregion

                        //this will send all the data in the array and arduino receives it as a asqii number
                        serialPortArduinoConnection.Write(dataToSend, 0, dataToSend.Length);
                        #endregion
                    }

                    if (comPort2Open == true)
                    {
                        #region data to send to second arduino
                        //this will make sure it starts reading from this line of code and that static will not be received
                        #region garbage filter
                        dataToSend2[0] = Convert.ToChar("a");
                        dataToSend2[1] = Convert.ToChar("b");
                        #endregion

                        ////this will send the data from the totaltime to arduino in asqii
                        #region Total Time
                        if (timeArray.Count() > 1)
                        {
                            dataToSend2[2] = timeArray[0];
                            dataToSend2[3] = timeArray[1];
                            dataToSend2[4] = timeArray[3];
                            dataToSend2[5] = timeArray[4];
                        }
                        #endregion

                        //this will send the data from the Speed to arduino in asqii
                        #region Speed
                        dataToSend2[6] = speedArray[0];
                        dataToSend2[7] = speedArray[1];
                        dataToSend2[8] = speedArray[2];
                        #endregion

                        //this will send the data from the brakes to arduino in asqii
                        #region Brakes
                        dataToSend2[9] = Convert.ToChar(breakingChar);
                        #endregion

                        Console.WriteLine(dataToSend2[9]);

                        //this will send all the data in the array and arduino receives it as a asqii number
                        serialPortArduinoConnection2.Write(dataToSend2, 0, dataToSend2.Length);
                        #endregion
                    }
                }
                catch (Exception err)
                {
                    //writes the error you get to the console
                    Console.WriteLine(err.ToString());
                }
            }
        }
    }
}
