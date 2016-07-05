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
using ASIOMessages;

namespace HDCLUart
{
    public partial class Form1 : Form
    {
        #region Marshalled Data

        //OBOX reply to Ping with ID and SW version data
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct MSG_O2H_Ping_Reply
        {
	        public byte opcode;

  	        public byte ID;
  	        public short SoftwareVersion;
        };

        //Host and Client must maintain a synchronized 1 millisecond timer clock. The client initiates the timer and transmits its value every 10 seconds
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_O2H_TimeStamp
        {
	        public byte opcode;

	        public UInt32 timestamp;

        };

        //Response to Device State / Get Device State Commands
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_O2H_Device_State_Reply
        {
	        public byte opcode;

	        public UInt32 timestamp;
	        public byte device;
	        public byte onoff;

        };

        //OBOX sends LOS (attitude) estimates data via Quaternions
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_O2H_Attitude_Quaternion_Estimate
        {
	        public byte opcode;

	        public UInt32 timestamp;
            public double Q1, Q2, Q3, Q4;
            public float uncertainty; 	// attitude uncertainty
	        public byte status;

        };

        //OBOX sends LOS (attitude) estimates data via Eulers Angles
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_O2H_Attitude_Eulers
        {
	        public byte opcode;

	        public UInt32 timestamp;
            public double Yaw;		// angle relative to North
            public double Pitch;	// angle relative to horizon
            public double Roll;	// angle relative to horizon
            public float uncertainty;// attitude uncertainty
	        public byte status;

        };

