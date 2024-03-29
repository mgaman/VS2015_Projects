/*
V2XE code based on:
V2XE Example.c
Created by John Gonzaga on Thu Dec 18 2003.
Copyright (c) 2003 PNI Corporation. All rights reserved.

SPI code based on:
Spi.cpp - SPI library
Copyright (c) 2008 Cam Thompson.
Author: Cam Thompson, Micromega Corporation, <www.micromegacorp.com>
Version: December 15, 2008



#define SCK_PIN   13
#define MISO_PIN  12
#define MOSI_PIN  11
// the compass is always selected, we don't need a /SS pin.
#define SYNC_PIN    10

*/


#define kSyncChar   0xAA
#define kTerminator 0x00

enum
{
	// commands/frame types
	kGetModInfo = 1,    // 0x01
	kModInfoResp,       // 0x02
	kSetDataComponents, // 0x03
	kGetData,           // 0x04
	kDataResp,          // 0x05
	kSetConfig,         // 0x06
	kGetConfig,         // 0x07
	kConfigResp,        // 0x08
	kSaveConfig,        // 0x09
	kStartCal,          // 0x0A
	kStopCal,           // 0x0B
	kGetCalData,        // 0x0C
	kCalDataResp,       // 0x0D
	kSetCalData,        // 0x0E

	// data types
	kRawX = 1,          // 0x01
	kRawY,              // 0x02
	kCalibratedX,       // 0x03
	kCalibratedY,       // 0x04
	kHeading,           // 0x05
	kMagnitude,         // 0x06
	kTemperature,       // 0x07
	kDistortion,        // 0x08
	kCalStatus,         // 0x09

	// config types
	kDeclination = 1,   // 0x01
	kTrueNorth,         // 0x02
	kCalSampleFreq,     // 0x03
	kSampleFreq,        // 0x04
	kPeriod,            // 0x05
	kBigEndian,         // 0x06
	kDampingSize,       // 0x07

	// cal data types
	kXOffset = 1,       // 0x01
	kYOffset,           // 0x02
	kXGain,             // 0x03
	kYGain,             // 0x04
	kPhi,               // 0x05
	kCalMagnitude       // 0x06
};


/* TODO clean this up*/
typedef struct
{
	long x, y;
	float xe, ye;
	float heading, magnitude;
	float temperature;
	byte distortion, calStatus;
} V2XEData;

V2XEData* data;



void init_compass(){
	byte tmp;

	// initialize the SPI pins
	pinMode(SCK_PIN, OUTPUT);
	pinMode(MOSI_PIN, OUTPUT);
	pinMode(MISO_PIN, INPUT);

	pinMode(SYNC_PIN, OUTPUT);
	digitalWrite(SYNC_PIN, LOW);

	// The V2Xe must be talked to at f <= 3.6864 MHz -> FOSC/4
	// enable SPI Master, MSB, SPI mode 0, FOSC/4 (= 2MHz on the 3,3v, 8MHz arduino mini pro)
	mode(2);
	delay(20);
	// sync the compass
	v2xe_sync();
}

void mode(byte config){
	// have a look at the atmega datasheet for meaning of the config parameter.
	byte tmp;

	// enable SPI master with configuration byte specified
	SPCR = 0;
	SPCR = (config & 0x7F) | (1 << SPE) | (1 << MSTR);
	tmp = SPSR;
	tmp = SPDR;
}

void v2xe_sync(){
	digitalWrite(SYNC_PIN, LOW);
	delayMicroseconds(100);
	digitalWrite(SYNC_PIN, HIGH);
	delayMicroseconds(100);
	digitalWrite(SYNC_PIN, LOW);
	delay(20);
}


// low level transmission function
byte spi_transmit(byte value){
	SPDR = value;
	while (!(SPSR & (1 << SPIF)));
	return SPDR;
}

//Higher level transmission functions
//----------------------------------------
byte spi_receive_byte(){
	return spi_transmit(0);
}

//Endinaness s by default the "wrong one" (aka not he one used by the arduino)
unsigned long spi_receive_ulong(){
	unsigned long res = ((unsigned long)spi_transmit(0)) << 24
		| ((unsigned long)spi_transmit(0)) << 16
		| ((unsigned long)spi_transmit(0)) << 8
		| ((unsigned long)spi_transmit(0));
	return res;
}
/*
// TODO revert the endianess of the compass and use this function instead
unsigned long spi_receive_ulong(){
unsigned long res = ((unsigned long)spi_transmit(0))
| ((unsigned long)spi_transmit(0)) << 8
| ((unsigned long)spi_transmit(0)) << 16
| ((unsigned long)spi_transmit(0)) << 24;
return res;
}*/

long spi_receive_long(){
	unsigned long res = spi_receive_ulong();
	return *((long*)((void*)&res));
}

float spi_receive_float(){
	unsigned long res = spi_receive_ulong();
	return *((float*)((void*)&res));
}


