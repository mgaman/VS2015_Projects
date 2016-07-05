using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ASIO2Messages
{
    public class UBLOX
    {
        // ublox classes and id's
        public enum uClass : byte { C_NAV = 0x01 };
        public enum uID : byte { POSLLH = 0x02, STATUS = 0x03, TIMEUTC = 0X21, ORB = 0x34, DOP = 0x04, SOL = 0x06, PVT=0x07, SAT = 0x35 };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sNavSol
        {
            public UInt32 itow;
            public Int32 ftow;
            public Int16 week;
            public byte gpsFix;
            public byte flags;
            public Int32 ecefX;
            public Int32 ecefY;
            public Int32 ecefZ;
            public Int32 pacc;
            public Int32 ecefVX;
            public Int32 ecefVY;
            public Int32 ecefVZ;
            public UInt32 sacc;
            public UInt16 pdop;
            byte rsv;
            public byte numSV;
            UInt32 rsv2;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sNavPVT
        {
            public UInt32 itow;
            public UInt16 year;
            public byte month;
            public byte day;
            public byte hour;
            public byte minute;
            public byte second;
            public byte valid;
            public UInt32 tAcc;
            public Int32 nano;
            public byte fixtype;
            public byte flags;
            public byte rsv1;
            public byte numSV;
            public Int32 lon;
            public Int32 lat;
            public Int32 height;
            public Int32 hMSL;
            public UInt32 hACC;
            public UInt32 VACC;
            public Int32 velN;
            public Int32 velE;
            public Int32 velD;
            public Int32 gspeed;
            public Int32 headMot;
            public UInt32 sAcc;
            public UInt32 headAcc;
            public Int16 pDOP;
            public UInt32 rsv2;
            public Int16 rsv3;
            public Int32 headVwh;
            public Int32 rsv4;
        };
    }

    public class ASIOMessages
    {
        public enum eOpcode : byte
        {
            PING = 0x11, SYSTEM_STATE_GET_PUT, EEPROM_READ, EEPROM_WRITE, UBX_POLL, UBLOX_FIRST, UBLOX_CONTINUE,
            SET_DEVICE_ON_OFF = 0x20, GET_DEVICE_ON_OFF, SINGLE_DEVICE_ON_OFF, ATTITUDE_QUATERNIONS,
            ATTITUDE_EULERS, ATTITUDE_FEEDBACK, POSITION_ESTIMATION, POSITION_FEEDBACK, SYNC_TIMESTAMP,
            ENTER_BRIDGE_MODE = 0X30, DISCONNECT_CDC, DEBUG_MESSAGE
        };
        public enum eOnOff : byte { ON = 0x55, OFF };
        public enum eDevices : byte
        {
            FIRST_DEVICE, GPS, NIGHT_CAMERA, DAY_CAMERA, LASER, MOTHER_BOARD, CDC_SERIAL,
            EPSON_IMU, PNI_PRIME, JPEG, OPTICAL_TRACKER,
            SYSTEMCLOCK, NUM_DEVICES
        };
        // index of DWORD usage in EEPROM
        public enum eEEPROMIndex
        {
            BOOT_KEY = 0, ID, UBX_RATE, // measurement period in millisec  e.g. 10 Hz is 100 millisec
                                        // up to 15 ublox messages & rates
            UBX_MSG1, UBX_MSG2, UBX_MSG3, UBX_MSG4, UBX_MSG5, UBX_MSG6, UBX_MSG7, UBX_MSG8, UBX_MSG9, UBX_MSG10,
            UBX_MSG11, UBX_MSG12, UBX_MSG13, UBX_MSG14, UBX_MSG15,
            // GPS interfaces
            UBLOX_BAUDRATE,
            // MV430 stuff
            MV430_BAUDRATE, MV430_SPS, MV430_TAP,
            // PNI stuff
            PNI_BAUDRATE, PNI_DECLINATION, PNI_CAL_NUM_POINTS, PNI_MOUNT_REF, PNI_TRUE_NORTH, PNI_CAL_STABLE_CHECK,
            PNI_CAL_AUTO_SAMPLE, PNI_CALIB_1, PNI_CALIB_2, PNI_CALIB_3,
            // button stuff
            SHORT_CLICK_TIME, LONG_CLICK_TIME, DOUBLE_CLICK_TIME,
            TERMINAL_BAUDRATE, Q1 = 50, Q2, Q3, Q4, Q1rate, Q2rate, Q3rate, Q4rate,END_MARKER
        };
        public enum eNavState { NOFIX = 0, DEADRECKONING, STANDALONE2D, STANDALONE3D, GPSPLUSDR, TIMEONLY, DIFFERENTIAL2D, DIFFERENTIAL3D };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sPingReplyPayload
        {
            public byte id;
            public UInt16 version;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sEEPROMValuePayload
        {
            public UInt16 DWORDindex;
            public UInt32 data;  // data can be 32 bit integer or single precision floating point
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sEEPROMValue
        {
            public byte opcode;
            public sEEPROMValuePayload eeprom;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sEnterBridgeMode
        {
            public eOpcode opcode;
            public eDevices device;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sAttitude_EulersPayload
        {
            public UInt32 timestamp;
            public float Yaw;
            public float Pitch;
            public float Roll;       // yaw, pitch, roll
            public float uncertainty;// attitude uncertainty
            public byte status;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sQuaternionPayload
        {
            public UInt32 timestamp;
            public float Q1;
            public float Q2;
            public float Q3;
            public float Q4;
            public float uncertainty;
            public byte status;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sDevicePayload
        {
            public eDevices device;
            public eOnOff onoff;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sGetDevicePayload
        {
            UInt32 timestamp;
            public eDevices device;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sGetDeviceState
        {
            public eOpcode opcode;
            public sGetDevicePayload device;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sSetDevicePayload
        {
            UInt32 timestamp;
            public eDevices device;
            public eOnOff onoff;
        };
        public struct sSetDeviceState
        {
            public eOpcode opcode;
            public sSetDevicePayload device;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sUBXHeader
        {
            byte header1; // 0xb5,0x62
            byte header2;
            public UBLOX.uClass uclass;
            public UBLOX.uID id;
            public UInt16 length;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sAttitude_Feedback
        {
            public eOpcode opcode;
            public UInt32 timestamp;
            public byte Device;
	        public float Q1;
            public float Q2;
            public float Q3;
            public float Q4;
            public float Q1_rate;
            public float Q2_rate;
            public float Q3_rate;
            public float Q4_rate;
            float score;
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sDebugMessage
        {
            public eOpcode opcode;
	        public UInt32 timestamp;
            public UInt32 value1;
            public UInt32 value2;
            public UInt32 value3;
        };
        // See Ublox messages gpsFix definition
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sPositionEstimate
        {
            public eOpcode opcode;
            public UInt32 timestamp;
            public UInt32 UTCtime;
            public Int32 latitude; // degrees * 1E7
            public Int32 longitude;
            public Int32 AltitudeElipsoid; // mm
            public eNavState NavStat;
            public UInt32 hAcc;
            public UInt32 vAcc; // Accuracy estimates mm
            public Int32 SOG, COG; // speed & course over ground mm/hour
            public Int32 vVel; // vertical velocity  m/s
            public Int32 diffAge; // -1 if DPGS not used
            public Int32 HDOP, VDOP, TDOP; // Dilution of Precision * 100
            public byte numSvs;
            public byte DR; // 1 if DR used, else 0
        };
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct sPositionFeedback
        {
            public eOpcode opcode;
            public UInt32 timestamp;
            public Int32 latitude;   // degrees * 1E7
            public Int32 longitude;
            public Int32 AltitudeElipsoid; //mm
            public Int32 score;   // * 1000
        };

    }
}
