using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//program dependencies
using System.Diagnostics;
using System.IO.Ports;
namespace ConsoleSimHub
{
    public partial class Form1 : Form
    {
        #region declarations
        Form2 SerialMonitor = new Form2();

        double packetOkCount = 0;
        double packetNokCount = 0;
        double percentageGoodPackets = 0;
        bool compPortsAvailable = false;
        #endregion

        #region initialization
        public Form1()
        {
            InitializeComponent();

            cbbAutoUpdate.SelectedIndex = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tbcD2cDkal.Appearance = TabAppearance.FlatButtons;
            tbcD2cDkal.ItemSize = new Size(0, 1);
            tbcD2cDkal.SizeMode = TabSizeMode.Fixed;

            tbcD2cDkal.SelectedTab = tbpConnectAndControlDkal;
        }
        #endregion

        #region COM ports
        private void btnScanPortsDkal_Click(object sender, EventArgs e)
        {
            ScanComPortsDkal();
        }

        private void ScanComPortsDkal()
        {
            String[] ports = SerialPort.GetPortNames();
            Array.Sort(ports);
            string m_portWithOutLastCharacter;

            compPortsAvailable = true;

            cbbSerialPortsDkal.Items.Clear();
            foreach (String port in ports)
            {
                if (cbSelectIfDangerShieldIsUsed.Checked == true)
                {
                    m_portWithOutLastCharacter = port.Substring(0, port.Length - 1);
                }
                else
                {
                    m_portWithOutLastCharacter = port;
                }

                cbbSerialPortsDkal.Items.Add(m_portWithOutLastCharacter);
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

        private void btnSerialPortOpenDkal_Click(object sender, EventArgs e)
        {
            if (!serialPortArduinoConnection.IsOpen)
            {
                try
                {
                    serialPortArduinoConnection.Open();
                }
                catch
                {
                    MessageBox.Show("ERROR. Please make sure that the correct port was selected, and the device, plugged in.", "Serial port", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbbSerialPortsDkal_SelectedIndexChanged(object sender, EventArgs e)
        {
            serialPortArduinoConnection.PortName = cbbSerialPortsDkal.Text;

            btnSerialPortOpenDkal.Enabled = true;
        }
        #endregion

        #region miscelleneous
        private void btnArduinoResetDkal_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reset can be controlled on the Arduino Dangershield board only.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region menu handling
        private void minViewDebugLogDkal_Click(object sender, EventArgs e)
        {
            mniViewConnectAndControlDkal.Checked = !mniViewConnectAndControlDkal.Checked;

            if (mniViewConnectAndControlDkal.Checked == true)
            {
                SerialMonitor.Show();
            }
            else
            {
                SerialMonitor.Hide();
            }
        }

        private void mniViewConnectAndControlDkal_Click(object sender, EventArgs e)
        {
            tbcD2cDkal.SelectedTab = tbpConnectAndControlDkal;
        }

        private void mniViewDangershieldDkal_Click(object sender, EventArgs e)
        {
            tbcD2cDkal.SelectedTab = tbpDangershieldDkal;
        }

        private void mniViewRfidDkal_Click(object sender, EventArgs e)
        {
            tbcD2cDkal.SelectedTab = tbpRfidDkal;
        }

        private void mniD2cAboutDkal_Click(object sender, EventArgs e)
        {
            string m_about;

            m_about = "Project: Dangershield and RFID control on Arduino UNO\n\n" +
                      "Created by\t: Max van den Boom\n" +
                      "Date\t\t: 22 mei 2017\n" +
                      "School\t\t: ROC ter AA\n";

            MessageBox.Show(m_about, "About..");
        }

        private void mniD2cTechInfoDangershieldDkal_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\..\\..\\doc\\Dangershield circuit diagram.jpg");
        }

        private void mniD2cTechInfoRfidDkal_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\..\\..\\doc\\ID-2LA, ID-12LA, ID-20LA2013-4-10 datasheet.pdf");
        }

        private void mniD2cTechInfoAsciiDkal_Click(object sender, EventArgs e)
        {
            Process.Start(Application.StartupPath + "\\..\\..\\doc\\asciifull.gif");
        }

        private void mniD2cQuitDkal_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do you really want to quit?", "Quitting application", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                Application.Exit();
            }
        }
        #endregion

        #region read Arduino
        private void ReadArduino()
        {
            string m_data;
            int m_potMeterValue0;
            int m_potMeterValue1;
            int m_potMeterValue2;

            int m_potMeterStringLength0;
            int m_potMeterStringLength1;
            int m_potMeterStringLength2;
            int m_potMeterStringStartIndex0;
            int m_potMeterStringStartIndex1;
            int m_potMeterStringStartIndex2;

            try
            {
                m_data = serialPortArduinoConnection.ReadLine();

                if (m_data.IndexOf("#") == 0 && m_data.IndexOf("%") > 0)
                {
                    m_data = m_data.Substring(1, m_data.Length - 3);

                    serialPortArduinoConnection.DiscardInBuffer();

                    pnlReadQualityIndicatorDkal.BackColor = Color.Lime;

                    packetOkCount++;

                    SerialMonitor.PrintLn(m_data, "W");

                    m_potMeterStringStartIndex0 = m_data.IndexOf("A0:") + 3;
                    m_potMeterStringStartIndex1 = m_data.IndexOf("A1:") + 3;
                    m_potMeterStringStartIndex2 = m_data.IndexOf("A2:") + 3;
                    m_potMeterStringLength0 = (m_data.IndexOf(" > A1:") - m_data.IndexOf("A0:")) - 3;
                    m_potMeterStringLength1 = (m_data.IndexOf(" > A2:") - m_data.IndexOf("A1:")) - 3;
                    m_potMeterStringLength2 = (m_data.IndexOf(" > A3:") - m_data.IndexOf("A2:")) - 3;

                    m_potMeterValue0 = Convert.ToInt32(m_data.Substring(m_potMeterStringStartIndex0, m_potMeterStringLength0));
                    trbA2.Value = (1024 - m_potMeterValue0);
                    SerialMonitor.PrintLn(m_potMeterValue0.ToString(), "W");

                    m_potMeterValue1 = Convert.ToInt32(m_data.Substring(m_potMeterStringStartIndex1, m_potMeterStringLength1));
                    trbA1.Value = (1024 - m_potMeterValue1);
                    SerialMonitor.PrintLn(m_potMeterValue1.ToString(), "W");

                    m_potMeterValue2 = Convert.ToInt32(m_data.Substring(m_potMeterStringStartIndex2, m_potMeterStringLength2));
                    trbA0.Value = (1024 - m_potMeterValue2);
                    SerialMonitor.PrintLn(m_potMeterValue2.ToString(), "W");


                }
                else
                {
                    pnlReadQualityIndicatorDkal.BackColor = Color.Red;

                    packetNokCount++;

                    if (cbShowErrors.Checked)
                    {
                        m_data = "ERROR: packet " + packetNokCount.ToString() + " missed";

                        SerialMonitor.PrintLn(m_data, "R");
                    }
                }

                percentageGoodPackets = Math.Round(((packetOkCount / (packetOkCount + packetNokCount)) * 100), 0);
                lblErrorRate.Text = Convert.ToString(percentageGoodPackets) + "%";
            }
            catch
            {
                MessageBox.Show("COM port not selected.", "Error");
            }
        }

        private void btnArduinoReadDkal_Click(object sender, EventArgs e)
        {
            ReadArduino();
        }

        private void tmArduinoReadUpdate_Tick(object sender, EventArgs e)
        {
            ReadArduino();
        }


        private void cbAutoUpdateDkal_CheckedChanged(object sender, EventArgs e)
        {
            if (compPortsAvailable == true)
            {
                tmArduinoReadUpdate.Enabled = cbAutoUpdateDkal.Checked;
            }
            else
            {
                MessageBox.Show("COM port not selected.", "Error");
            }
        }

        private void cbbAutoUpdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            tmArduinoReadUpdate.Interval = Convert.ToInt32(cbbAutoUpdate.Text);
        }
        #endregion

        #region write Arduino
        /// <summary>
        /// <para>Arduino receives ASCII characters and interprets as integers</para>
        /// <para>check ASCII tabel for values</para>
        /// </summary>
        /// <param name="a_action"></param>
        private void WriteArduino(string a_action)
        {
            char[] m_data = new char[3];

            m_data[0] = 'A';
            m_data[1] = 'B';
            m_data[2] = Convert.ToChar(a_action);

            serialPortArduinoConnection.Write(m_data, 0, 3);
        }

        private void btnArduinoD10Dkal_Click(object sender, EventArgs e)
        {
            WriteArduino("A");
        }

        private void btnArduinoD11Dkal_Click(object sender, EventArgs e)
        {
            WriteArduino("C");
        }

        private void btnArduinoD12Dkal_Click(object sender, EventArgs e)
        {
            WriteArduino("D");
        }

        private void btnArduinoWriteDkal_Click(object sender, EventArgs e)
        {

        }

        private void btnArduinoBuzzerDxxDkal_Click(object sender, EventArgs e)
        {
            WriteArduino("E");
        }
        #endregion
    }
}
