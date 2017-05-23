namespace ConsoleSimHub
{
    partial class Form2
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
            this.btnClear = new System.Windows.Forms.Button();
            this.rtbSerialMonitor = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(769, 368);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            // 
            // rtbSerialMonitor
            // 
            this.rtbSerialMonitor.BackColor = System.Drawing.Color.Black;
            this.rtbSerialMonitor.ForeColor = System.Drawing.Color.White;
            this.rtbSerialMonitor.Location = new System.Drawing.Point(174, 105);
            this.rtbSerialMonitor.Margin = new System.Windows.Forms.Padding(4);
            this.rtbSerialMonitor.Name = "rtbSerialMonitor";
            this.rtbSerialMonitor.Size = new System.Drawing.Size(693, 255);
            this.rtbSerialMonitor.TabIndex = 2;
            this.rtbSerialMonitor.Text = "";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 501);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.rtbSerialMonitor);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox rtbSerialMonitor;
    }
}