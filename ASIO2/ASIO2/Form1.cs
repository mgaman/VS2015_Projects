using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Runtime.InteropServices;

using HDLC;
using ASIO2Messages;

namespace ASIO2
{
    public partial class Form1 : Form
    {
        #region Global Data
        SerialPort AsioSerialPort;   // Serial to ASIO
        delegate void hdlchandler(byte[] b);
        hdlchandler HdlcHandle;
        HDLCClass ReliableUart;
        byte[] GeneralWorkArea = new byte[50];  // must be larger than largest messager host -> tiva
        byte[] SingleByteMessage = new byte[1];
        uint counter;
        byte[] UBLOXAny;
        int UBLOXAnyIndex;
        #endregion
        #region Delegates
        delegate void UpdateLabel(Label l, String s);
        void updatelabel(Label l, String s)
        {
            l.Text = s;
        }
        #endregion
        #region Marshalled Data
        // messages sent are opcode + payload, messages received are just payload
        ASIOMessages.sPingReplyPayload PingReplyPayload;
        IntPtr PingPtr;
        ASIOMessages.sEEPROMValue EEPROMValue;
        IntPtr EEPROMValuePtr;
        ASIOMessages.sEnterBridgeMode EnterBridgeMode;
        IntPtr BridgeModePtr;
        ASIOMessages.sQuaternionPayload QuaternionPayload;
        IntPtr QuaternionPayloadPtr;
        ASIOMessages.sAttitude_EulersPayload EulersPayload;
        IntPtr EulersPayloadPtr;
        ASIOMessages.sUBXHeader ubloxHeader;
        IntPtr uboxHeaderPtr;
        UBLOX.sNavSol NavSol;
        IntPtr NavSolPtr;
        UBLOX.sNavPVT NavPVT;
        IntPtr NavPVTPtr;
        ASIOMessages.sAttitude_Feedback AttitudeFeedback;
        IntPtr AttitudeFeedbackPtr;
        ASIOMessages.sPositionEstimate PositionEstimate;
        IntPtr PositionEstimatePtr;
        ASIOMessages.sDebugMessage DebugMessage;
        IntPtr DebugMessagePtr;
        ASIOMessages.sPositionFeedback PositionFeedback;
        IntPtr PositionFeedbackPtr;
        ASIOMessages.sGetDeviceState GetDeviceState;
        IntPtr GetDeviceStatePtr;
        ASIOMessages.sSetDeviceState SetDeviceState;
        IntPtr SetDeviceStatePtr;
        #endregion
        void HdlcHandler(byte[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                if (ReliableUart.HDLCParse(b[i]))
                {
                    byte[] message = ReliableUart.HDLCUnStuff();
                    switch ((ASIOMessages.eOpcode)message[0])
                    {
                        case ASIOMessages.eOpcode.PING:
                            Marshal.Copy(message, 1, PingPtr, message.Length - 1);
                            PingReplyPayload = (ASIOMessages.sPingReplyPayload)Marshal.PtrToStructure(PingPtr, typeof(ASIOMessages.sPingReplyPayload));
                            PingLabel.Text = String.Format("ID {0},Ver {1}", PingReplyPayload.id, PingReplyPayload.version);
                            break;
                        case ASIOMessages.eOpcode.EEPROM_READ:
                            Marshal.Copy(message, 0, EEPROMValuePtr, message.Length);
                            EEPROMValue = (ASIOMessages.sEEPROMValue)Marshal.PtrToStructure(EEPROMValuePtr, typeof(ASIOMessages.sEEPROMValue));
                            counter = EEPROMValue.eeprom.data;
                            FormatEEPROMNumber();
                            break;
                        case ASIOMessages.eOpcode.DISCONNECT_CDC:
                            // AsioSerialPort.Close();
                            //  Application.Exit();
                            MessageBox.Show("Click the Disconnect button");
//                            AsioSerialPort.DiscardInBuffer();
//                            AsioSerialPort.Close();
                            break;
                        case ASIOMessages.eOpcode.ATTITUDE_QUATERNIONS:
                            Marshal.Copy(message, 1, QuaternionPayloadPtr, message.Length - 1);
                            QuaternionPayload = (ASIOMessages.sQuaternionPayload)Marshal.PtrToStructure(QuaternionPayloadPtr, typeof(ASIOMessages.sQuaternionPayload));
                            IMUtimestampLabel.Text = String.Format("TS {0} Q1 {1:F6} Q2 {2:F6} Q3 {3:F6} Q4 {4:F6}",
                                QuaternionPayload.timestamp.ToString(),QuaternionPayload.Q1.ToString(), QuaternionPayload.Q2.ToString(), QuaternionPayload.Q3.ToString(), QuaternionPayload.Q4.ToString());
                            break;
                        case ASIOMessages.eOpcode.SINGLE_DEVICE_ON_OFF:
                            Marshal.Copy(message, 0, SetDeviceStatePtr, message.Length);
                            SetDeviceState = (ASIOMessages.sSetDeviceState)Marshal.PtrToStructure(SetDeviceStatePtr, typeof(ASIOMessages.sSetDeviceState));
                            switch (SetDeviceState.device.device)
                            {
                                case ASIOMessages.eDevices.EPSON_IMU:
                                    IMUDevice.Enabled = true;
                                    IMUDevice.Checked = SetDeviceState.device.onoff == ASIOMessages.eOnOff.ON;
                                    break;
                                case ASIOMessages.eDevices.PNI_PRIME:
                                    PNIDevice.Enabled = true;
                                    PNIDevice.Checked = SetDeviceState.device.onoff == ASIOMessages.eOnOff.ON;
                                    break;
                                case ASIOMessages.eDevices.GPS:
                                    GPSDevice.Enabled = true;
                                    GPSDevice.Checked = SetDeviceState.device.onoff == ASIOMessages.eOnOff.ON;
                                    break;
                            }
                            break;
                        case ASIOMessages.eOpcode.UBLOX_FIRST:
                            // ublox messages may be > 255 bytes so they are sent in slices, 1 UBLOX_FIRST then 0 to N
                            // UBLOX_CONTINUE. The first block in the sequence < 255 byes long is the last block 
                            // assign space for the complete UBLOX block, assuming that the next bytes are a header
                            Marshal.Copy(message, 1, uboxHeaderPtr, 6/*System.Runtime.InteropServices.Marshal.SizeOf(sUBXHeader)*/);
                            ubloxHeader = (ASIOMessages.sUBXHeader)Marshal.PtrToStructure(uboxHeaderPtr, typeof(ASIOMessages.sUBXHeader));
                            UBLOXAny = new byte[ubloxHeader.length + 6];    // allow for header
                            Array.Copy(message, 1, UBLOXAny, 0, message.Length - 1);
                            if (ubloxHeader.length < (255 - 6))  // maxlength - headerlength
                                UBLOXAnalyse(UBLOXAny);
                            else
                                UBLOXAnyIndex = message.Length - 1;
                            break;
                        case ASIOMessages.eOpcode.UBLOX_CONTINUE:
                            Array.Copy(message, 1, UBLOXAny, UBLOXAnyIndex, message.Length - 1);
                            if (message.Length - 1 < 255)
                                UBLOXAnalyse(UBLOXAny);
                            else
                                UBLOXAnyIndex += message.Length - 1;
                            break;
                        case ASIOMessages.eOpcode.ATTITUDE_EULERS:
                            Marshal.Copy(message, 1, EulersPayloadPtr, message.Length - 1);
                            EulersPayload = (ASIOMessages.sAttitude_EulersPayload)Marshal.PtrToStructure(EulersPayloadPtr, typeof(ASIOMessages.sAttitude_EulersPayload));
                            EulerLabel.Text = String.Format("TS {0} Yaw {1:F6} Pitch {2:F6} Roll {3:F6}",
                                EulersPayload.timestamp.ToString(), EulersPayload.Yaw.ToString(), EulersPayload.Pitch.ToString(), EulersPayload.Roll.ToString(), QuaternionPayload.Q4.ToString());
                            break;
                        case ASIOMessages.eOpcode.DEBUG_MESSAGE:
                            Marshal.Copy(message, 0, DebugMessagePtr, message.Length);
                            DebugMessage = (ASIOMessages.sDebugMessage)Marshal.PtrToStructure(DebugMessagePtr, typeof(ASIOMessages.sDebugMessage));
                            DebugMessageLabel.Text = String.Format("Debug TS {0} Line {1} Code {2} Value {3}",
                                DebugMessage.timestamp.ToString(), DebugMessage.value1.ToString(), DebugMessage.value2.ToString(), DebugMessage.value3.ToString());
                            break;
                        case ASIOMessages.eOpcode.POSITION_ESTIMATION:
                            Marshal.Copy(message, 0, PositionEstimatePtr, message.Length);
                            PositionEstimate = (ASIOMessages.sPositionEstimate)Marshal.PtrToStructure(PositionEstimatePtr, typeof(ASIOMessages.sPositionEstimate));
                            GPStimeLabel.Text = PositionEstimate.timestamp.ToString();
                            GPSPosLabel.Text = String.Format("Lat {0} Long {1} Alt {2}",
                                ((float)PositionEstimate.latitude / 10000000.0F).ToString("F6"),
                                ((float)PositionEstimate.longitude / 10000000.0F).ToString("F6"),
                                ((float)PositionEstimate.AltitudeElipsoid / 1000.0F).ToString("F6"));
                            break;
                        default:
                            label1.Text = "unknown";
                            break;
                    }
                }
            }
        }
        /// <summary>
        /// Convert the 4-byte data received from EEPROM and display according to
        /// radio button selected.
        /// </summary>
        void FormatEEPROMNumber()
        {
            if (DecimalRB.Checked)
                EEPROMValueText.Text = counter.ToString();
            else if (HexRB.Checked)
                EEPROMValueText.Text = "0X" + counter.ToString("X8");
            else if (FloatRB.Checked)
            {
                // convert counter to byte array then byte array to float
                byte[] arr = BitConverter.GetBytes(counter);
                float f = BitConverter.ToSingle(arr, 0);
                EEPROMValueText.Text = f.ToString("F8") + "F";
            }
            else // must be float
            {
                EEPROMValueText.Text = "????";
            }
        }