void ask_mod_info(){
	byte index, count, type, buffer[64];

	index = 0;

	buffer[index++] = kSyncChar;            // all frames always start with a sync character
	buffer[index++] = kGetModInfo;   // the frame type
	buffer[index++] = kTerminator;          // don't forget the terminator

	// now transmit the command
	count = index;
	index = 0;
	while (count--){
		// just throw away whatever is receive
		spi_transmit(buffer[index++]);
	}
}


void ask_heading(){
	byte index, count, type, buffer[64];

	index = 0;

	buffer[index++] = kSyncChar;            // all frames always start with a sync character
	buffer[index++] = kSetDataComponents;   // the frame type
	buffer[index++] = 1;                    // number of components to retrieve
	buffer[index++] = kHeading;
	buffer[index++] = kTerminator;          // don't forget the terminator

	// now transmit the command
	count = index;
	index = 0;
	while (count--){
		// just throw away whatever is receive
		spi_transmit(buffer[index++]);
	}

}

void get_data(){
	byte index, count, type, buffer[64];

	index = 0;

	buffer[index++] = kSyncChar;            // all frames always start with a sync character
	buffer[index++] = kGetData;   // the frame type
	buffer[index++] = kTerminator;          // don't forget the terminator

	// now transmit the command
	count = index;
	index = 0;
	while (count--){
		// just throw away whatever is receive
		spi_transmit(buffer[index++]);
	}

}

void receive_data(){
	byte frame, count, type;

	// now poll the response by looking for the response SyncChar
	while (spi_receive_byte() != kSyncChar) delayMicroseconds(250);

	frame = spi_receive_byte();

	// the next byte will be the response frame type
	if (frame == kDataResp){
		// the next byte will be the data component count
		count = spi_receive_byte();

		while (count--){
			// get the component data identifier
			type = spi_receive_byte();
			switch (type){
			case kRawX:
				data->x = spi_receive_long();
				// The "long" raw X count is now stored in data->x
				// The V2Xe can return in little endian or big endian
				// format, so prior to any transmission set the
				// V2Xe endian format
				break;
			case kRawY:
				data->y = spi_receive_long();
				break;
			case kCalibratedX:
				data->xe = spi_receive_float();
				break;
			case kCalibratedY:
				data->ye = spi_receive_float();
				break;
			case kHeading:
				data->heading = spi_receive_float();
				break;
			case kMagnitude:
				data->magnitude = spi_receive_float();
				break;
			case kTemperature:
				data->temperature = spi_receive_float();
				break;
			case kDistortion:
				data->distortion = spi_receive_byte();
				break;
			case kCalStatus:
				data->calStatus = spi_receive_byte();
				break;
			default:
				// error condition
				break;
			}
		}
	}

	Serial.println((int)data->heading);

	// cleanup of the eventual garbage
	while (spi_receive_byte() != kTerminator);
}






void GetData()
{
	byte index, count, type, buffer[64];

	index = 0;

	// send SetDataComponents command to get all the data
	// the order of the components

	buffer[index++] = kSyncChar;            // all frames always start with a sync character
	buffer[index++] = kSetDataComponents;  // the frame type
	buffer[index++] = 9;                    // number of components to retrieve
	buffer[index++] = kRawX;                // followed by the component identifiers
	buffer[index++] = kRawY;                // ... the order of the identifiers
	buffer[index++] = kCalibratedX;         // ... will the same in the response
	buffer[index++] = kCalibratedY;         // ... frame, DataResp
	buffer[index++] = kHeading;
	buffer[index++] = kMagnitude;
	buffer[index++] = kTemperature;
	buffer[index++] = kDistortion;
	buffer[index++] = kCalStatus;
	buffer[index++] = kTerminator;          // don't forget the terminator

	// now transmit the command
	count = index;
	index = 0;
	while (count--)
	{
		// just throw away whatever is receive
		spi_transmit(buffer[index++]);
	}

	// now poll the response by looking for the response SyncChar
	while (spi_receive_byte() == kSyncChar);

	// the next byte will be the response frame type
	if (spi_receive_byte() == kDataResp)
	{
		// the next byte will be the data component count
		count = spi_receive_byte();
		while (count--)
		{
			// get the component data identifier
			type = (0);
			switch (type)
			{
			case kRawX:
				data->x = spi_receive_long();
				// The "long" raw X count is now stored in data->x
				// The V2Xe can return in little endian or big endian
				// format, so prior to any transmission set the
				// V2Xe endian format
				break;
			case kRawY:
				data->y = spi_receive_long();
				break;
			case kCalibratedX:
				data->xe = spi_receive_float();
				break;
			case kCalibratedY:
				data->ye = spi_receive_float();
				break;
			case kHeading:
				data->heading = spi_receive_float();
				break;
			case kMagnitude:
				data->magnitude = spi_receive_float();
				break;
			case kTemperature:
				data->temperature = spi_receive_float();
				break;
			case kDistortion:
				data->distortion = spi_receive_byte();
				break;
			case kCalStatus:
				data->calStatus = spi_receive_byte();
				break;
			default:
				// error condition
				break;
			}
		}
	}
}