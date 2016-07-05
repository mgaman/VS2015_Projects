using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIOMessages
{
    public class ASIOMessages
    {
        // opcodes
        public enum eOpcode : byte { PING = 0x11, SYSTEM_STATE_GET_PUT, EEPROM_READ, EEPROM_WRITE, UBX_POLL, UBLOX_FIRST, UBLOX_CONTINUE, ADIS16480_DATA, ADIS16480_ANY_REGISTER };
        // enums
        public enum eSystemStatus: byte {SYSTEM_OFF = 0x11,SYSTEM_OPERATIONAL};
        public enum eLaserStatus : byte { LASER_STANDBY = 0x21 ,LASER_LASER};
        public enum eNavigationStatus : byte {NAVIGATION_OFF = 0x31,NAVIGATION_CAL,NAVIGATION_ZERO,NAVIGATION_OPERATIONAL};


        public enum  Opcode_O2H : byte//OBOX to Host opcode
        {

	        PING_REPLY = 0x12,
            DEVICE_ON_OFF_REPLY = 0x22,
	        ATTITUDE_QUATERNIONS,
	        ATTITUDE_EULERS = 0x25,
	        POSITION_ESTIMATION,
	        SYNC_TIMESTAMP = 0x28,

            O2H_RESERVED  //these field is for development purposes

        };


        public enum Opcode_H2O : byte//OBOX to Host opcode
        {

            PING_REQUEST = 0x11,
            SET_DEVICE_ON_OFF = 0x20,
            GET_DEVICE_ON_OFF,
            ATTITUDE_FEEDBACK = 0x24,
            POSITION_FEEDBACK = 0x27,

            H2O_RESERVED  //these field is for development purposes

        };

        public enum Nav_Stat_Vals : byte//OBOX to Host opcode
        {

            NOFIX = 0,
            DEADRECKONING,
            STANDALONE_2D,
            STANDALONE_3D,
            GPS_PLUSDR,
            TIME_ONLY,
            DIFFERENTIAL_2D,
            DIFFERENTIAL_3D

        };

        public enum eDevices
        {

            UBLOX = 0x66,		//GPS = 0x66,
            NIGHT_CAMERA,
            DAY_CAMERA,
            LASER,
            MOTHER_BOARD,
            EPSON_V340, 		//IMU
            PNI_PRIME, 			//COMPASS
            JPEG,
            OPTICAL_TRACKER,
            SYSTEM_CLOCK,
            CDC_SERIAL,
            NUM_DEVICES

        };

        public enum eOnOff
        {
            ON = 0x55,
            OFF
        };


    }
}