        /// <summary>
        /// Allocate static storage for marshal data structures and their pointers
        /// </summary>
        void AllocateMarshalSpace()
        {
            PingPtr = Marshal.AllocHGlobal(Marshal.SizeOf(PingReplyPayload));
            BridgeModePtr = Marshal.AllocHGlobal(Marshal.SizeOf(EnterBridgeMode));
            EEPROMValuePtr = Marshal.AllocHGlobal(Marshal.SizeOf(EEPROMValue));
            QuaternionPayloadPtr = Marshal.AllocHGlobal(Marshal.SizeOf(QuaternionPayload));
            EulersPayloadPtr = Marshal.AllocHGlobal(Marshal.SizeOf(EulersPayload));
            SetDeviceStatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(SetDeviceState));
            uboxHeaderPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ubloxHeader));
            NavSolPtr = Marshal.AllocHGlobal(Marshal.SizeOf(NavSol));
            NavPVTPtr = Marshal.AllocHGlobal(Marshal.SizeOf(NavPVT));
            AttitudeFeedbackPtr = Marshal.AllocHGlobal(Marshal.SizeOf(AttitudeFeedback));
            DebugMessagePtr = Marshal.AllocHGlobal(Marshal.SizeOf(DebugMessage));
            PositionEstimatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(PositionEstimate));
            PositionFeedbackPtr = Marshal.AllocHGlobal(Marshal.SizeOf(PositionFeedback));
            GetDeviceStatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(GetDeviceState));
        }
        public Form1()
        {
            InitializeComponent();
            GlobalButtonsEnable(false);
            ReliableUart = new HDLCClass();
            ReliableUart.HDLCInit(1000);
            // initialize listbox
            string[] comports = SerialPort.GetPortNames();
            foreach (string s in comports)
            {
                ComPortListBox.Items.Add(s);
            }
            // select the last one
            ComPortListBox.SelectedIndex = ComPortListBox.Items.Count - 1;
            // setup serial ports
            AsioSerialPort = new SerialPort("blah1", 115200, Parity.None, 8, StopBits.One);
            // setup handler for hdlc data
            HdlcHandle = new hdlchandler(HdlcHandler);
            // allocate static memory for marshalled data
            AllocateMarshalSpace();
            // populate the EEPROM item combobox
            for (int i = (int)ASIOMessages.eEEPROMIndex.BOOT_KEY; i < (int)ASIOMessages.eEEPROMIndex.END_MARKER; i++)
                ItemListBox.Items.Add(((ASIOMessages.eEEPROMIndex)i).ToString());
            ItemListBox.SelectedIndex = 0;
            // populate the device list box, but only those that use UARTs and are candidates for Bridge mode
            DeviceListBox.Items.Add(ASIOMessages.eDevices.GPS.ToString());
            DeviceListBox.Items.Add(ASIOMessages.eDevices.EPSON_IMU.ToString());
            DeviceListBox.Items.Add(ASIOMessages.eDevices.PNI_PRIME.ToString());
//            DeviceListBox.SelectedIndex = 0;
            // populate the attitude feedback list box, only jpeg & optical tracker
            DeviceListBox2.Items.Add(ASIOMessages.eDevices.JPEG.ToString());
            DeviceListBox2.Items.Add(ASIOMessages.eDevices.OPTICAL_TRACKER.ToString());
        }
        private void UARTbutton_Click(object sender, EventArgs e)
        {
            if (AsioSerialPort == null || !AsioSerialPort.IsOpen)
            {
                AsioSerialPort.PortName = ComPortListBox.SelectedItem.ToString();
                AsioSerialPort.DataReceived += sp_DataReceived;
                AsioSerialPort.Open();
                UARTbutton.Text = "Disconnect";
                GlobalButtonsEnable(true);
            }
            else
            {
                AsioSerialPort.DataReceived -= sp_DataReceived;
                AsioSerialPort.Close();
                UARTbutton.Text = "Connect";
                GlobalButtonsEnable(false);
            }
        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //       throw new NotImplementedException();
            int size = AsioSerialPort.BytesToRead;
            byte[] data = new byte[size];
            AsioSerialPort.Read(data, 0, size);
            // invoke handler with the data
            this.Invoke(HdlcHandle, new object[] { data });
        }
        private void PingButton_Click(object sender, EventArgs e)
        {
            PingLabel.Text = String.Empty;
            SingleByteMessage[0] = (byte)ASIOMessages.eOpcode.PING;
            uint msglength = ReliableUart.HDLCStuff(SingleByteMessage, ref GeneralWorkArea);
            AsioSerialPort.Write(GeneralWorkArea, 0, (int)msglength);
        }
        private void EEPROMButton_Click(object sender, EventArgs e)
        {
            if (ReadRB.Checked)
            {
                EEPROMValue.opcode = (byte)ASIOMessages.eOpcode.EEPROM_READ;
                //                EEPROMPayloadMessage.eeprom.DWORDindex = (UInt16)EEPROMOffsetupdown.Value;
                EEPROMValue.eeprom.DWORDindex = (UInt16)(ASIOMessages.eEEPROMIndex)(Enum.Parse(typeof(ASIOMessages.eEEPROMIndex), ItemListBox.SelectedItem.ToString()));
                EEPROMValueText.Text = String.Empty;
            }
            else if (WriteRB.Checked)
            {
                EEPROMValue.opcode = (byte)ASIOMessages.eOpcode.EEPROM_WRITE;
                //          EEPROMPayloadMessage.eeprom.DWORDindex = 0; // (UInt16)EEPROMOffsetupdown.Value;
                EEPROMValue.eeprom.DWORDindex = (UInt16)(ASIOMessages.eEEPROMIndex)(Enum.Parse(typeof(ASIOMessages.eEEPROMIndex), ItemListBox.SelectedItem.ToString()));
                if (EEPROMValueText.Text.Length > 1 && EEPROMValueText.Text.Substring(0, 2).Equals("0X", StringComparison.CurrentCultureIgnoreCase))
                    EEPROMValue.eeprom.data = Convert.ToUInt32(EEPROMValueText.Text, 16);
                else if (EEPROMValueText.Text.EndsWith("F",StringComparison.InvariantCultureIgnoreCase))
                {
                    float f = float.Parse(EEPROMValueText.Text.Substring(0,EEPROMValueText.Text.Length-1));
                    byte[] arr = BitConverter.GetBytes(f);
                    EEPROMValue.eeprom.data = BitConverter.ToUInt32(arr, 0);
                }
                else
                    EEPROMValue.eeprom.data = UInt32.Parse(EEPROMValueText.Text);
            }
            byte[] payload = new byte[Marshal.SizeOf(EEPROMValue)];
            Marshal.StructureToPtr(EEPROMValue, EEPROMValuePtr, true);
            Marshal.Copy(EEPROMValuePtr, payload, 0, payload.Length);
            uint msglength = ReliableUart.HDLCStuff(payload, ref GeneralWorkArea);
            AsioSerialPort.Write(GeneralWorkArea, 0, (int)msglength);
        }
        private void DecimalRB_CheckedChanged(object sender, EventArgs e)
        {
            if (DecimalRB.Checked)
                FormatEEPROMNumber();
        }
        private void HexRB_CheckedChanged(object sender, EventArgs e)
        {
            if (HexRB.Checked)
                FormatEEPROMNumber();
        }
        private void FloatRB_CheckedChanged(object sender, EventArgs e)
        {
            if (FloatRB.Checked)
                FormatEEPROMNumber();
        }
        private void BridgeButton_Click(object sender, EventArgs e)
        {
            if (DeviceListBox.SelectedItem == null)
            {
                MessageBox.Show("You must select a device");
                return;
            }
            EnterBridgeMode.opcode = ASIOMessages.eOpcode.ENTER_BRIDGE_MODE;
            EnterBridgeMode.device = (ASIOMessages.eDevices)(Enum.Parse(typeof(ASIOMessages.eDevices), DeviceListBox.SelectedItem.ToString()));
            byte[] payload = new byte[Marshal.SizeOf(EnterBridgeMode)];
            Marshal.StructureToPtr(EnterBridgeMode, BridgeModePtr, true);
            Marshal.Copy(BridgeModePtr, payload, 0, payload.Length);
            uint msglength = ReliableUart.HDLCStuff(payload, ref GeneralWorkArea);
            AsioSerialPort.Write(GeneralWorkArea, 0, (int)msglength);
        }
        private void GlobalButtonsEnable(bool onoff)
        {
            PingButton.Enabled = onoff;
            BridgeButton.Enabled = onoff;
            EEPROMButton.Enabled = onoff;
            DeviceGetSetButton.Enabled = onoff;
            AttitudeFeedbackButton.Enabled = onoff;
            PositionFeedbackButton.Enabled = onoff;
        }
        private void DeviceGetSetButton_Click(object sender, EventArgs e)
        {
            SetDeviceState.opcode = ASIOMessages.eOpcode.SET_DEVICE_ON_OFF;
            GetDeviceState.opcode = ASIOMessages.eOpcode.GET_DEVICE_ON_OFF;
            bool set = DeviceSetRadioButton.Checked;
            if (GPSRb.Checked)
            {
               if (set)
               {
                    SetDeviceState.device.device = ASIOMessages.eDevices.GPS;
                    SetDeviceState.device.onoff = GPSDevice.Checked ? ASIOMessages.eOnOff.ON : ASIOMessages.eOnOff.OFF;
               }
               else
                    GetDeviceState.device.device = ASIOMessages.eDevices.GPS;
            }
            else if (IMURb.Checked)
            {
                if (set)
                {
                    SetDeviceState.device.device = ASIOMessages.eDevices.EPSON_IMU;
                    SetDeviceState.device.onoff = IMUDevice.Checked ? ASIOMessages.eOnOff.ON : ASIOMessages.eOnOff.OFF;
                }
                else
                    GetDeviceState.device.device = ASIOMessages.eDevices.EPSON_IMU;
            }
            else
            {
                if (set)
                {
                    SetDeviceState.device.device = ASIOMessages.eDevices.PNI_PRIME;
                    SetDeviceState.device.onoff = PNIDevice.Checked ? ASIOMessages.eOnOff.ON : ASIOMessages.eOnOff.OFF;
                }
                else
                    GetDeviceState.device.device = ASIOMessages.eDevices.PNI_PRIME;
            }
            byte[] payload;
            if (set)
            {
                payload = new byte[Marshal.SizeOf(SetDeviceState)];
                Marshal.StructureToPtr(SetDeviceState, SetDeviceStatePtr, true);
                Marshal.Copy(SetDeviceStatePtr, payload, 0, payload.Length);
            }
            else
            {
                payload = new byte[Marshal.SizeOf(GetDeviceState)];
                Marshal.StructureToPtr(GetDeviceState, GetDeviceStatePtr, true);
                Marshal.Copy(GetDeviceStatePtr, payload, 0, payload.Length);
            }
            uint msglength = ReliableUart.HDLCStuff(payload, ref GeneralWorkArea);
            AsioSerialPort.Write(GeneralWorkArea, 0, (int)msglength);
        }
        void UBLOXAnalyse(byte[] ublox)
        {
            // look at the header
            switch (ubloxHeader.uclass)
            {
                case UBLOX.uClass.C_NAV:
                    switch (ubloxHeader.id)
                    {
                        case UBLOX.uID.SOL:
                            Marshal.Copy(ublox, 6, NavSolPtr, System.Runtime.InteropServices.Marshal.SizeOf(NavSol));
                            NavSol = (UBLOX.sNavSol)Marshal.PtrToStructure(NavSolPtr, typeof(UBLOX.sNavSol));
                            GPStimeLabel.Text = String.Format("Week {0} ms {1}", NavSol.week.ToString(), NavSol.itow.ToString());
                            NumSVlabel.Text = String.Format("NumSv {0} Fix {1}", NavSol.numSV.ToString(), NavSol.gpsFix.ToString());
                            break;
                        case UBLOX.uID.PVT:
                            Marshal.Copy(ublox, 6, NavPVTPtr, System.Runtime.InteropServices.Marshal.SizeOf(NavPVT));
                            NavPVT = (UBLOX.sNavPVT)Marshal.PtrToStructure(NavPVTPtr, typeof(UBLOX.sNavPVT));
                            GPStimeLabel.Text = String.Format("{0}/{1}/{2} {3}:{4}:{5}",
                                NavPVT.year.ToString(),NavPVT.month.ToString(),NavPVT.day.ToString(),
                                NavPVT.hour.ToString(),NavPVT.minute.ToString(),NavPVT.second.ToString());
                            NumSVlabel.Text = String.Format("NumSv {0} Fix {1}", NavPVT.numSV.ToString(), NavPVT.fixtype.ToString());
                            if (NavPVT.fixtype > 1 && NavPVT.fixtype < 5)
                                GPSPosLabel.Text = String.Format("{0:F6} N {1:F6} E",
                                    NavPVT.lat/10000000F,NavPVT.lon/10000000F);
                            else
                                GPSPosLabel.Text = "No Fix";
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        private void AttitudeFeedbackButton_Click(object sender, EventArgs e)
        {
            if (DeviceListBox2.SelectedItem == null)
            {
                MessageBox.Show("You must select a device");
                return;
            }
            AttitudeFeedback.opcode = ASIOMessages.eOpcode.ATTITUDE_FEEDBACK;
            AttitudeFeedback.Device = (byte)(ASIOMessages.eDevices)(Enum.Parse(typeof(ASIOMessages.eDevices), DeviceListBox2.SelectedItem.ToString()));
            byte[] payload = new byte[Marshal.SizeOf(AttitudeFeedback)];
            Marshal.StructureToPtr(AttitudeFeedback, AttitudeFeedbackPtr, true);
            Marshal.Copy(AttitudeFeedbackPtr, payload, 0, payload.Length);
            uint msglength = ReliableUart.HDLCStuff(payload, ref GeneralWorkArea);
            AsioSerialPort.Write(GeneralWorkArea, 0, (int)msglength);
        }

        private void PositionFeedbackButton_Click(object sender, EventArgs e)
        {
            PositionFeedback.opcode = ASIOMessages.eOpcode.POSITION_FEEDBACK;
            byte[] payload = new byte[Marshal.SizeOf(PositionFeedback)];
            Marshal.StructureToPtr(PositionFeedback, PositionFeedbackPtr, true);
            Marshal.Copy(PositionFeedbackPtr, payload, 0, payload.Length);
            uint msglength = ReliableUart.HDLCStuff(payload, ref GeneralWorkArea);
            AsioSerialPort.Write(GeneralWorkArea, 0, (int)msglength);
        }

        private void Form1_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AsioSerialPort.IsOpen)
            {
                AsioSerialPort.DiscardInBuffer();
                AsioSerialPort.Close();
            }
        }
    }
}
