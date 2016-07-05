namespace ASIO2
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
            this.UARTbutton = new System.Windows.Forms.Button();
            this.ComPortListBox = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PingLabel = new System.Windows.Forms.Label();
            this.PingButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.DeviceListBox = new System.Windows.Forms.ListBox();
            this.BridgeButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.EEPROMButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.FloatRB = new System.Windows.Forms.RadioButton();
            this.HexRB = new System.Windows.Forms.RadioButton();
            this.DecimalRB = new System.Windows.Forms.RadioButton();
            this.EEPROMValueText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ItemListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.WriteRB = new System.Windows.Forms.RadioButton();
            this.ReadRB = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.IMUtimestampLabel = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.DeviceGetSetButton = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.PNIRb = new System.Windows.Forms.RadioButton();
            this.GPSRb = new System.Windows.Forms.RadioButton();
            this.PNIDevice = new System.Windows.Forms.CheckBox();
            this.IMURb = new System.Windows.Forms.RadioButton();
            this.IMUDevice = new System.Windows.Forms.CheckBox();
            this.GPSDevice = new System.Windows.Forms.CheckBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.DeviceGetRadioButton = new System.Windows.Forms.RadioButton();
            this.DeviceSetRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.GPSPosLabel = new System.Windows.Forms.Label();
            this.NumSVlabel = new System.Windows.Forms.Label();
            this.GPStimeLabel = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.EulerLabel = new System.Windows.Forms.Label();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.AttitudeFeedbackButton = new System.Windows.Forms.Button();
            this.DeviceListBox2 = new System.Windows.Forms.ListBox();
            this.DebugMessageLabel = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.PositionFeedbackButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.SuspendLayout();
            // 
            // UARTbutton
            // 
            this.UARTbutton.Location = new System.Drawing.Point(13, 13);
            this.UARTbutton.Name = "UARTbutton";
            this.UARTbutton.Size = new System.Drawing.Size(75, 23);
            this.UARTbutton.TabIndex = 0;
            this.UARTbutton.Text = "Connect";
            this.UARTbutton.UseVisualStyleBackColor = true;
            this.UARTbutton.Click += new System.EventHandler(this.UARTbutton_Click);
            // 
            // ComPortListBox
            // 
            this.ComPortListBox.FormattingEnabled = true;
            this.ComPortListBox.Location = new System.Drawing.Point(94, 13);
            this.ComPortListBox.Name = "ComPortListBox";
            this.ComPortListBox.Size = new System.Drawing.Size(120, 30);
            this.ComPortListBox.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.groupBox1.Controls.Add(this.PingLabel);
            this.groupBox1.Controls.Add(this.PingButton);
            this.groupBox1.Location = new System.Drawing.Point(13, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 53);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ping";
            // 
            // PingLabel
            // 
            this.PingLabel.AutoSize = true;
            this.PingLabel.Location = new System.Drawing.Point(88, 28);
            this.PingLabel.Name = "PingLabel";
            this.PingLabel.Size = new System.Drawing.Size(73, 13);
            this.PingLabel.TabIndex = 2;
            this.PingLabel.Text = "Ping...............";
            // 
            // PingButton
            // 
            this.PingButton.Location = new System.Drawing.Point(6, 19);
            this.PingButton.Name = "PingButton";
            this.PingButton.Size = new System.Drawing.Size(75, 23);
            this.PingButton.TabIndex = 1;
            this.PingButton.Text = "Ping";
            this.PingButton.UseVisualStyleBackColor = true;
            this.PingButton.Click += new System.EventHandler(this.PingButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.groupBox2.Controls.Add(this.DeviceListBox);
            this.groupBox2.Controls.Add(this.BridgeButton);
            this.groupBox2.Location = new System.Drawing.Point(246, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 53);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bridge";
            // 
            // DeviceListBox
            // 
            this.DeviceListBox.FormattingEnabled = true;
            this.DeviceListBox.Location = new System.Drawing.Point(87, 19);
            this.DeviceListBox.Name = "DeviceListBox";
            this.DeviceListBox.Size = new System.Drawing.Size(120, 30);
            this.DeviceListBox.TabIndex = 3;
            // 
            // BridgeButton
            // 
            this.BridgeButton.Location = new System.Drawing.Point(6, 19);
            this.BridgeButton.Name = "BridgeButton";
            this.BridgeButton.Size = new System.Drawing.Size(75, 23);
            this.BridgeButton.TabIndex = 2;
            this.BridgeButton.Text = "Send";
            this.BridgeButton.UseVisualStyleBackColor = true;
            this.BridgeButton.Click += new System.EventHandler(this.BridgeButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.groupBox3.Controls.Add(this.EEPROMButton);
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.EEPROMValueText);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.ItemListBox);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(13, 130);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(453, 100);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EEPROM";
            // 
            // EEPROMButton
            // 
            this.EEPROMButton.Location = new System.Drawing.Point(362, 19);
            this.EEPROMButton.Name = "EEPROMButton";
            this.EEPROMButton.Size = new System.Drawing.Size(75, 23);
            this.EEPROMButton.TabIndex = 8;
            this.EEPROMButton.Text = "Send";
            this.EEPROMButton.UseVisualStyleBackColor = true;
            this.EEPROMButton.Click += new System.EventHandler(this.EEPROMButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox5.Controls.Add(this.FloatRB);
            this.groupBox5.Controls.Add(this.HexRB);
            this.groupBox5.Controls.Add(this.DecimalRB);
            this.groupBox5.Location = new System.Drawing.Point(272, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(84, 88);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Format";
            // 
            // FloatRB
            // 
            this.FloatRB.AutoSize = true;
            this.FloatRB.Location = new System.Drawing.Point(6, 65);
            this.FloatRB.Name = "FloatRB";
            this.FloatRB.Size = new System.Drawing.Size(48, 17);
            this.FloatRB.TabIndex = 3;
            this.FloatRB.Text = "Float";
            this.FloatRB.UseVisualStyleBackColor = true;
            this.FloatRB.CheckedChanged += new System.EventHandler(this.FloatRB_CheckedChanged);
            // 
            // HexRB
            // 
            this.HexRB.AutoSize = true;
            this.HexRB.Location = new System.Drawing.Point(6, 43);
            this.HexRB.Name = "HexRB";
            this.HexRB.Size = new System.Drawing.Size(44, 17);
            this.HexRB.TabIndex = 2;
            this.HexRB.Text = "Hex";
            this.HexRB.UseVisualStyleBackColor = true;
            this.HexRB.CheckedChanged += new System.EventHandler(this.HexRB_CheckedChanged);
            // 
            // DecimalRB
            // 
            this.DecimalRB.AutoSize = true;
            this.DecimalRB.Checked = true;
            this.DecimalRB.Location = new System.Drawing.Point(6, 20);
            this.DecimalRB.Name = "DecimalRB";
            this.DecimalRB.Size = new System.Drawing.Size(63, 17);
            this.DecimalRB.TabIndex = 1;
            this.DecimalRB.TabStop = true;
            this.DecimalRB.Text = "Decimal";
            this.DecimalRB.UseVisualStyleBackColor = true;
            this.DecimalRB.CheckedChanged += new System.EventHandler(this.DecimalRB_CheckedChanged);
            // 
            // EEPROMValueText
            // 
            this.EEPROMValueText.Location = new System.Drawing.Point(120, 68);
            this.EEPROMValueText.Name = "EEPROMValueText";
            this.EEPROMValueText.Size = new System.Drawing.Size(125, 20);
            this.EEPROMValueText.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Value";
            // 
            // ItemListBox
            // 
            this.ItemListBox.FormattingEnabled = true;
            this.ItemListBox.Location = new System.Drawing.Point(120, 19);
            this.ItemListBox.Name = "ItemListBox";
            this.ItemListBox.Size = new System.Drawing.Size(120, 30);
            this.ItemListBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Item";
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.groupBox4.Controls.Add(this.WriteRB);
            this.groupBox4.Controls.Add(this.ReadRB);
            this.groupBox4.Location = new System.Drawing.Point(7, 20);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(62, 74);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "R/W";
            // 
            // WriteRB
            // 
            this.WriteRB.AutoSize = true;
            this.WriteRB.Location = new System.Drawing.Point(7, 43);
            this.WriteRB.Name = "WriteRB";
            this.WriteRB.Size = new System.Drawing.Size(50, 17);
            this.WriteRB.TabIndex = 1;
            this.WriteRB.Text = "Write";
            this.WriteRB.UseVisualStyleBackColor = true;
            // 
            // ReadRB
            // 
            this.ReadRB.AutoSize = true;
            this.ReadRB.Checked = true;
            this.ReadRB.Location = new System.Drawing.Point(7, 20);
            this.ReadRB.Name = "ReadRB";
            this.ReadRB.Size = new System.Drawing.Size(51, 17);
            this.ReadRB.TabIndex = 0;
            this.ReadRB.TabStop = true;
            this.ReadRB.Text = "Read";
            this.ReadRB.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.Silver;
            this.groupBox6.Controls.Add(this.IMUtimestampLabel);
            this.groupBox6.Location = new System.Drawing.Point(13, 237);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(453, 49);
            this.groupBox6.TabIndex = 5;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Quaternion";
            // 
            // IMUtimestampLabel
            // 
            this.IMUtimestampLabel.AutoSize = true;
            this.IMUtimestampLabel.Location = new System.Drawing.Point(14, 20);
            this.IMUtimestampLabel.Name = "IMUtimestampLabel";
            this.IMUtimestampLabel.Size = new System.Drawing.Size(35, 13);
            this.IMUtimestampLabel.TabIndex = 0;
            this.IMUtimestampLabel.Text = "label3";
            // 
            // groupBox7
            // 
            this.groupBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.groupBox7.Controls.Add(this.DeviceGetSetButton);
            this.groupBox7.Controls.Add(this.groupBox9);
            this.groupBox7.Controls.Add(this.groupBox8);
            this.groupBox7.Location = new System.Drawing.Point(481, 59);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(238, 139);
            this.groupBox7.TabIndex = 6;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Devices";
            // 
            // DeviceGetSetButton
            // 
            this.DeviceGetSetButton.Location = new System.Drawing.Point(15, 100);
            this.DeviceGetSetButton.Name = "DeviceGetSetButton";
            this.DeviceGetSetButton.Size = new System.Drawing.Size(75, 23);
            this.DeviceGetSetButton.TabIndex = 9;
            this.DeviceGetSetButton.Text = "Send";
            this.DeviceGetSetButton.UseVisualStyleBackColor = true;
            this.DeviceGetSetButton.Click += new System.EventHandler(this.DeviceGetSetButton_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.PNIRb);
            this.groupBox9.Controls.Add(this.GPSRb);
            this.groupBox9.Controls.Add(this.PNIDevice);
            this.groupBox9.Controls.Add(this.IMURb);
            this.groupBox9.Controls.Add(this.IMUDevice);
            this.groupBox9.Controls.Add(this.GPSDevice);
            this.groupBox9.Location = new System.Drawing.Point(109, 19);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(109, 90);
            this.groupBox9.TabIndex = 6;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Select";
            // 
            // PNIRb
            // 
            this.PNIRb.AutoSize = true;
            this.PNIRb.Location = new System.Drawing.Point(11, 59);
            this.PNIRb.Name = "PNIRb";
            this.PNIRb.Size = new System.Drawing.Size(14, 13);
            this.PNIRb.TabIndex = 5;
            this.PNIRb.UseVisualStyleBackColor = true;
            // 
            // GPSRb
            // 
            this.GPSRb.AutoSize = true;
            this.GPSRb.Location = new System.Drawing.Point(11, 38);
            this.GPSRb.Name = "GPSRb";
            this.GPSRb.Size = new System.Drawing.Size(14, 13);
            this.GPSRb.TabIndex = 4;
            this.GPSRb.UseVisualStyleBackColor = true;
            // 
            // PNIDevice
            // 
            this.PNIDevice.AutoSize = true;
            this.PNIDevice.Enabled = false;
            this.PNIDevice.Location = new System.Drawing.Point(31, 58);
            this.PNIDevice.Name = "PNIDevice";
            this.PNIDevice.Size = new System.Drawing.Size(44, 17);
            this.PNIDevice.TabIndex = 4;
            this.PNIDevice.Text = "PNI";
            this.PNIDevice.UseVisualStyleBackColor = true;
            // 
            // IMURb
            // 
            this.IMURb.AutoSize = true;
            this.IMURb.Checked = true;
            this.IMURb.Location = new System.Drawing.Point(11, 19);
            this.IMURb.Name = "IMURb";
            this.IMURb.Size = new System.Drawing.Size(14, 13);
            this.IMURb.TabIndex = 1;
            this.IMURb.TabStop = true;
            this.IMURb.UseVisualStyleBackColor = true;
            // 
            // IMUDevice
            // 
            this.IMUDevice.AutoSize = true;
            this.IMUDevice.Enabled = false;
            this.IMUDevice.Location = new System.Drawing.Point(31, 19);
            this.IMUDevice.Name = "IMUDevice";
            this.IMUDevice.Size = new System.Drawing.Size(46, 17);
            this.IMUDevice.TabIndex = 3;
            this.IMUDevice.Text = "IMU";
            this.IMUDevice.UseVisualStyleBackColor = true;
            // 
            // GPSDevice
            // 
            this.GPSDevice.AutoSize = true;
            this.GPSDevice.Enabled = false;
            this.GPSDevice.Location = new System.Drawing.Point(31, 38);
            this.GPSDevice.Name = "GPSDevice";
            this.GPSDevice.Size = new System.Drawing.Size(48, 17);
            this.GPSDevice.TabIndex = 2;
            this.GPSDevice.Text = "GPS";
            this.GPSDevice.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.DeviceGetRadioButton);
            this.groupBox8.Controls.Add(this.DeviceSetRadioButton);
            this.groupBox8.Location = new System.Drawing.Point(8, 19);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(95, 75);
            this.groupBox8.TabIndex = 5;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Action";
            // 
            // DeviceGetRadioButton
            // 
            this.DeviceGetRadioButton.AutoSize = true;
            this.DeviceGetRadioButton.Checked = true;
            this.DeviceGetRadioButton.Location = new System.Drawing.Point(6, 19);
            this.DeviceGetRadioButton.Name = "DeviceGetRadioButton";
            this.DeviceGetRadioButton.Size = new System.Drawing.Size(42, 17);
            this.DeviceGetRadioButton.TabIndex = 0;
            this.DeviceGetRadioButton.TabStop = true;
            this.DeviceGetRadioButton.Text = "Get";
            this.DeviceGetRadioButton.UseVisualStyleBackColor = true;
            // 
            // DeviceSetRadioButton
            // 
            this.DeviceSetRadioButton.AutoSize = true;
            this.DeviceSetRadioButton.Location = new System.Drawing.Point(7, 42);
            this.DeviceSetRadioButton.Name = "DeviceSetRadioButton";
            this.DeviceSetRadioButton.Size = new System.Drawing.Size(41, 17);
            this.DeviceSetRadioButton.TabIndex = 1;
            this.DeviceSetRadioButton.Text = "Set";
            this.DeviceSetRadioButton.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.BackColor = System.Drawing.Color.Olive;
            this.groupBox10.Controls.Add(this.GPSPosLabel);
            this.groupBox10.Controls.Add(this.NumSVlabel);
            this.groupBox10.Controls.Add(this.GPStimeLabel);
            this.groupBox10.Location = new System.Drawing.Point(481, 204);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(200, 88);
            this.groupBox10.TabIndex = 7;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "GPS";
            // 
            // GPSPosLabel
            // 
            this.GPSPosLabel.AutoSize = true;
            this.GPSPosLabel.Location = new System.Drawing.Point(7, 70);
            this.GPSPosLabel.Name = "GPSPosLabel";
            this.GPSPosLabel.Size = new System.Drawing.Size(35, 13);
            this.GPSPosLabel.TabIndex = 2;
            this.GPSPosLabel.Text = "label3";
            // 
            // NumSVlabel
            // 
            this.NumSVlabel.AutoSize = true;
            this.NumSVlabel.Location = new System.Drawing.Point(7, 43);
            this.NumSVlabel.Name = "NumSVlabel";
            this.NumSVlabel.Size = new System.Drawing.Size(35, 13);
            this.NumSVlabel.TabIndex = 1;
            this.NumSVlabel.Text = "label3";
            // 
            // GPStimeLabel
            // 
            this.GPStimeLabel.AutoSize = true;
            this.GPStimeLabel.Location = new System.Drawing.Point(7, 20);
            this.GPStimeLabel.Name = "GPStimeLabel";
            this.GPStimeLabel.Size = new System.Drawing.Size(35, 13);
            this.GPStimeLabel.TabIndex = 0;
            this.GPStimeLabel.Text = "label3";
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.Color.Silver;
            this.groupBox11.Controls.Add(this.EulerLabel);
            this.groupBox11.Location = new System.Drawing.Point(13, 303);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(453, 49);
            this.groupBox11.TabIndex = 8;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Euler";
            // 
            // EulerLabel
            // 
            this.EulerLabel.AutoSize = true;
            this.EulerLabel.Location = new System.Drawing.Point(14, 20);
            this.EulerLabel.Name = "EulerLabel";
            this.EulerLabel.Size = new System.Drawing.Size(35, 13);
            this.EulerLabel.TabIndex = 0;
            this.EulerLabel.Text = "label3";
            // 
            // groupBox12
            // 
            this.groupBox12.BackColor = System.Drawing.Color.Fuchsia;
            this.groupBox12.Controls.Add(this.AttitudeFeedbackButton);
            this.groupBox12.Controls.Add(this.DeviceListBox2);
            this.groupBox12.Location = new System.Drawing.Point(13, 359);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(228, 67);
            this.groupBox12.TabIndex = 9;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "AttitudeFeedback";
            // 
            // AttitudeFeedbackButton
            // 
            this.AttitudeFeedbackButton.Location = new System.Drawing.Point(133, 19);
            this.AttitudeFeedbackButton.Name = "AttitudeFeedbackButton";
            this.AttitudeFeedbackButton.Size = new System.Drawing.Size(75, 23);
            this.AttitudeFeedbackButton.TabIndex = 5;
            this.AttitudeFeedbackButton.Text = "Send";
            this.AttitudeFeedbackButton.UseVisualStyleBackColor = true;
            this.AttitudeFeedbackButton.Click += new System.EventHandler(this.AttitudeFeedbackButton_Click);
            // 
            // DeviceListBox2
            // 
            this.DeviceListBox2.FormattingEnabled = true;
            this.DeviceListBox2.Location = new System.Drawing.Point(7, 19);
            this.DeviceListBox2.Name = "DeviceListBox2";
            this.DeviceListBox2.Size = new System.Drawing.Size(120, 30);
            this.DeviceListBox2.TabIndex = 4;
            // 
            // DebugMessageLabel
            // 
            this.DebugMessageLabel.AutoSize = true;
            this.DebugMessageLabel.Location = new System.Drawing.Point(249, 18);
            this.DebugMessageLabel.Name = "DebugMessageLabel";
            this.DebugMessageLabel.Size = new System.Drawing.Size(82, 13);
            this.DebugMessageLabel.TabIndex = 10;
            this.DebugMessageLabel.Text = "debug message";
            // 
            // groupBox13
            // 
            this.groupBox13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.groupBox13.Controls.Add(this.PositionFeedbackButton);
            this.groupBox13.Location = new System.Drawing.Point(268, 358);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(228, 67);
            this.groupBox13.TabIndex = 12;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "PositionFeedback";
            // 
            // PositionFeedbackButton
            // 
            this.PositionFeedbackButton.Location = new System.Drawing.Point(55, 20);
            this.PositionFeedbackButton.Name = "PositionFeedbackButton";
            this.PositionFeedbackButton.Size = new System.Drawing.Size(75, 23);
            this.PositionFeedbackButton.TabIndex = 5;
            this.PositionFeedbackButton.Text = "Send";
            this.PositionFeedbackButton.UseVisualStyleBackColor = true;
            this.PositionFeedbackButton.Click += new System.EventHandler(this.PositionFeedbackButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 514);
            this.Controls.Add(this.groupBox13);
            this.Controls.Add(this.DebugMessageLabel);
            this.Controls.Add(this.groupBox12);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ComPortListBox);
            this.Controls.Add(this.UARTbutton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "ASIO2 Technician Console";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.Form1_GiveFeedback);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox12.ResumeLayout(false);
            this.groupBox13.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UARTbutton;
        private System.Windows.Forms.ListBox ComPortListBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label PingLabel;
        private System.Windows.Forms.Button PingButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox DeviceListBox;
        private System.Windows.Forms.Button BridgeButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox EEPROMValueText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox ItemListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton WriteRB;
        private System.Windows.Forms.RadioButton ReadRB;
        private System.Windows.Forms.Button EEPROMButton;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton FloatRB;
        private System.Windows.Forms.RadioButton HexRB;
        private System.Windows.Forms.RadioButton DecimalRB;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label IMUtimestampLabel;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.CheckBox PNIDevice;
        private System.Windows.Forms.CheckBox IMUDevice;
        private System.Windows.Forms.CheckBox GPSDevice;
        private System.Windows.Forms.RadioButton DeviceSetRadioButton;
        private System.Windows.Forms.RadioButton DeviceGetRadioButton;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton PNIRb;
        private System.Windows.Forms.RadioButton GPSRb;
        private System.Windows.Forms.RadioButton IMURb;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button DeviceGetSetButton;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Label NumSVlabel;
        private System.Windows.Forms.Label GPStimeLabel;
        private System.Windows.Forms.Label GPSPosLabel;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label EulerLabel;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Button AttitudeFeedbackButton;
        private System.Windows.Forms.ListBox DeviceListBox2;
        private System.Windows.Forms.Label DebugMessageLabel;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Button PositionFeedbackButton;
    }
}