        //OBOX sends GNSS Position estimates data
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_O2H_Position_Estimate
        {
	        public byte opcode;

	        public UInt32 timestamp;
	        public UInt32 UTCtime;
	        public Int32 latitude; // degrees * 1E7
	        public Int32 longitude;
	        public Int32 AltitudeElipsoid; // mm
	        public byte NavStat;
	        public UInt32 hAcc; // Accuracy estimates mm
	        public UInt32 vAcc;// Accuracy estimates mm
	        public Int32 SOG; // speed & course over ground mm/hour
	        public Int32 COG; // speed & course over ground mm/hour
	        public Int32 vVel; // vertical velocity  m/s
	        public Int32 diffAge; // -1 if DPGS not used
	        public Int32 HDOP,VDOP,TDOP; // Dilution of Precision * 100
	        public byte numSvs;
	        public byte DR; // 1 if DR used, else 0

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct MSG_O2H_Reserved_for_Tests
        {
            public byte opcode;

            public UInt32 ts_snt_from_tiva;
            public UInt32 ts_rcvd_from_sensor;
            public UInt32 ts_went_from_sensor;           
          
            public float gx,gy,gz;//gyro
            public float ax,ay,az;//accl
            public double q1, q2, q3, q4;//queterions
            public double ex, ey, ez;//eulers

            public float w_b_x, w_b_y, w_b_z;


        };


        /*
		        OBOX to Host messages end
        */




        /*
		        Host to OBOX messages start
        */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_H2O_Set_Device_State
        {
	        public byte opcode;

	        public UInt32 timestamp;
	        public byte device;  // see list of devices
	        public byte onoff;  // see values ON and OFF

        };

        //The host can request the state of the internal devices
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_H2O_Get_Device_State
        {
	        public byte opcode;

	        public UInt32 timestamp;
	        public byte device;

        };

        //Host returns attitude feedback / corrections from an external device / source to the OBOX. These are used to enhance OBOX real-time performance
        //size: 74 bytes
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_H2O_Attitude_Feedback
        {
	        public byte opcode;

	        public UInt32 timestamp;
	        public byte Device;
	        public double Q1,Q2,Q3,Q4;
	        public double Q1_rate,Q2_rate,Q3_rate,Q4_rate;
	        public float score;

        };

        //Host returns position feedback / corrections from an external device / source to the OBOX. These are used to enhance OBOX real-time performance
        //size: 21 bytes
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_H2O_Position_Feedback
        {
	        public byte opcode;

	        public UInt32 timestamp;
	        public Int32 latitude;	// degrees * 1E7
	        public Int32 longitude;
	        public Int32 AltitudeElipsoid; //mm
	        public Int32 score;   // * 1000

        };

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
         struct MSG_H2O_TimeStamp
        {
	        public byte opcode;

	        public UInt32 timestamp;

        };


        /*
                 Host to OBOX messages end
         */



        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sUBX_NAV_SAT {
            public UInt32 iTOW;
            public byte version;
            public byte numSVs;
            public  byte r1, r2;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sUBX_NAV_POSLLH
        {
            public UInt32 iTOW;
            public Int32 lon;
            public Int32 lat;
            public Int32 height;
            public Int32 hMSL;
            public UInt32 hAcc;
            public UInt32 vAcc;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sADISdata
        {
            public Int16 gx, gy, gz;  // gyro
            public Int16 ax, ay, az;  // accel
            public Int16 mx, my, mz;  // mag
            public Int16 baro;  // baro
            public Int16 temp;  // temperature
        } ;
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sPingReply
        {
            public byte id;
            public UInt16 version;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sEEPROMValue
        {
            public UInt16 DWORDindex;
            public UInt32 data;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sEEPROMPayload {
	        public byte opcode;
	        public sEEPROMValue eeprom;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sSystemState
        {
            public byte System;
            public byte Laser;
            public byte Navigation;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sUBXHeader
        {
            byte header1; // 0xb5,0x62
            byte header2;
            public ASIOMessages.UBLOX.uClass uclass;
            public ASIOMessages.UBLOX.uID id;
            public UInt16 length;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sUBX_NAV_TIMEUTC {
	        public UInt32 iTOW;
	        public UInt32 timeaccuracyestimate;
            public Int32 nanosec;
	        public UInt16 year;
	        public byte month;
	        public byte day;
	        public byte hour;
	        public byte minute;
	        public byte second;
            public byte valid;
        };
        const int MAX_DATA_PER_ADIS_REGS_BLOCK = 20;
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct sADISregisters {
	        public byte  page;
	        public byte  first_register;
	        public byte  extent;
       //     public byte[] data = new byte[MAX_DATA_PER_ADIS_REGS_BLOCK];
        };
        #endregion
        // allocate space once & never free 

        IntPtr DataPtr;
        //ASIO2
        //H2O
        IntPtr SetDeviceStatePtr;
        MSG_H2O_Set_Device_State SetDeviceState;

        IntPtr GetDeviceStatePtr;
        MSG_H2O_Get_Device_State GetDeviceState;

        IntPtr AttitudeFeedbackPtr;
        MSG_H2O_Attitude_Feedback AttitudeFeedback;

        IntPtr PositionFeedbackPtr;
        MSG_H2O_Position_Feedback PositionFeedback;

        IntPtr TimeStampPtr;
        MSG_H2O_TimeStamp TimeStamp;


        //O2H
        IntPtr Ping_ReplyPtr;
        MSG_O2H_Ping_Reply Ping_Reply;

        IntPtr Device_State_ReplyPtr;
        MSG_O2H_Device_State_Reply Device_State_Reply;

        IntPtr O2H_TimeStampPtr;
        MSG_O2H_TimeStamp O2H_TimeStamp;

        IntPtr Attitide_Quaternion_EstimatePtr;
        MSG_O2H_Attitude_Quaternion_Estimate Attitude_Quaternion_Estimate;

        IntPtr Attitude_EulersPtr;
        MSG_O2H_Attitude_Eulers Attitude_Eulers;

        IntPtr Position_EstimatePtr;
        MSG_O2H_Position_Estimate Position_Estimate;

        IntPtr Reserved_for_TestsPtr;
        MSG_O2H_Reserved_for_Tests Reserved_for_Tests;

        //ASIO2 end



        IntPtr NAVSATPtr;
        sUBX_NAV_SAT NAVSATmessage;
        IntPtr PosLLHPtr;
        sUBX_NAV_POSLLH PosLLHMessage;
        IntPtr uboxHeaderPtr;
        sUBXHeader ubloxHeader;
        IntPtr PingPtr;
        sPingReply PingMessage;
        IntPtr EEPROMPtr;
        sEEPROMValue EEPROMMessage;
        IntPtr EEPROMPayloadPtr;
        sEEPROMPayload EEPROMPayloadMessage;
        IntPtr SystemStatePtr;
        sSystemState SystemStateMessage;
        IntPtr ADISDataPtr;
        sADISdata AdisMessage;
        IntPtr TimeUTCPtr;
        sUBX_NAV_TIMEUTC TimeUTCMessage;
        sADISregisters ADISregisters;
        IntPtr ADISregsPtr;

        UInt16 tst_total = 1000;
        UInt16 tst_indx = 0;
        UInt32[] tst_arr = new UInt32[4000];

        //for data tests start
        StringBuilder csv_write_h = new StringBuilder();
        int csv_write_cnt = 0;

        bool continous_data_test_on = false;
        StreamReader csv_read_h = null;
        //for data tests end

        #region Global Data
        delegate void hdlchandler(byte[] b);
        hdlchandler HdlcHandle;
        HDLCClass ReliableUart;
        SerialPort sp;
 //       System.Windows.Forms.Timer ticktock;
        byte[] GeneralWorkArea = new byte[256];
        byte [] SingleByteMessage = new byte[1];
        struct sUBX
        {
            public string text;
            public byte code;
        };
        sUBX[] UBXValues = new sUBX[] {
            new sUBX() {text = "DOP",code= 0x04},
            new sUBX() {text = "ORB", code = 0x34 }
        };
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
        void HdlcHandler(byte[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                if (ReliableUart.HDLCParse(b[i]))
                {
                    byte[] message = ReliableUart.HDLCUnStuff();
                    switch ((ASIOMessages.ASIOMessages.Opcode_O2H)message[0])
                    {
                         //the following cases are relevant for OBOX (ASIO2) project

                        case ASIOMessages.ASIOMessages.Opcode_O2H.PING_REPLY:
                            Marshal.Copy(message, 0, PingPtr, message.Length );
                            Ping_Reply = (MSG_O2H_Ping_Reply)Marshal.PtrToStructure(PingPtr, typeof(MSG_O2H_Ping_Reply));
                            label1.Text = String.Format("ID {0}, Ver {1}", Ping_Reply.ID, Ping_Reply.SoftwareVersion);
                            break;

                        case ASIOMessages.ASIOMessages.Opcode_O2H.DEVICE_ON_OFF_REPLY:
                            Marshal.Copy(message, 0, Device_State_ReplyPtr, message.Length );
                            Device_State_Reply = (MSG_O2H_Device_State_Reply)Marshal.PtrToStructure(Device_State_ReplyPtr, typeof(MSG_O2H_Device_State_Reply));
                            label1.Text = String.Format("Device {0}, Status {1}", (ASIOMessages.ASIOMessages.eDevices)Device_State_Reply.device, (ASIOMessages.ASIOMessages.eOnOff)Device_State_Reply.onoff);
                            PrintMsgCont(Device_State_Reply);
                       

                            break;

                        case ASIOMessages.ASIOMessages.Opcode_O2H.ATTITUDE_QUATERNIONS:

                            Marshal.Copy(message, 0, Attitide_Quaternion_EstimatePtr, message.Length);
                            Attitude_Quaternion_Estimate = (MSG_O2H_Attitude_Quaternion_Estimate)Marshal.PtrToStructure(Attitide_Quaternion_EstimatePtr, typeof(MSG_O2H_Attitude_Quaternion_Estimate));
                            PrintMsgCont(Attitude_Quaternion_Estimate);
                            break;

                        case ASIOMessages.ASIOMessages.Opcode_O2H.ATTITUDE_EULERS:
                       
                            Marshal.Copy(message, 0, Attitude_EulersPtr, message.Length);
                            Attitude_Eulers = (MSG_O2H_Attitude_Eulers)Marshal.PtrToStructure(Attitude_EulersPtr, typeof(MSG_O2H_Attitude_Eulers));
                            PrintMsgCont(Attitude_Eulers);
                                                        
                            break;
                        case ASIOMessages.ASIOMessages.Opcode_O2H.POSITION_ESTIMATION:

                            Marshal.Copy(message, 0, Position_EstimatePtr, message.Length);
                            Position_Estimate = (MSG_O2H_Position_Estimate)Marshal.PtrToStructure(Position_EstimatePtr, typeof(MSG_O2H_Position_Estimate));
                            PrintMsgCont(Position_Estimate);
                            break;
                        case ASIOMessages.ASIOMessages.Opcode_O2H.SYNC_TIMESTAMP:

                            Marshal.Copy(message, 0, O2H_TimeStampPtr, message.Length);
                            O2H_TimeStamp = (MSG_O2H_TimeStamp)Marshal.PtrToStructure(O2H_TimeStampPtr, typeof(MSG_O2H_TimeStamp));
                            PrintMsgCont(O2H_TimeStamp);
                            break;

                        case ASIOMessages.ASIOMessages.Opcode_O2H.O2H_RESERVED:

                            Marshal.Copy(message, 0, Reserved_for_TestsPtr, message.Length);
                            Reserved_for_Tests = (MSG_O2H_Reserved_for_Tests)Marshal.PtrToStructure(Reserved_for_TestsPtr, typeof(MSG_O2H_Reserved_for_Tests));

                            if (csv_write_cnt % 40 == 0)
                                PrintMsgContEulers(Reserved_for_Tests);

                            //saves incomming msgs to csv file
                        
                            if (continous_data_test_on)
                            {
                                csv_write_cnt++;
                                string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19}",
                                    csv_write_cnt, //{0}
                                    Reserved_for_Tests.ts_snt_from_tiva,//{1}
                                    Reserved_for_Tests.ts_rcvd_from_sensor, //{2}
                                    Reserved_for_Tests.ts_went_from_sensor,//{3}
                                    Reserved_for_Tests.gx, Reserved_for_Tests.gy, Reserved_for_Tests.gz,//{4,5,6}
                                    Reserved_for_Tests.ax, Reserved_for_Tests.ay, Reserved_for_Tests.az,//{7,8,9}
                                    Reserved_for_Tests.q1, Reserved_for_Tests.q2, Reserved_for_Tests.q3, Reserved_for_Tests.q4, //{10,11,12,13}
                                    Reserved_for_Tests.ex, Reserved_for_Tests.ey, Reserved_for_Tests.ez, //{14,15,16}
                                    Reserved_for_Tests.w_b_x, Reserved_for_Tests.w_b_y, Reserved_for_Tests.w_b_z //{17},{18},{19}


                                    );
                
                             
                                csv_write_h.AppendLine(line);
                                /*
                                tst_arr[tst_indx++] = Reserved_for_Tests.ts_snt_from_tiva;
                                if (tst_indx == tst_total)
                                {
                                    UInt32 diff_avg = 0;
                                    UInt32 diff = 0;
                                    int j;
                                    for (j = 0; j < tst_total; j++)
                                    {
                                        diff = tst_arr[j + 1] - tst_arr[j];
                                        diff_avg += diff;
                                    }

                                    float res = diff_avg / j;
                                    res = res;
                                }*/
                                label1.Text = String.Format("Line {0}", csv_write_cnt);    
                            }
                            else
                            {
                                
                            }
                           
                            
                            break;
                        default:
                            label1.Text = "unknown";
                            txt_MSG_replies.AppendText("unknown");
                            break;




                            /*
                        case ASIOMessages.ASIOMessages.eOpcode.PING:
                            Marshal.Copy(message, 1, PingPtr, message.Length - 1);
                            PingMessage = (sPingReply)Marshal.PtrToStructure(PingPtr, typeof(sPingReply));
                            label1.Text = String.Format("ID {0},Ver {1}", PingMessage.id,PingMessage.version);
                            break;
                        case ASIOMessages.ASIOMessages.eOpcode.EEPROM_READ:
                            counter = (uint)(message[6] * 0x1000000)+(uint)(message[5] * 0x10000)+(uint)(message[4] * 0x100) + (uint)message[3];
                            FormatEEPROMNumber();
                            break;
                        case ASIOMessages.ASIOMessages.eOpcode.SYSTEM_STATE_GET_PUT:
                            Marshal.Copy(message,1,SystemStatePtr,message.Length - 1);
                            SystemStateMessage = (sSystemState)Marshal.PtrToStructure(SystemStatePtr, typeof(sSystemState));
                            StatusLabel.Text = String.Format("{0}\n{1}\n{2}",
                                ((ASIOMessages.ASIOMessages.eSystemStatus)SystemStateMessage.System).ToString(),
                                ((ASIOMessages.ASIOMessages.eLaserStatus)SystemStateMessage.Laser).ToString(),
                                ((ASIOMessages.ASIOMessages.eNavigationStatus)SystemStateMessage.Navigation).ToString());
                            break;
                        case ASIOMessages.ASIOMessages.eOpcode.ADIS16480_DATA:
                            Marshal.Copy(message, 1, ADISDataPtr, message.Length - 1);
                            AdisMessage = (sADISdata)Marshal.PtrToStructure(ADISDataPtr, typeof(sADISdata));
                            gyrolabel.Text = String.Format("GYRO {0},{1},{2}", AdisMessage.gx, AdisMessage.gy, AdisMessage.gz);
                            accellabel.Text = String.Format("ACCEL {0},{1},{2}", AdisMessage.ax, AdisMessage.ay, AdisMessage.az);
                            maglabel.Text = String.Format("MAG {0},{1},{2}", AdisMessage.mx, AdisMessage.my, AdisMessage.mz);
                            barolabel.Text = String.Format("{0} µbar", AdisMessage.baro*40);
                            temperaturelabel.Text = String.Format("{0} °C", (25.0 + AdisMessage.temp * 0.00565));
                            break;
                        case ASIOMessages.ASIOMessages.eOpcode.UBLOX_FIRST:
                            // ublox messages may be > 255 bytes so they are sent in slices, 1 UBLOX_FIRST then 0 to N
                            // UBLOX_CONTINUE. The first block in the sequence < 255 byes long is the last block 
                            // assign space for the complete UBLOX block, assuming that the next bytes are a header
                            Marshal.Copy(message,1,uboxHeaderPtr,6); //System.Runtime.InteropServices.Marshal.SizeOf(sUBXHeader)
                            ubloxHeader = (sUBXHeader)Marshal.PtrToStructure(uboxHeaderPtr, typeof(sUBXHeader));
                            UBLOXAny = new byte[ubloxHeader.length + 6];    // allow for header
                            Array.Copy(message, 1, UBLOXAny, 0, message.Length - 1);
                            if (ubloxHeader.length < (255-6))  // maxlength - headerlength
                                UBLOXAnalyse(UBLOXAny);
                            else
                                UBLOXAnyIndex = message.Length - 1;
                            break;
                        case ASIOMessages.ASIOMessages.eOpcode.UBLOX_CONTINUE:
                            Array.Copy(message, 1, UBLOXAny, UBLOXAnyIndex, message.Length - 1);
                            if (message.Length - 1 < 255)
                                UBLOXAnalyse(UBLOXAny);
                            else
                                UBLOXAnyIndex += message.Length - 1;
                            break;
                        case ASIOMessages.ASIOMessages.eOpcode.ADIS16480_ANY_REGISTER:
                            Marshal.Copy(message, 1, ADISregsPtr, 3);
                            ADISregisters = (sADISregisters)Marshal.PtrToStructure(ADISregsPtr, typeof(sADISregisters));
                            // extent is the number of 16 bit words that are active, just copy that bit to a word array
                            ushort[] data = new ushort[ADISregisters.extent];
                           // Array.Copy(message, 4, data, 0, data.Length);
                           // data = BitConverter.ToInt16(message,4)
                            Buffer.BlockCopy(message, 4, data, 0, data.Length*2);
                            ADISAnalyse(ADISregisters.page, ADISregisters.first_register, data);
                            break;
                        default:
                            label1.Text = "unknown";
                            break;
                            */
                    }
                }
            }
        }
        public Form1()
        {
            InitializeComponent();
            ReliableUart = new HDLCClass();
            ReliableUart.HDLCInit(1000);
            // initialize listboc
            string[] comports = SerialPort.GetPortNames();
            foreach (string s in comports)
                listBox_coms.Items.Add(s);
            // select the last one
            listBox_coms.SelectedIndex = listBox_coms.Items.Count - 1;
            // setup serial port
            sp = new SerialPort("blah", 115200, Parity.None, 8, StopBits.One);
            // setup handler for hdlc data
            HdlcHandle = new hdlchandler(HdlcHandler);
            //           ticktock = new System.Windows.Forms.Timer();
            // set 1Hz rate
            //           ticktock.Interval = 1000;
            //           ticktock.Tick += ticktock_Tick;
            //          ticktock.Start();
            // allocate static areas for structures

            //ASIO2
            //H2O
            SetDeviceStatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(SetDeviceState));
            GetDeviceStatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(GetDeviceState));
            AttitudeFeedbackPtr = Marshal.AllocHGlobal(Marshal.SizeOf(AttitudeFeedback));
            PositionFeedbackPtr = Marshal.AllocHGlobal(Marshal.SizeOf(PositionFeedback));
            TimeStampPtr = Marshal.AllocHGlobal(Marshal.SizeOf(TimeStamp));

            //O2H
            Ping_ReplyPtr = Marshal.AllocHGlobal(Marshal.SizeOf(Ping_Reply));
            Device_State_ReplyPtr = Marshal.AllocHGlobal(Marshal.SizeOf(Device_State_Reply));
            O2H_TimeStampPtr = Marshal.AllocHGlobal(Marshal.SizeOf(O2H_TimeStamp));
            Attitide_Quaternion_EstimatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(Attitude_Quaternion_Estimate));
            Attitude_EulersPtr = Marshal.AllocHGlobal(Marshal.SizeOf(Attitude_Eulers));
            Position_EstimatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(Position_Estimate));
            Reserved_for_TestsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(Reserved_for_Tests));

  


            //ASIO2 end


            PosLLHPtr = Marshal.AllocHGlobal(Marshal.SizeOf(PosLLHMessage));
            PingPtr = Marshal.AllocHGlobal(Marshal.SizeOf(PingMessage));
            EEPROMPtr = Marshal.AllocHGlobal(Marshal.SizeOf(EEPROMMessage));
            EEPROMPayloadPtr = Marshal.AllocHGlobal(Marshal.SizeOf(EEPROMPayloadMessage));
            SystemStatePtr = Marshal.AllocHGlobal(Marshal.SizeOf(SystemStateMessage));
            ADISDataPtr = Marshal.AllocHGlobal(Marshal.SizeOf(AdisMessage));
            TimeUTCPtr = Marshal.AllocHGlobal(Marshal.SizeOf(TimeUTCMessage));
            uboxHeaderPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ubloxHeader));
            NAVSATPtr = Marshal.AllocHGlobal(Marshal.SizeOf(NAVSATmessage));
    //        ADISregisters.data = new byte[MAX_DATA_PER_ADIS_REGS_BLOCK];
            ADISregsPtr = Marshal.AllocHGlobal(Marshal.SizeOf(ADISregisters));

