using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIOMessages
{
    public class ASIORegisters
    {
    // page 0
        public enum eASIOReg {PAGE_ID=0,SYS_E_FLAG=0x08,TEMP_OUT=0x0e,X_GYRO_LOW=0X10,X_GYRO_OUT=0X12,Y_GYRO_LOW=0X14,Y_GYRO_OUT=0X16,Z_GYRO_LOW=0X18,Z_GYRO_OUT=0X1A,X_ACCEL_LOW=0X1C,
            X_ACCEL_OUT=0X1E,Y_ACCEL_LOW=0X20,Y_ACCEL_OUT=0X22,Z_ACCEL_LOW=0X24,Z_ACCEL_OUT=0X26,X_MAG_OUT=0x28,Y_MAG_OUT=0x2A,Z_MAG_OUT=0x2C,BAROM_LOW=0x2e,BAROM_OUT=0x30,
            ROLL_C23_OUT = 0x6a, PITCH_C31_OUT = 0x6c, YAW_C32_OUT=0x6e,PROD_ID = 0x7E
        };
    }
}
