namespace HDCLUart
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
            this.UBXcomboBox = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.TimeUtclabel = new System.Windows.Forms.Label();
            this.temperaturelabel = new System.Windows.Forms.Label();
            this.barolabel = new System.Windows.Forms.Label();
            this.maglabel = new System.Windows.Forms.Label();
            this.accellabel = new System.Windows.Forms.Label();
            this.gyrolabel = new System.Windows.Forms.Label();
            this.btn_GetStatus = new System.Windows.Forms.Button();
            this.GPSlabel = new System.Windows.Forms.Label();
            this.EEPROMReadradioButton = new System.Windows.Forms.RadioButton();
            this.EEPROMWriteradioButton = new System.Windows.Forms.RadioButton();
            this.EEPROMButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EEPROMcomboBox = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.HexButton = new System.Windows.Forms.RadioButton();
            this.DecimalButton = new System.Windows.Forms.RadioButton();
            this.EEPROMtextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PingButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox_coms = new System.Windows.Forms.ListBox();
            this.AnyUBXlabel = new System.Windows.Forms.Label();
            this.Rolllabel = new System.Windows.Forms.Label();
            this.btn_SetStatus = new System.Windows.Forms.Button();
            this.btn_Attitude_Feedback = new System.Windows.Forms.Button();
            this.btn_Position_Feedback = new System.Windows.Forms.Button();
            this.btn_TimeStamp = new System.Windows.Forms.Button();
            this.txt_MSG_replies = new System.Windows.Forms.RichTextBox();
            this.btn_data_tst_stop = new System.Windows.Forms.Button();
            this.btn_data_tst = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // UBXcomboBox
            // 
            this.UBXcomboBox.FormattingEnabled = true;
            this.UBXcomboBox.Location = new System.Drawing.Point(99, 393);
            this.UBXcomboBox.Name = "UBXcomboBox";
            this.UBXcomboBox.Size = new System.Drawing.Size(78, 21);
            this.UBXcomboBox.TabIndex = 16;
            this.UBXcomboBox.Text = "UBX";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(15, 393);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 14;
            this.button3.Text = "UBX POLL";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // TimeUtclabel
            // 
            this.TimeUtclabel.AutoSize = true;
            this.TimeUtclabel.Location = new System.Drawing.Point(229, 187);
            this.TimeUtclabel.Name = "TimeUtclabel";
            this.TimeUtclabel.Size = new System.Drawing.Size(22, 13);
            this.TimeUtclabel.TabIndex = 13;
            this.TimeUtclabel.Text = "utc";
            // 
            // temperaturelabel
            // 
            this.temperaturelabel.AutoSize = true;
            this.temperaturelabel.Location = new System.Drawing.Point(179, 362);
            this.temperaturelabel.Name = "temperaturelabel";
            this.temperaturelabel.Size = new System.Drawing.Size(63, 13);
            this.temperaturelabel.TabIndex = 12;
            this.temperaturelabel.Text = "temperature";
            // 
            // barolabel
            // 
            this.barolabel.AutoSize = true;
            this.barolabel.Location = new System.Drawing.Point(12, 362);
            this.barolabel.Name = "barolabel";
            this.barolabel.Size = new System.Drawing.Size(29, 13);
            this.barolabel.TabIndex = 11;
            this.barolabel.Text = "Baro";
            // 
            // maglabel
            // 
            this.maglabel.AutoSize = true;
            this.maglabel.Location = new System.Drawing.Point(12, 336);
            this.maglabel.Name = "maglabel";
            this.maglabel.Size = new System.Drawing.Size(27, 13);
            this.maglabel.TabIndex = 10;
            this.maglabel.Text = "mag";
            // 
            // accellabel
            // 
            this.accellabel.AutoSize = true;
            this.accellabel.Location = new System.Drawing.Point(12, 312);
            this.accellabel.Name = "accellabel";
            this.accellabel.Size = new System.Drawing.Size(34, 13);
            this.accellabel.TabIndex = 9;
            this.accellabel.Text = "Accel";
            // 
            // gyrolabel
            // 
            this.gyrolabel.AutoSize = true;
            this.gyrolabel.Location = new System.Drawing.Point(12, 288);
            this.gyrolabel.Name = "gyrolabel";
            this.gyrolabel.Size = new System.Drawing.Size(27, 13);
            this.gyrolabel.TabIndex = 8;
            this.gyrolabel.Text = "gyro";
            // 
            // btn_GetStatus
            // 
            this.btn_GetStatus.Location = new System.Drawing.Point(12, 258);
            this.btn_GetStatus.Name = "btn_GetStatus";
            this.btn_GetStatus.Size = new System.Drawing.Size(84, 23);
            this.btn_GetStatus.TabIndex = 6;
            this.btn_GetStatus.Text = "Get Status";
            this.btn_GetStatus.UseVisualStyleBackColor = true;
            this.btn_GetStatus.Click += new System.EventHandler(this.btn_GetStatus_Click);
            // 
            // GPSlabel
            // 
            this.GPSlabel.AutoSize = true;
            this.GPSlabel.Location = new System.Drawing.Point(13, 187);
            this.GPSlabel.Name = "GPSlabel";
            this.GPSlabel.Size = new System.Drawing.Size(44, 13);
            this.GPSlabel.TabIndex = 5;
            this.GPSlabel.Text = "location";
            // 
            // EEPROMReadradioButton
            // 
            this.EEPROMReadradioButton.AutoSize = true;
            this.EEPROMReadradioButton.Checked = true;
            this.EEPROMReadradioButton.Location = new System.Drawing.Point(11, 19);
            this.EEPROMReadradioButton.Name = "EEPROMReadradioButton";
            this.EEPROMReadradioButton.Size = new System.Drawing.Size(51, 17);
            this.EEPROMReadradioButton.TabIndex = 0;
            this.EEPROMReadradioButton.TabStop = true;
            this.EEPROMReadradioButton.Text = "Read";
            this.EEPROMReadradioButton.UseVisualStyleBackColor = true;
            // 
            // EEPROMWriteradioButton
            // 
            this.EEPROMWriteradioButton.AutoSize = true;
            this.EEPROMWriteradioButton.Location = new System.Drawing.Point(11, 43);
            this.EEPROMWriteradioButton.Name = "EEPROMWriteradioButton";
            this.EEPROMWriteradioButton.Size = new System.Drawing.Size(50, 17);
            this.EEPROMWriteradioButton.TabIndex = 1;
            this.EEPROMWriteradioButton.Text = "Write";
            this.EEPROMWriteradioButton.UseVisualStyleBackColor = true;
            // 
            // EEPROMButton
            // 
            this.EEPROMButton.Location = new System.Drawing.Point(354, 17);
            this.EEPROMButton.Name = "EEPROMButton";
            this.EEPROMButton.Size = new System.Drawing.Size(58, 23);
            this.EEPROMButton.TabIndex = 3;
            this.EEPROMButton.Text = "Send";
            this.EEPROMButton.UseVisualStyleBackColor = true;
            this.EEPROMButton.Click += new System.EventHandler(this.EEPROMButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Item";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Data";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Yellow;
            this.groupBox1.Controls.Add(this.EEPROMcomboBox);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.EEPROMButton);
            this.groupBox1.Controls.Add(this.EEPROMtextBox);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(16, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(418, 80);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "EEPROM";
            // 
            // EEPROMcomboBox
            // 
            this.EEPROMcomboBox.FormattingEnabled = true;
            this.EEPROMcomboBox.Location = new System.Drawing.Point(114, 14);
            this.EEPROMcomboBox.Name = "EEPROMcomboBox";
            this.EEPROMcomboBox.Size = new System.Drawing.Size(126, 21);
            this.EEPROMcomboBox.TabIndex = 17;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.HexButton);
            this.groupBox3.Controls.Add(this.DecimalButton);
            this.groupBox3.Location = new System.Drawing.Point(246, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(74, 65);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "H/D";
            // 
            // HexButton
            // 
            this.HexButton.AutoSize = true;
            this.HexButton.Checked = true;
            this.HexButton.Location = new System.Drawing.Point(7, 41);
            this.HexButton.Name = "HexButton";
            this.HexButton.Size = new System.Drawing.Size(44, 17);
            this.HexButton.TabIndex = 1;
            this.HexButton.TabStop = true;
            this.HexButton.Text = "Hex";
            this.HexButton.UseVisualStyleBackColor = true;
            this.HexButton.CheckedChanged += new System.EventHandler(this.HexButton_CheckedChanged);
            // 
            // DecimalButton
            // 
            this.DecimalButton.AutoSize = true;
            this.DecimalButton.Location = new System.Drawing.Point(7, 19);
            this.DecimalButton.Name = "DecimalButton";
            this.DecimalButton.Size = new System.Drawing.Size(63, 17);
            this.DecimalButton.TabIndex = 0;
            this.DecimalButton.Text = "Decimal";
            this.DecimalButton.UseVisualStyleBackColor = true;
            this.DecimalButton.CheckedChanged += new System.EventHandler(this.DecimalButton_CheckedChanged);
            // 
            // EEPROMtextBox
            // 
            this.EEPROMtextBox.Location = new System.Drawing.Point(114, 41);
            this.EEPROMtextBox.Name = "EEPROMtextBox";
            this.EEPROMtextBox.Size = new System.Drawing.Size(126, 20);
            this.EEPROMtextBox.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.EEPROMReadradioButton);
            this.groupBox2.Controls.Add(this.EEPROMWriteradioButton);
            this.groupBox2.Location = new System.Drawing.Point(7, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(68, 59);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "R/W";
            // 
            // PingButton
            // 
            this.PingButton.Location = new System.Drawing.Point(16, 41);
            this.PingButton.Name = "PingButton";
            this.PingButton.Size = new System.Drawing.Size(58, 23);
            this.PingButton.TabIndex = 3;
            this.PingButton.Text = "Ping";
            this.PingButton.UseVisualStyleBackColor = true;
            this.PingButton.Click += new System.EventHandler(this.PingButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // listBox_coms
            // 
            this.listBox_coms.FormattingEnabled = true;
            this.listBox_coms.Location = new System.Drawing.Point(119, 12);
            this.listBox_coms.Name = "listBox_coms";
            this.listBox_coms.Size = new System.Drawing.Size(120, 17);
            this.listBox_coms.TabIndex = 1;
            // 
            // AnyUBXlabel
            // 
            this.AnyUBXlabel.AutoSize = true;
            this.AnyUBXlabel.Location = new System.Drawing.Point(13, 209);
            this.AnyUBXlabel.Name = "AnyUBXlabel";
            this.AnyUBXlabel.Size = new System.Drawing.Size(100, 13);
            this.AnyUBXlabel.TabIndex = 17;
            this.AnyUBXlabel.Text = "Messages to OBOX";
            // 
            // Rolllabel
            // 
            this.Rolllabel.AutoSize = true;
            this.Rolllabel.Location = new System.Drawing.Point(266, 288);
            this.Rolllabel.Name = "Rolllabel";
            this.Rolllabel.Size = new System.Drawing.Size(27, 13);
            this.Rolllabel.TabIndex = 18;
            this.Rolllabel.Text = "gyro";
            // 
            // btn_SetStatus
            // 
            this.btn_SetStatus.Location = new System.Drawing.Point(12, 229);
            this.btn_SetStatus.Name = "btn_SetStatus";
            this.btn_SetStatus.Size = new System.Drawing.Size(84, 23);
            this.btn_SetStatus.TabIndex = 19;
            this.btn_SetStatus.Text = "Set Status";
            this.btn_SetStatus.UseVisualStyleBackColor = true;
            this.btn_SetStatus.Click += new System.EventHandler(this.btn_SetStatus_Click);
            // 
            // btn_Attitude_Feedback
            // 
            this.btn_Attitude_Feedback.Location = new System.Drawing.Point(130, 228);
            this.btn_Attitude_Feedback.Name = "btn_Attitude_Feedback";
            this.btn_Attitude_Feedback.Size = new System.Drawing.Size(138, 23);
            this.btn_Attitude_Feedback.TabIndex = 20;
            this.btn_Attitude_Feedback.Text = "Attitude Feedback";
            this.btn_Attitude_Feedback.UseVisualStyleBackColor = true;
            this.btn_Attitude_Feedback.Click += new System.EventHandler(this.btn_Attitude_Feedback_Click);
            // 
            // btn_Position_Feedback
            // 
            this.btn_Position_Feedback.Location = new System.Drawing.Point(130, 257);
            this.btn_Position_Feedback.Name = "btn_Position_Feedback";
            this.btn_Position_Feedback.Size = new System.Drawing.Size(138, 23);
            this.btn_Position_Feedback.TabIndex = 21;
            this.btn_Position_Feedback.Text = "Position Feedback";
            this.btn_Position_Feedback.UseVisualStyleBackColor = true;
            this.btn_Position_Feedback.Click += new System.EventHandler(this.btn_Position_Feedback_Click);
            // 
            // btn_TimeStamp
            // 
            this.btn_TimeStamp.Location = new System.Drawing.Point(290, 229);
            this.btn_TimeStamp.Name = "btn_TimeStamp";
            this.btn_TimeStamp.Size = new System.Drawing.Size(82, 23);
            this.btn_TimeStamp.TabIndex = 22;
            this.btn_TimeStamp.Text = "TimeStamp";
            this.btn_TimeStamp.UseVisualStyleBackColor = true;
            this.btn_TimeStamp.Click += new System.EventHandler(this.btn_TimeStamp_Click);
            // 
            // txt_MSG_replies
            // 
            this.txt_MSG_replies.Location = new System.Drawing.Point(458, 12);
            this.txt_MSG_replies.Name = "txt_MSG_replies";
            this.txt_MSG_replies.Size = new System.Drawing.Size(595, 449);
            this.txt_MSG_replies.TabIndex = 23;
            this.txt_MSG_replies.Text = "";
            // 
            // btn_data_tst_stop
            // 
            this.btn_data_tst_stop.Location = new System.Drawing.Point(232, 41);
            this.btn_data_tst_stop.Name = "btn_data_tst_stop";
            this.btn_data_tst_stop.Size = new System.Drawing.Size(115, 23);
            this.btn_data_tst_stop.TabIndex = 27;
            this.btn_data_tst_stop.Text = "Stop RT Data Test";
            this.btn_data_tst_stop.UseVisualStyleBackColor = true;
            this.btn_data_tst_stop.Click += new System.EventHandler(this.btn_data_tst_stop_Click);
            // 
            // btn_data_tst
            // 
            this.btn_data_tst.Location = new System.Drawing.Point(115, 41);
            this.btn_data_tst.Name = "btn_data_tst";
            this.btn_data_tst.Size = new System.Drawing.Size(111, 23);
            this.btn_data_tst.TabIndex = 26;
            this.btn_data_tst.Text = "Start RT Data Test";
            this.btn_data_tst.UseVisualStyleBackColor = true;
            this.btn_data_tst.Click += new System.EventHandler(this.btn_data_tst_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 473);
            this.Controls.Add(this.btn_data_tst_stop);
            this.Controls.Add(this.btn_data_tst);
            this.Controls.Add(this.txt_MSG_replies);
            this.Controls.Add(this.btn_TimeStamp);
            this.Controls.Add(this.btn_Position_Feedback);
            this.Controls.Add(this.btn_Attitude_Feedback);
            this.Controls.Add(this.btn_SetStatus);
            this.Controls.Add(this.Rolllabel);
            this.Controls.Add(this.AnyUBXlabel);
            this.Controls.Add(this.UBXcomboBox);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.TimeUtclabel);
            this.Controls.Add(this.temperaturelabel);
            this.Controls.Add(this.barolabel);
            this.Controls.Add(this.maglabel);
            this.Controls.Add(this.accellabel);
            this.Controls.Add(this.gyrolabel);
            this.Controls.Add(this.btn_GetStatus);
            this.Controls.Add(this.GPSlabel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PingButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox_coms);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox UBXcomboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label TimeUtclabel;
        private System.Windows.Forms.Label temperaturelabel;
        private System.Windows.Forms.Label barolabel;
        private System.Windows.Forms.Label maglabel;
        private System.Windows.Forms.Label accellabel;
        private System.Windows.Forms.Label gyrolabel;
        private System.Windows.Forms.Button btn_GetStatus;
        private System.Windows.Forms.Label GPSlabel;
        private System.Windows.Forms.RadioButton EEPROMReadradioButton;
        private System.Windows.Forms.RadioButton EEPROMWriteradioButton;
        private System.Windows.Forms.Button EEPROMButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button PingButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBox_coms;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton HexButton;
        private System.Windows.Forms.RadioButton DecimalButton;
        private System.Windows.Forms.ComboBox EEPROMcomboBox;
        private System.Windows.Forms.TextBox EEPROMtextBox;
        private System.Windows.Forms.Label AnyUBXlabel;
        private System.Windows.Forms.Label Rolllabel;
        private System.Windows.Forms.Button btn_SetStatus;
        private System.Windows.Forms.Button btn_Attitude_Feedback;
        private System.Windows.Forms.Button btn_Position_Feedback;
        private System.Windows.Forms.Button btn_TimeStamp;
        private System.Windows.Forms.RichTextBox txt_MSG_replies;
        private System.Windows.Forms.Button btn_data_tst_stop;
        private System.Windows.Forms.Button btn_data_tst;

    }
}

