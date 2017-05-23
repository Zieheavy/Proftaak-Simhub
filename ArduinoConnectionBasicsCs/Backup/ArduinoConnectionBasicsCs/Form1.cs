using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//program dependencies
using System.Diagnostics;
using System.IO.Ports;

namespace ArduinoConnectionBasicsCs
{
    public partial class Form1 : Form
    {
        #region steps
        //add SerialPort form item
        //add comboboxes for Comport selection and baudrate, the baudrate in the Arduino can vary
        //check buffer size of the SerialPort object (default 4096)
        //
        #endregion

        #region Arduino
        //Arduino code in CompassCMPS10AndSerialMonitorV2

//        /****************************************************************
// *                  Arduino CMPS10 example code                  *
// *          CMPS10 running I2C mode with serial monitor          *
// *     by Dick van Kalsbeek, ROC ter AA, Helmond, 20feb2015      *
// *                thanks to: James Henderson                     *
// *****************************************************************/
//#include <Wire.h>

//#define ADDRESS 0x60                                          // Defines address of CMPS10
//String stringToCheck = "";
//int doRxAck = 7;

//void setup()
//{
//  Wire.begin();  

//  pinMode(doRxAck, OUTPUT);

//  digitalWrite(doRxAck, HIGH);
//  delay(500);
//  digitalWrite(doRxAck, LOW);

//  Serial.begin(9600);
//  Serial.println("Start up finished..");
//}

//void loop()
//{
//  byte highByte, lowByte, fine;              // highByte and lowByte store high and low bytes of the bearing and fine stores decimal place of bearing
//  byte pitch, roll;                          // Stores pitch and roll values of CMPS10, chars are used because they support signed value
//  int bearing;                               // Stores full bearing

//    int m_receivedValue;
//  char m_data[3];

//  //Serial.println("Start communication with CMPS10..");
//  Wire.beginTransmission(ADDRESS);           //starts communication with CMPS10
//  Wire.write(2);                              //Sends the register we wish to start reading from
//  Wire.endTransmission();

//  //Serial.println("Get data from CMPS10..");
//  Wire.requestFrom(ADDRESS, 4);              // Request 4 bytes from CMPS10
//  while(Wire.available() < 4);               // Wait for bytes to become available

//  highByte = Wire.read();           
//  lowByte = Wire.read();            
//  pitch = (byte)Wire.read();              
//  roll = (byte)Wire.read();               

//  bearing = ((highByte<<8)+lowByte)/10;      // Calculate full bearing
//  fine = ((highByte<<8)+lowByte)%10;         // Calculate decimal place of bearing

//  if(Serial.available() > 0)
//  {
//    //    m_receivedValue = Serial.read();
//    //    
//    //    if(m_receivedValue == 55)
//    //    {
//    //      digitalWrite(doRxAck, HIGH);
//    //      delay(500);      
//    //    }
//    //    digitalWrite(doRxAck, LOW);

//    Serial.readBytes(m_data, 3);

//    if(m_data[0] == 65 && m_data[1] == 66)
//    {
//      switch(m_data[2])
//      {
//      case 67: 
//        digitalWrite(doRxAck, HIGH);
//        delay(500); 
//        break;     
//      case 68: 
//        digitalWrite(doRxAck, HIGH);
//        delay(1000); 
//        break;
//      default: 
//        digitalWrite(doRxAck, HIGH);
//        delay(100); 
//        break;
//      }
//    }

//    digitalWrite(doRxAck, LOW);
//  }

//  // Serial.println("#received value: " + (String)m_receivedValue + (String)"%");
//  Serial.println("#received value: " + (String)m_data[0] + " > "  + (String)m_data[1] + " > " + (String)m_data[2] + " > " + (String)"%");

//  Serial.println((String)"#" + "bearing: " + (String)bearing + "\t\t" + "fine: " + (String)fine + "\t\t" + 
//    "pitch: " + (String)pitch + "\t\t" + "roll: " + (String)roll +
//    (String)"%"); 

//  Serial.flush();



//  delay(100);
//}
        #endregion

        public Form1()
        {
            InitializeComponent();
        }

        private void tmArduinoUpdate_Tick(object sender, EventArgs e)
        {

        }

        private void btnScanPortsDkal_Click(object sender, EventArgs e)
        {
            ScanComPortsDkal();
        }

        private void ScanComPortsDkal()
        {
            String[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);

            cbbSerialPortsDkal.Items.Clear();
            foreach (String port in ports)
            {
                cbbSerialPortsDkal.Items.Add(port);
            }

            if (cbbBaudRateDkal.Items.Count > 0)
            {
                cbbSerialPortsDkal.Text = "Select!";
                cbbSerialPortsDkal.Enabled = true;
            }
            else
            {
                cbbSerialPortsDkal.Text = "N.A.";
                cbbSerialPortsDkal.Enabled = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Upload de Arduino code uit de Arduino region in Form1.cs\n" +
                            "Kies in deze applicatie de juiste comport en zorg voor de juiste baudrate.");

            ScanComPortsDkal();

            cbbBaudRateDkal.SelectedIndex = 0;

            btnSerialPortOpenDkal.Enabled = false;
        }

        private void btnSerialPortOpenDkal_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.Open();
                    //serialPort1.Write("T");
                    //serialPort1.Close();
                }
                catch
                {
                    MessageBox.Show("There was an error. Please make sure that the correct port was selected, and the device, plugged in.");
                }
            }
        }

        private void btnReadArduinoDkal_Click(object sender, EventArgs e)
        {
            string m_data;

            //m_data = serialPort1.ReadLine();
            m_data = serialPort1.ReadLine();

            if (m_data.IndexOf("#") == 0 && m_data.IndexOf("%") > 0)
            {
                m_data = m_data.Substring(1, m_data.Length - 3) + "\n";

                serialPort1.DiscardInBuffer();

                rtbArduinoDataDkal.AppendText(m_data);
                rtbArduinoDataDkal.ScrollToCaret();

                pnlReadIndicatorDkal.BackColor = Color.Lime;
            }
            else
            {
                pnlReadIndicatorDkal.BackColor = Color.Red;
            }
        }

        private void cbbSerialPortsDkal_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPort1.PortName = cbbSerialPortsDkal.Text;

            btnSerialPortOpenDkal.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }

        private void btnWriteArduinoDkal_Click(object sender, EventArgs e)
        {
            char[] m_data = new char[3];

            m_data[0] = 'A';
            m_data[1] = 'B';
            m_data[2] = Convert.ToChar(tbLastCharDkal.Text);

            //serialPort1.Write("7");
            serialPort1.Write(m_data, 0, 3);
        }
    }
}
