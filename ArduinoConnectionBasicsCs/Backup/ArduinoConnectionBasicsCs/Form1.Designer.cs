namespace ArduinoConnectionBasicsCs
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmArduinoUpdate = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cbbSerialPortsDkal = new System.Windows.Forms.ComboBox();
            this.btnScanPortsDkal = new System.Windows.Forms.Button();
            this.cbbBaudRateDkal = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSerialPortOpenDkal = new System.Windows.Forms.Button();
            this.btnReadArduinoDkal = new System.Windows.Forms.Button();
            this.rtbArduinoDataDkal = new System.Windows.Forms.RichTextBox();
            this.pnlReadIndicatorDkal = new System.Windows.Forms.Panel();
            this.btnWriteArduinoDkal = new System.Windows.Forms.Button();
            this.tbLastCharDkal = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tmArduinoUpdate
            // 
            this.tmArduinoUpdate.Tick += new System.EventHandler(this.tmArduinoUpdate_Tick);
            // 
            // cbbSerialPortsDkal
            // 
            this.cbbSerialPortsDkal.FormattingEnabled = true;
            this.cbbSerialPortsDkal.Location = new System.Drawing.Point(141, 12);
            this.cbbSerialPortsDkal.Name = "cbbSerialPortsDkal";
            this.cbbSerialPortsDkal.Size = new System.Drawing.Size(89, 21);
            this.cbbSerialPortsDkal.TabIndex = 2;
            this.cbbSerialPortsDkal.SelectedIndexChanged += new System.EventHandler(this.cbbSerialPortsDkal_SelectedIndexChanged);
            // 
            // btnScanPortsDkal
            // 
            this.btnScanPortsDkal.Location = new System.Drawing.Point(12, 10);
            this.btnScanPortsDkal.Name = "btnScanPortsDkal";
            this.btnScanPortsDkal.Size = new System.Drawing.Size(75, 23);
            this.btnScanPortsDkal.TabIndex = 3;
            this.btnScanPortsDkal.Text = "Scan ports";
            this.btnScanPortsDkal.UseVisualStyleBackColor = true;
            this.btnScanPortsDkal.Click += new System.EventHandler(this.btnScanPortsDkal_Click);
            // 
            // cbbBaudRateDkal
            // 
            this.cbbBaudRateDkal.FormattingEnabled = true;
            this.cbbBaudRateDkal.Items.AddRange(new object[] {
            "9600",
            "38400"});
            this.cbbBaudRateDkal.Location = new System.Drawing.Point(141, 36);
            this.cbbBaudRateDkal.Name = "cbbBaudRateDkal";
            this.cbbBaudRateDkal.Size = new System.Drawing.Size(89, 21);
            this.cbbBaudRateDkal.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "select Arduino Baudrate:";
            // 
            // btnSerialPortOpenDkal
            // 
            this.btnSerialPortOpenDkal.Location = new System.Drawing.Point(236, 12);
            this.btnSerialPortOpenDkal.Name = "btnSerialPortOpenDkal";
            this.btnSerialPortOpenDkal.Size = new System.Drawing.Size(75, 23);
            this.btnSerialPortOpenDkal.TabIndex = 6;
            this.btnSerialPortOpenDkal.Text = "Open port";
            this.btnSerialPortOpenDkal.UseVisualStyleBackColor = true;
            this.btnSerialPortOpenDkal.Click += new System.EventHandler(this.btnSerialPortOpenDkal_Click);
            // 
            // btnReadArduinoDkal
            // 
            this.btnReadArduinoDkal.Location = new System.Drawing.Point(12, 90);
            this.btnReadArduinoDkal.Name = "btnReadArduinoDkal";
            this.btnReadArduinoDkal.Size = new System.Drawing.Size(75, 23);
            this.btnReadArduinoDkal.TabIndex = 7;
            this.btnReadArduinoDkal.Text = "Read";
            this.btnReadArduinoDkal.UseVisualStyleBackColor = true;
            this.btnReadArduinoDkal.Click += new System.EventHandler(this.btnReadArduinoDkal_Click);
            // 
            // rtbArduinoDataDkal
            // 
            this.rtbArduinoDataDkal.Location = new System.Drawing.Point(12, 119);
            this.rtbArduinoDataDkal.Name = "rtbArduinoDataDkal";
            this.rtbArduinoDataDkal.Size = new System.Drawing.Size(483, 130);
            this.rtbArduinoDataDkal.TabIndex = 8;
            this.rtbArduinoDataDkal.Text = "";
            this.rtbArduinoDataDkal.WordWrap = false;
            // 
            // pnlReadIndicatorDkal
            // 
            this.pnlReadIndicatorDkal.Location = new System.Drawing.Point(93, 90);
            this.pnlReadIndicatorDkal.Name = "pnlReadIndicatorDkal";
            this.pnlReadIndicatorDkal.Size = new System.Drawing.Size(10, 22);
            this.pnlReadIndicatorDkal.TabIndex = 9;
            // 
            // btnWriteArduinoDkal
            // 
            this.btnWriteArduinoDkal.Location = new System.Drawing.Point(394, 18);
            this.btnWriteArduinoDkal.Name = "btnWriteArduinoDkal";
            this.btnWriteArduinoDkal.Size = new System.Drawing.Size(75, 23);
            this.btnWriteArduinoDkal.TabIndex = 10;
            this.btnWriteArduinoDkal.Text = "Write";
            this.btnWriteArduinoDkal.UseVisualStyleBackColor = true;
            this.btnWriteArduinoDkal.Click += new System.EventHandler(this.btnWriteArduinoDkal_Click);
            // 
            // tbLastCharDkal
            // 
            this.tbLastCharDkal.Location = new System.Drawing.Point(395, 47);
            this.tbLastCharDkal.Name = "tbLastCharDkal";
            this.tbLastCharDkal.Size = new System.Drawing.Size(74, 20);
            this.tbLastCharDkal.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 261);
            this.Controls.Add(this.tbLastCharDkal);
            this.Controls.Add(this.btnWriteArduinoDkal);
            this.Controls.Add(this.pnlReadIndicatorDkal);
            this.Controls.Add(this.rtbArduinoDataDkal);
            this.Controls.Add(this.btnReadArduinoDkal);
            this.Controls.Add(this.btnSerialPortOpenDkal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbBaudRateDkal);
            this.Controls.Add(this.btnScanPortsDkal);
            this.Controls.Add(this.cbbSerialPortsDkal);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmArduinoUpdate;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cbbSerialPortsDkal;
        private System.Windows.Forms.Button btnScanPortsDkal;
        private System.Windows.Forms.ComboBox cbbBaudRateDkal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSerialPortOpenDkal;
        private System.Windows.Forms.Button btnReadArduinoDkal;
        private System.Windows.Forms.RichTextBox rtbArduinoDataDkal;
        private System.Windows.Forms.Panel pnlReadIndicatorDkal;
        private System.Windows.Forms.Button btnWriteArduinoDkal;
        private System.Windows.Forms.TextBox tbLastCharDkal;
    }
}

