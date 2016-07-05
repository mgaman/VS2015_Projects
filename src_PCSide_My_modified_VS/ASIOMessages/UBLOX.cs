using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASIOMessages
{
    public class UBLOX
    {
        // ublox classes and id's
        public enum uClass:byte { C_NAV = 0x01 };
        public enum uID:byte { POSLLH=0x02,STATUS = 0x03,TIMEUTC=0X21,ORB=0x34,DOP=0x04,SAT = 0x35};
    }
}
