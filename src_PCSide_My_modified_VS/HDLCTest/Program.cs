using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDLC;

namespace ReliableUARTTest
{
    class Program
    {
        static bool rc;
        static HDLCClass reliable;
        static byte[] single = new byte[1];
        static byte[] multi = new byte[] { 1, 2, 3, 4, 5 };
        static byte[] multidel = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 8, 7, 6, 5, 4, 3, 2, 1 }; // length is DEL
        static byte[] datadel = new byte[] { 1, 2, 3, 4, 0x10, 6, 7, 8 }; // data contains DEL
        static byte[] workarea = new byte[30];
        static bool compareOldNew(byte[] raw, byte[] work, uint length)
        {
            for (int i = 0; i < length;i++ )
                if (reliable.HDLCParse(work[i]))
                    break;
            byte[] final = reliable.HDLCUnStuff();
            return raw.SequenceEqual(final);
        }
        static void Main(string[] args)
        {
            rc = true;
            reliable = new HDLCClass();
            reliable.HDLCInit(1000);    // more than enough
            single[0] = 0xea;   // typical
            uint length = reliable.HDLCStuff(single, ref workarea);
            Console.WriteLine(compareOldNew(single, workarea, length) ? "pass" : "fail");
            single[0] = 0x10;   // DLE
            length = reliable.HDLCStuff(single, ref workarea);
            Console.WriteLine(compareOldNew(single, workarea, length) ? "pass" : "fail");
            length = reliable.HDLCStuff(multi, ref workarea);
            Console.WriteLine(compareOldNew(multi, workarea, length) ? "pass" : "fail");
            length = reliable.HDLCStuff(multidel, ref workarea);
            Console.WriteLine(compareOldNew(multidel, workarea, length) ? "pass" : "fail");
            length = reliable.HDLCStuff(datadel, ref workarea);
            Console.WriteLine(compareOldNew(datadel, workarea, length) ? "pass" : "fail");
        }
    }
}
