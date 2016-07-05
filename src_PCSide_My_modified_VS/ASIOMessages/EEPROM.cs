using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIOMessages
{
    public class EEPROM
    {
        // index of DWORD usage in EEPROM
        public enum eEEPROMIndex {BOOT_KEY=0,ID,YEAR,MONTH,DAY,HOUR,MINUTE,VERSION,
        // button stuff
	            SHORT_CLICK_TIME,LONG_CLICK_TIME,DOUBLE_CLICK_TIME,
        // ublox definitions
	    UBX_RATE_INDEX, // measurement period in millisec  e.g. 10 Hz is 100 millisec
        // up to 15 ublox messages & rates
	    UBX_MSG1,UBX_MSG2,UBX_MSG3,UBX_MSG4,UBX_MSG5,UBX_MSG6,UBX_MSG7,UBX_MSG8,UBX_MSG9,UBX_MSG10,
	    UBX_MSG11,UBX_MSG12,UBX_MSG13,UBX_MSG14,UBX_MSG15,
        //  ADIS16480 configuration and registers to read
	    ADIS_CFG1,ADIS_CFG2,ADIS_CFG3,ADIS_CFG4,ADIS_CFG5,ADIS_CFG6,ADIS_CFG7,ADIS_CFG8,ADIS_CFG9,ADIS_CFG10,
	    ADIS_REG1,ADIS_REG2,ADIS_REG3,ADIS_REG4,ADIS_REG5,ADIS_REG6,ADIS_REG7,ADIS_REG8,ADIS_REG9,ADIS_REG10,
        // UART interfaces
	    UBLOX_BAUDRATE,TERMINAL_BAUDRATE,END_MARKER
        };
    }
}
