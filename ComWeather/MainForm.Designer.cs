using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace ComWeather
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.txtPortName = new System.Windows.Forms.TextBox();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.lblPortName = new System.Windows.Forms.Label();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtParsedData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            this.txtFileData = new System.Windows.Forms.TextBox();
            this.txtJsonData = new System.Windows.Forms.TextBox();
            // 
            // txtPortName
            // 
            this.txtPortName.Location = new System.Drawing.Point(12, 29);
            this.txtPortName.Name = "txtPortName";
            this.txtPortName.Size = new System.Drawing.Size(100, 22);
            this.txtPortName.TabIndex = 0;
            this.txtPortName.Text = "COM3";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Location = new System.Drawing.Point(118, 29);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(100, 22);
            this.txtBaudRate.TabIndex = 1;
            this.txtBaudRate.Text = "2400";
            // 
            // lblPortName
            // 
            this.lblPortName.AutoSize = true;
            this.lblPortName.Location = new System.Drawing.Point(12, 9);
            this.lblPortName.Name = "lblPortName";
            this.lblPortName.Size = new System.Drawing.Size(78, 17);
            this.lblPortName.TabIndex = 2;
            this.lblPortName.Text = "Имя порта:";
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(115, 9);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(137, 17);
            this.lblBaudRate.TabIndex = 3;
            this.lblBaudRate.Text = "Скорость (бод/сек):";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(224, 28);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(120, 23);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Enabled = false; // Кнопка изначально неактивна
            this.btnDisconnect.Location = new System.Drawing.Point(350, 28);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(120, 23);
            this.btnDisconnect.TabIndex = 5;
            this.btnDisconnect.Text = "Отключиться";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 69);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(776, 150);
            this.txtLog.TabIndex = 6;
            // 
            // txtParsedData
            // 
            this.txtParsedData.Location = new System.Drawing.Point(12, 225);
            this.txtParsedData.Multiline = true;
            this.txtParsedData.Name = "txtParsedData";
            this.txtParsedData.ReadOnly = true;
            this.txtParsedData.Size = new System.Drawing.Size(776, 150);
            this.txtParsedData.TabIndex = 7;
            // Настройка txtFileData
            this.txtFileData.Location = new System.Drawing.Point(12, 300);
            this.txtFileData.Multiline = true;
            this.txtFileData.ReadOnly = true;
            this.txtFileData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtFileData.Size = new System.Drawing.Size(376, 200);
            this.txtFileData.TabIndex = 5;

            // Настройка txtJsonData
            this.txtJsonData.Location = new System.Drawing.Point(400, 300);
            this.txtJsonData.Multiline = true;
            this.txtJsonData.ReadOnly = true;
            this.txtJsonData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtJsonData.Size = new System.Drawing.Size(376, 200);
            this.txtJsonData.TabIndex = 6;

            // Добавьте их на форму
            this.Controls.Add(this.txtFileData);
            this.Controls.Add(this.txtJsonData);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtParsedData);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.lblPortName);
            this.Controls.Add(this.txtBaudRate);
            this.Controls.Add(this.txtPortName);
            this.Name = "MainForm";
            this.Text = "Weather Sensor App";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TextBox txtPortName;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.Label lblPortName;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtParsedData;
        private System.Windows.Forms.TextBox txtFileData;
        private System.Windows.Forms.TextBox txtJsonData;
    }
}
