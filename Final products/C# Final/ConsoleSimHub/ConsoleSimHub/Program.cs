﻿using System;
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
            if (userName == "ryan" || userName == "maarten" || userName == "simhub" || userName == "max")
            {
                //welcomes the user logged in
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
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nCredits to Maarten Jakobs, Max van den Boom & Ryan van den Broek");
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
            else if (userName == "max")
            {
                System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Steam\steamapps\common\DiRT 3 Complete Edition\dirt3_game.exe");
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
                else if (userName == "max")
                {
                    System.Diagnostics.Process.Start(@"C:\Users\max\Desktop\Proftaak\git\Proftaak-Simhub\DiRTTelemetryErrorFix_Release\DiRTTelemetryErrorFix.exe");
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
                serialPortArduinoConnection.WriteTimeout = 1;
                serialPortArduinoConnection.Open();
                comPortOpen = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(comPort + " is not available\n");
            }
            #endregion

            #region initiallize second connection with arduino
            SerialPort serialPortArduinoConnection2 = new SerialPort();
            try
            {
                //checks if the comport is open
                serialPortArduinoConnection2.PortName = comPort2;
                serialPortArduinoConnection2.WriteTimeout = 1;
                serialPortArduinoConnection2.Open();
                comPort2Open = true;
            }
            catch (Exception err)
            {
                //writes the error you get to the console
                Console.WriteLine(err.ToString());
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(comPort2 + " is not available\n");
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

                    string roundString;
                    string speedString;
                    string RPMString;
                    bool braking;
                    string totalTimeString;
                    string lapTimeString;

                    #region gets the data from the game
                    //this gets the round data out of the game and puts it in a variable
                    float round = BitConverter.ToSingle(dataGame, 144) + 1;
                    roundString = Convert.ToString(round);

                    //this gets the gear data out of the game and puts it in a variable
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

                    //this gets the speed data out of the game and puts it in a variable
                    float speed = BitConverter.ToSingle(dataGame, 28);
                    speedString = ((int)Math.Round(speed * 3.6, 0)).ToString("000");

                    //this gets the RPM data out of the game and puts it in a variable
                    float RPM = BitConverter.ToSingle(dataGame, 148);
                    RPMString = ((int)(Math.Round(RPM * 10))).ToString("0000");

                    //this gets the brakes data out of the game and puts it in a variable
                    braking = false;
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

                    //this gets the total time data out of the game and puts it in a variable
                    float totalTime = BitConverter.ToSingle(dataGame, 0);
                    TimeSpan resultTotalTime = TimeSpan.FromSeconds(totalTime - 3);
                    totalTimeString = resultTotalTime.ToString("mm':'ss");

                    //this gets the lap time data out of the game and puts it in a variable
                    float lapTime = BitConverter.ToSingle(dataGame, 4);
                    TimeSpan resultLapTime = TimeSpan.FromSeconds(lapTime);
                    lapTimeString = resultLapTime.ToString("mm':'ss");
                    #endregion

                    //writes the data you get from the game in the console
                    Console.WriteLine(" Lap Time: " + lapTimeString +
                                      " \n Lap: " + roundString +
                                      " \n Total Time: " + totalTimeString +
                                      " \n Speed: " + speedString +
                                      " KPH \n RMP: " + RPMString +
                                      "\n Gear: " + currentGear +
                                      " \n Braking: " + braking + "\n");


                    //turns the string values in seperated numbers or letters
                    var speedArray = speedString.ToCharArray();
                    var RPMArray = RPMString.ToCharArray();
                    var timeArray = totalTimeString.ToCharArray();
                    var roundTimeArray = lapTimeString.ToCharArray();

                    //these are the array that will be filled end send to arduino
                    char[] dataToSend = new char[12];
                    char[] dataToSend2 = new char[12];

                    //only happens when we checked if the first comport is open
                    if (comPortOpen == true)
                    {
                        #region data to send to arduino
                        //this will make sure it starts reading from this line of code and that static will not be received
                        dataToSend[0] = Convert.ToChar("a");
                        dataToSend[1] = Convert.ToChar("b");

                        //this will send the data from the gear to arduino in asqii
                        dataToSend[2] = Convert.ToChar(currentGear);

                        //this will send the data from the RPM to arduino in asqii
                        dataToSend[3] = RPMArray[0];
                        dataToSend[4] = RPMArray[1];
                        dataToSend[5] = RPMArray[2];

                        ////this will send the data from the roundtime to arduino in asqii
                        dataToSend[7] = roundTimeArray[0];
                        dataToSend[8] = roundTimeArray[1];
                        dataToSend[9] = roundTimeArray[3];
                        dataToSend[10] = roundTimeArray[4];

                        //this will send the data from the round to arduino in asqiir
                        dataToSend[11] = Convert.ToChar(roundString);

                        //this will send all the data in the array and arduino receives it as a asqii number
                        serialPortArduinoConnection.Write(dataToSend, 0, dataToSend.Length);
                        #endregion
                    }

                    //only happens when whe checked if the second comport is open
                    if (comPort2Open == true)
                    {
                        #region data to send to second arduino
                        //this will make sure it starts reading from this line of code and that static will not be received
                        dataToSend2[0] = Convert.ToChar("a");
                        dataToSend2[1] = Convert.ToChar("b");

                        ////this will send the data from the totaltime to arduino in asqii
                        dataToSend2[2] = timeArray[0];
                        dataToSend2[3] = timeArray[1];
                        dataToSend2[4] = timeArray[3];
                        dataToSend2[5] = timeArray[4];

                        //this will send the data from the Speed to arduino in asqii
                        dataToSend2[6] = speedArray[0];
                        dataToSend2[7] = speedArray[1];
                        dataToSend2[8] = speedArray[2];

                        //this will send the data from the brakes to arduino in asqii
                        dataToSend2[9] = Convert.ToChar(breakingChar);


                        //this will send all the data in the array and arduino receives it as a asqii number
                        serialPortArduinoConnection2.Write(dataToSend2, 0, dataToSend2.Length);
                        #endregion
                    }

                }
                catch (Exception err)
                {
                    //writes the error you get to the consol
                    //Console.WriteLine(err.ToString());
                }

            }
        }
    }
}
