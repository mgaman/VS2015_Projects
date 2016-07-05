// PinPrime.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <Windows.h>
#include <stdint.h>
#include <cstddef>
#include "Prime.h"

HANDLE comport;
DCB dcb;
DWORD byteswritten, bytesread;

uint16_t wordswap(uint16_t data)
{
	return (data >> 8) + ((data & 0xff) << 8);
}

// function to calculate CRC-16
// returns LITTLE-ENDIAN
uint16_t CRC(void * data, uint32_t len)
{
	uint8_t * dataPtr = (uint8_t *)data;
	uint32_t index = 0;
	// Update the CRC for transmitted and received data using
	// the CCITT 16bit algorithm (X^16 + X^12 + X^5 + 1).
	uint16_t crc = 0;
	while (len--)
	{
		crc = (unsigned char)(crc >> 8) | (crc << 8);
		crc ^= dataPtr[index++];
		crc ^= (unsigned char)(crc & 0xff) >> 4;
		crc ^= (crc << 8) << 4;
		crc ^= ((crc & 0xff) << 4) << 1;
	}
	return crc;
}

/*
   Scan packet for length, content, calculate checksum
   return true if OK, else false
*/
bool verifyPacket(uint8_t *in)
{
	bool rc =  false;
	uint16_t length = wordswap(*(uint16_t *)in);
	uint16_t crc = CRC(in, length - 2);	// calculate CEC excluding swap
	return  wordswap(*(uint16_t *)&in[length - 2]) == crc;
}
/*
   Pack a message by adding 2 length bytes before and 2 CRC bytes after
*/
int packmessage(uint8_t *in, uint16_t length, uint8_t *out)
{
	// swap length endian & copy to target
	uint16_t ll = wordswap(length + 4);
	memcpy(out, &ll, 2);
	// copy message
	memcpy(&out[2], in, length);
	// compute checksum including length bytes
	uint16_t crc = wordswap(CRC(out, length + 2));
	memcpy(&out[length+2], &crc, 2);
	return length + 4;
}

uint8_t single[1] = { kGetModInfo };
uint8_t output[50], inputbuffer[100];
uint8_t modeinfo[] = { 0x00, 0x0D, 0x02, 0x50, 0x52, 0x4D, 0x45, 0x30, 0x31, 0x30, 0x39, 0x65, 0xB3};
char *menu = "Menu\n"
"1 Modeinfo\n"
"4 Getdata\n"
"q quit\n";
char readbuf[30] = { 0 };
int main()
{
	int outlength;
	comport = CreateFile(TEXT("COM1"), GENERIC_READ | GENERIC_WRITE, 0, NULL, OPEN_EXISTING, 0, NULL);
	if (!GetCommState(comport, &dcb))
		return 1;
	dcb.BaudRate = CBR_57600; //9600 Baud
	dcb.ByteSize = 8; //8 data bits
	dcb.Parity = NOPARITY; //no parity
	dcb.StopBits = ONESTOPBIT; //1 stop
	if (!SetCommState(comport, &dcb))
		return 1;
	#if 0
	outlength = packmessage(single, 1, output);
	single[0] = kGetData;
	outlength = packmessage(single, 1, output);
	if (verifyPacket(modeinfo))
	{
		switch (modeinfo[2])
		{
		case kModInfoResp:
			modeinfo[modeinfo[1] - 2] = 0; // add string marker
			printf("mode %s\n", &modeinfo[3]);
			break;
		default:
			printf("unknown\n");
			break;
		}
	}
	#endif
	puts(menu);
	while (*readbuf != 'q')
	{
		fgets(readbuf, 30, stdin);
		switch (readbuf[0])
		{
		case '1':
			single[0] = { kGetModInfo };
			outlength = packmessage(single, 1, output);
			WriteFile(comport, output, outlength, &byteswritten, NULL);
			break;
		case '4':
			single[0] = { kGetData };
			outlength = packmessage(single, 1, output);
			WriteFile(comport, output, outlength, &byteswritten, NULL);
			break;
		case 'q':
			CloseHandle(comport); //close the handle
			return 0;
			break;
		default:
			break;
		}
		ReadFile(comport, inputbuffer, 100, &bytesread, NULL);
		if (verifyPacket(inputbuffer))
		{
			switch (inputbuffer[2])
			{
			case kModInfoResp:
				modeinfo[inputbuffer[1] - 2] = 0; // add string marker
				printf("mode %s\n", &modeinfo[3]);
				break;
			default:
				printf("unknown\n");
				break;
			}

		}
	}
	return 0;
}