            // populate UBX box
            foreach (sUBX s in UBXValues)
                UBXcomboBox.Items.Add(s.text);
            UBXcomboBox.SelectedIndex = 0;
            // populate the EEPROM item combobox
            for (int i = (int)ASIOMessages.EEPROM.eEEPROMIndex.BOOT_KEY; i < (int)ASIOMessages.EEPROM.eEEPROMIndex.END_MARKER; i++)
                EEPROMcomboBox.Items.Add(((ASIOMessages.EEPROM.eEEPROMIndex)i).ToString());
            EEPROMcomboBox.SelectedIndex = 0;


            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            btn_data_tst_stop_Click(null, null);

        }

        void ticktock_Tick(object sender, EventArgs e)
        {
 	       // throw new NotImplementedException();
            // send a new PING message
         //   sp.Write(PingWorkArea, 0, (int)pinglength);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (sp == null || !sp.IsOpen)
            {
                try
                {
                    //sp.PortName = "COM40"; 
                    sp.PortName = listBox_coms.SelectedItem.ToString(); //canceled by alexey cos making problems
                    sp.DataReceived += sp_DataReceived;
                    sp.Open();
                    button1.Text = "Disconnect";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
      
            }
            else
            {
                try
                {
                    sp.DataReceived -= sp_DataReceived;
                    sp.Close();
                    button1.Text = "Connect";
                }
              
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
     //       throw new NotImplementedException();
            int size = sp.BytesToRead;
            byte[] data = new byte[size];
            sp.Read(data, 0, size);
            // invoke handler with the data
            this.Invoke(HdlcHandle, new object[] {data });
        }
        private void PingButton_Click(object sender, EventArgs e)
        {
            label1.Text = String.Empty;
            SingleByteMessage[0] = (byte)ASIOMessages.ASIOMessages.eOpcode.PING;
            uint msglength = ReliableUart.HDLCStuff(SingleByteMessage, ref GeneralWorkArea);
            sp.Write(GeneralWorkArea, 0, (int)msglength);
        }
        private void EEPROMButton_Click(object sender, EventArgs e)
        {
            if (EEPROMReadradioButton.Checked)
            {
                EEPROMPayloadMessage.opcode = (byte)ASIOMessages.ASIOMessages.eOpcode.EEPROM_READ;
//                EEPROMPayloadMessage.eeprom.DWORDindex = (UInt16)EEPROMOffsetupdown.Value;
                EEPROMPayloadMessage.eeprom.DWORDindex = (UInt16)(ASIOMessages.EEPROM.eEEPROMIndex)(Enum.Parse(typeof(ASIOMessages.EEPROM.eEEPROMIndex), EEPROMcomboBox.SelectedItem.ToString()));
                EEPROMtextBox.Text = String.Empty;
            }
            else if (EEPROMWriteradioButton.Checked)
            {
                EEPROMPayloadMessage.opcode = (byte)ASIOMessages.ASIOMessages.eOpcode.EEPROM_WRITE;
      //          EEPROMPayloadMessage.eeprom.DWORDindex = 0; // (UInt16)EEPROMOffsetupdown.Value;
                EEPROMPayloadMessage.eeprom.DWORDindex = (UInt16)(ASIOMessages.EEPROM.eEEPROMIndex)(Enum.Parse(typeof(ASIOMessages.EEPROM.eEEPROMIndex), EEPROMcomboBox.SelectedItem.ToString()));
                if (EEPROMtextBox.Text.Substring(0, 2).Equals("0X", StringComparison.CurrentCultureIgnoreCase))
                    EEPROMPayloadMessage.eeprom.data = Convert.ToUInt32(EEPROMtextBox.Text, 16);
                else
                    EEPROMPayloadMessage.eeprom.data = UInt32.Parse(EEPROMtextBox.Text);
            }
            byte[] payload = new byte[Marshal.SizeOf(EEPROMPayloadMessage)];
            Marshal.StructureToPtr(EEPROMPayloadMessage, EEPROMPayloadPtr, true);
            Marshal.Copy(EEPROMPayloadPtr, payload,0, payload.Length);
            uint msglength = ReliableUart.HDLCStuff(payload, ref GeneralWorkArea);
            sp.Write(GeneralWorkArea, 0, (int)msglength);
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            /* send a polling message back to the Tiva */
            // find relevant code in ubxmessages
            byte code = 0;
            foreach (sUBX s in UBXValues)
            {
                if (s.text.Equals(UBXcomboBox.SelectedItem.ToString()))
                {
                    code = s.code;
                    break;
                }
            }
            // create a message & send back
            byte[] message = new byte[] { (byte)ASIOMessages.ASIOMessages.eOpcode.UBX_POLL, 0x01, code };
            uint msglength = ReliableUart.HDLCStuff(message, ref GeneralWorkArea);
            sp.Write(GeneralWorkArea, 0, (int)msglength);
        }
        void FormatEEPROMNumber()
        {
            if (DecimalButton.Checked)
                EEPROMtextBox.Text = counter.ToString();
            else
                EEPROMtextBox.Text = "0X" + counter.ToString("X8");
        }
        private void DecimalButton_CheckedChanged(object sender, EventArgs e)
        {
            FormatEEPROMNumber();
        }
        private void HexButton_CheckedChanged(object sender, EventArgs e)
        {
            FormatEEPROMNumber();
        }
        void UBLOXAnalyse(byte[] ublox)
        {
            // look at the header
            switch (ubloxHeader.uclass)
            {
                case UBLOX.uClass.C_NAV:
                    switch (ubloxHeader.id)
                    {
                        case UBLOX.uID.TIMEUTC:
//                            Marshal.Copy(ublox, 6, TimeUTCPtr, sizeof(sTimeUTC));
                            Marshal.Copy(ublox, 6, TimeUTCPtr, System.Runtime.InteropServices.Marshal.SizeOf(TimeUTCMessage));
                            TimeUTCMessage = (sUBX_NAV_TIMEUTC)Marshal.PtrToStructure(TimeUTCPtr, typeof(sUBX_NAV_TIMEUTC));
                            UpdateLabel ul2 = new UpdateLabel(updatelabel);
                            this.Invoke(ul2, new object [] {TimeUtclabel,
                                String.Format("UTC {0}/{1}/{2} {3}:{4}:{5}",
                                TimeUTCMessage.day,TimeUTCMessage.month,TimeUTCMessage.year,
                                TimeUTCMessage.hour, TimeUTCMessage.minute, TimeUTCMessage.second)});
                            break;
                        case UBLOX.uID.POSLLH:
                            Marshal.Copy(ublox, 6, PosLLHPtr, System.Runtime.InteropServices.Marshal.SizeOf(PosLLHMessage));
                            PosLLHMessage = (sUBX_NAV_POSLLH)Marshal.PtrToStructure(PosLLHPtr, typeof(sUBX_NAV_POSLLH));
                            UpdateLabel ul = new UpdateLabel(updatelabel);
                            this.Invoke(ul, new object[] { GPSlabel,
                                    String.Format("Long {0} Lat {1} Elev {2}",
                                        PosLLHMessage.lon/10000000.0,PosLLHMessage.lat/10000000.0,PosLLHMessage.height/1000)});
                            break;
                        case UBLOX.uID.SAT:
                            Marshal.Copy(ublox, 6, NAVSATPtr, System.Runtime.InteropServices.Marshal.SizeOf(NAVSATmessage));
                            NAVSATmessage = (sUBX_NAV_SAT)Marshal.PtrToStructure(NAVSATPtr, typeof(sUBX_NAV_SAT));
                            UpdateLabel ul3 = new UpdateLabel(updatelabel);
                            this.Invoke(ul3, new object[] { AnyUBXlabel,
                                    String.Format("SAT Numsv {0}",NAVSATmessage.numSVs)});
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        void ADISAnalyse(byte page, byte firstReg, ushort[] data)
        {
            int i,dataindex=0;
            Int16[] tempvalues = new Int16[3];
            switch (page)
            {
                case 0:
                    for (i = firstReg; i < firstReg + (data.Length*2); i+=2)  // register number goes up in 2's
                    {
                        switch (i)
                        {
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.TEMP_OUT:
                                temperaturelabel.Text = String.Format("{0} °C", (25.0 + (short)data[dataindex++] * 0.00565));
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.BAROM_OUT:
                                barolabel.Text = String.Format("{0} µbar", (short)data[dataindex++] * 40);
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.X_GYRO_OUT:
                                tempvalues[0] = (short)data[dataindex++];
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Y_GYRO_OUT:
                                tempvalues[1] = (short)data[dataindex++];
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Z_GYRO_OUT:
                                tempvalues[2] = (short)data[dataindex++];
                                gyrolabel.Text = String.Format("GYRO {0},{1},{2}", tempvalues[0], tempvalues[1], tempvalues[2]);
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.X_ACCEL_OUT:
                                tempvalues[0] = (short)data[dataindex++];
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Y_ACCEL_OUT:
                                tempvalues[1] = (short)data[dataindex++];
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Z_ACCEL_OUT:
                                tempvalues[2] = (short)data[dataindex++];
                                accellabel.Text = String.Format("ACCEL {0},{1},{2}", tempvalues[0], tempvalues[1], tempvalues[2]);
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.X_MAG_OUT:
                                tempvalues[0] = (short)data[dataindex++];
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Y_MAG_OUT:
                                tempvalues[1] = (short)data[dataindex++];
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Z_MAG_OUT:
                                tempvalues[2] = (short)data[dataindex++];
                                maglabel.Text = String.Format("MAG {0},{1},{2}", tempvalues[0], tempvalues[1], tempvalues[2]);
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.X_ACCEL_LOW:
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.X_GYRO_LOW:
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Y_ACCEL_LOW:
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Y_GYRO_LOW:
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Z_ACCEL_LOW:
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.Z_GYRO_LOW:
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.BAROM_LOW:
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.PITCH_C31_OUT:
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.YAW_C32_OUT:
                                dataindex++;
                                break;
                            case (int)ASIOMessages.ASIORegisters.eASIOReg.ROLL_C23_OUT:
                                tempvalues[0] = (short)data[dataindex++];
                                Rolllabel.Text = String.Format("Roll {0}", tempvalues[0]);
                                break;
                            default:
                                dataindex++;  // should never get here
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        private void H2O_Send(IntPtr DataPtr, int DataSize)
        {
            byte[] payload = new byte[DataSize];
            


            Marshal.Copy(DataPtr, payload, 0, payload.Length);
            uint msglength = ReliableUart.HDLCStuff(payload, ref GeneralWorkArea);
            sp.Write(GeneralWorkArea, 0, (int)msglength);

        }
        private void btn_GetStatus_Click(object sender, EventArgs e)
        {

            GetDeviceState.opcode = (byte)ASIOMessages.ASIOMessages.Opcode_H2O.GET_DEVICE_ON_OFF;
            GetDeviceState.device = (byte)ASIOMessages.ASIOMessages.eDevices.CDC_SERIAL;

            Marshal.StructureToPtr(GetDeviceState, GetDeviceStatePtr, true);
            H2O_Send(GetDeviceStatePtr, Marshal.SizeOf(GetDeviceState));


        }

        private void btn_SetStatus_Click(object sender, EventArgs e)
        {
            SetDeviceState.opcode = (byte)ASIOMessages.ASIOMessages.Opcode_H2O.SET_DEVICE_ON_OFF;
            SetDeviceState.onoff = (byte)ASIOMessages.ASIOMessages.eOnOff.OFF;
            SetDeviceState.device = (byte)ASIOMessages.ASIOMessages.eDevices.CDC_SERIAL;

            Marshal.StructureToPtr(SetDeviceState, SetDeviceStatePtr, true);
            H2O_Send(SetDeviceStatePtr, Marshal.SizeOf(SetDeviceState));

        }

        private void btn_Attitude_Feedback_Click(object sender, EventArgs e)
        {
            AttitudeFeedback.opcode = (byte)ASIOMessages.ASIOMessages.Opcode_H2O.ATTITUDE_FEEDBACK;
            AttitudeFeedback.Device = 1;
            AttitudeFeedback.Q1 = 2;
            AttitudeFeedback.Q2 = 3;
            AttitudeFeedback.Q3 = 3.5;
            AttitudeFeedback.Q4 = 4;
            AttitudeFeedback.Q1_rate = 5.23332;
            AttitudeFeedback.Q2_rate = 6;
            AttitudeFeedback.Q3_rate = 7;
            AttitudeFeedback.Q4_rate = 8;
            AttitudeFeedback.score = 9;

            Marshal.StructureToPtr(AttitudeFeedback, AttitudeFeedbackPtr, true);
            H2O_Send(AttitudeFeedbackPtr, Marshal.SizeOf(AttitudeFeedback));


        }

        private void btn_Position_Feedback_Click(object sender, EventArgs e)
        {
            PositionFeedback.opcode = (byte)ASIOMessages.ASIOMessages.Opcode_H2O.POSITION_FEEDBACK;
            PositionFeedback.latitude = 52;
            PositionFeedback.longitude = 13;
            PositionFeedback.score = 3;
            PositionFeedback.timestamp = 123456;
            PositionFeedback.AltitudeElipsoid = 9875;

            Marshal.StructureToPtr(PositionFeedback, PositionFeedbackPtr, true);
            H2O_Send(PositionFeedbackPtr, Marshal.SizeOf(PositionFeedback));
        }

        private void btn_TimeStamp_Click(object sender, EventArgs e)
        {
            //no such msg
            //O2H_TimeStamp.opcode = (byte)ASIOMessages.ASIOMessages.Opcode_H2O.
        }

        private void PrintMsgCont(Object myObj)
        {
            txt_MSG_replies.AppendText(myObj.ToString() + " - ");


            foreach (var prop in myObj.GetType().GetProperties())
            {
                txt_MSG_replies.AppendText(prop.Name + ": " + prop.GetValue(myObj, null));
            }

            foreach (var field in myObj.GetType().GetFields())
            {
                txt_MSG_replies.AppendText(field.Name + ": " + field.GetValue(myObj) + " ");
            }

            txt_MSG_replies.AppendText("\n");
            txt_MSG_replies.ScrollToCaret();

            /*
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(obj);
                string final = String.Format("{0}={1}", name, value);
                txt_MSG_replies.AppendText(final);

            }
             */
        }


        private void PrintMsgContEulers(MSG_O2H_Reserved_for_Tests myObj)
        {

            txt_MSG_replies.AppendText("Ex: " + myObj.ex + " Ey: " + myObj.ey + " Ez: " + myObj.ez + "\n");
            txt_MSG_replies.ScrollToCaret();

        }
        private void btn_data_tst_Click(object sender, EventArgs e)
        {
            if (continous_data_test_on == true)
                return;

            try
            {
                string line = "Cnt, TStxT, TSrxT, TStxE, Gx, Gy, Gz, Ax, Ay, Az, Q1, Q2, Q3, Q4, Ex, Ey, Ez, WBx, WBy, WBz";
                         //count, timestamp when sent from tiva, timestamp when received at tiva from epson, timetamp internal epson, gyro, accel, queterion
                csv_write_h.AppendLine(line);
               
                SingleByteMessage[0] = (byte)ASIOMessages.ASIOMessages.Opcode_H2O.H2O_RESERVED;
                continous_data_test_on = true;

                uint msglength = ReliableUart.HDLCStuff(SingleByteMessage, ref GeneralWorkArea);
                sp.Write(GeneralWorkArea, 0, (int)msglength);

                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btn_data_tst_stop_Click(object sender, EventArgs e)
        {
            if (continous_data_test_on == false)
                return;

            string filename = "tiva_tst_rt_eulers_.csv";
            try
            {
                SingleByteMessage[0] = (byte)ASIOMessages.ASIOMessages.Opcode_H2O.H2O_RESERVED;
                continous_data_test_on = false;
                uint msglength = ReliableUart.HDLCStuff(SingleByteMessage, ref GeneralWorkArea);
                sp.Write(GeneralWorkArea, 0, (int)msglength);

                File.WriteAllText(filename, csv_write_h.ToString());

                csv_write_cnt = 0;
                csv_write_h.Clear();
                txt_MSG_replies.Clear();
                
            }
            catch (IOException ex)
            {
                MessageBox.Show("Problem with the file: " + ex.ToString());
                return;
            }


            MessageBox.Show("Test is complete, the file has been saved under the name " + filename);
        }

        
    }
}
