#pragma once 

/*
Note: This file contains objects used to handle the serial communication with the unit. Unfortunately,
these files are not available as the program was written on a non-PC computer. The comments in the
code should explain what is expected to be sent or received from these functions so that you can
write this section for your specific platform. For example, with the TickGenerator.h, you would need
to write a routing that generates 10msec ticks.

*/

// 
//CommHandler is a base class that provides a callback for 
//incoming messages. 
// 

/*
#include <mscorlib.dll>
#include <System.dll>



using namespace System;
using namespace System::Diagnostics;
using namespace System::Drawing;
using namespace System::Windows::Forms;

*/

#include "SystemSerPort.h" 
#include "Processes.h" 
#include "Types.h"


#pragma pack(push, 1)
typedef struct
{
	UInt8 PollingMode;
	UInt8 FlushFilter;
	Float32 SensorAcqTime;
	Float32 IntervalRespTime;

}kSetAcqParams_t;

#pragma pack(pop)


class CommHandler
{
	public: // Call back to be implemented in derived class. 
		virtual void HandleComm(UInt8 frameType, void * dataPtr = NULL, UInt16 dataLen = 0) {} 
};

// 
//CommProtocol handles actual serial communication with the unit. 
//Process is a base class that provides CommProtocol with 
//cooperative parallel processing. The Control method will be 
//called by a process manager on a continuous basis. 
// 
class CommProtocol //: public Process
{
	public: 
		enum 
		{ 
			// Frame IDs (Commands) 
			kGetModInfo = 1, // 1 
			kModInfoResp, // 2 
			kSetDataComponents, // 3 
			kGetData, // 4 
			kDataResp, // 5 
			kSetConfig, // 6 
			kGetConfig, // 7 
			kConfigResp, // 8 
			kSave, // 9 
			kStartCal, // 10 
			kStopCal, // 11 
			kSetParam, // 12 
			kGetParam, // 13 
			kParamResp, // 14 
			kPowerDown, // 15 
			kSaveDone, // 16 
			kUserCalSampCount, // 17 
			kUserCalScore, // 18 
			kSetConfigDone, // 19 
			kSetParamDone, // 20 
			kStartIntervalMode, // 21
			kStopIntervalMode, // 22 
			kPowerUp, // 23 
			kSetAcqParams, // 24 
			kGetAcqParams, // 25 
			kAcqParamsDone, // 26 
			kAcqParamsResp, // 27 
			kPowerDoneDown, // 28 
			kFactoryUserCal, // 29 
			kFactoryUserCalDone, // 30 
			kTakeUserCalSample, // 31 
			kFactoryInclCal, // 36 
			kFactoryInclCalDone, // 37 

			// Param IDs 
			kFIRConfig = 1, // 3-AxisID(UInt8)+Count(UInt8)+Value(Float64)+... 
	
			// Data Component IDs 
			kHeading = 5, // 5 - type Float32 
			kDistortion = 8, //8 - Boolean
			kCalStatus, //9 - Boolean
			kPAligned = 21, // 21 - type Float32 
			kRAligned, // 22 - type Float32 
			kIZAligned, // 23 - type Float32 
			kPAngle, // 24 - type Float32
			kRAngle, // 25 - type Float32
			KXAligned = 27,// 27 - type Float32
			KYAligned, // 28 - type Float32
			KZAligned, // 29 - type Float32

			// Configuration Parameter IDs 
			kDeclination = 1, // 1 - type Float32 
			kTrueNorth, // 2 - type boolean 
			kBigEndian = 6, // 6 - type boolean 
			kMountingRef = 10, // 10 - type UInt8 
			kUserCalStableCheck, // 11 - type boolean 
			kUserCalNumPoints, // 12 - type UInt32 
			kUserCalAutoSampling, // 13 – type boolean 
			kBaudRate, // 14 – UInt8 

			// Mounting Reference IDs 
			kMountedStandard = 1, // 1 
			kMountedXUp, // 2 
			kMountedYUp, // 3 
			kMountedStdPlus90, // 4 
			kMountedStdPlus180, // 5 
			kMountedStdPlus270, // 6 // Result IDs 
			kErrNone = 0, // 0 
			kErrSave, // 1 

			// data types
			kRawX = 1,          // 0x01
			kRawY,              // 0x02
			kCalibratedX,       // 0x03
			kCalibratedY,       // 0x04
			kStam,      //kHeading,           // 0x05
			kMagnitude,         // 0x06
			kTemperature,       // 0x07
			//kDistortion,        // 0x08
			//kCalStatus,         // 0x09
		};

		enum 
		{ 
			//maximum size of our input buffer 
			kBufferSize = 512,
			//minimum size of a serial packet 
			kPacketMinSize = 5 
		}; 

		//SerPort is a serial communication object abstracting 
		//the hardware implementation 
		CommProtocol(CommHandler * handler = NULL, SerPort * serPort = NULL);
		void Init(UInt32 baud = DEFAULT_BAUD_RATE); 
		void WriteBlock(UInt8 frame, void * dataPtr = NULL, UInt32 len = 0);
		void SetBaud(UInt32 baud);
		void Control();

	protected: 
		CommHandler * mHandler; 
		SerPort * mSerialPort;
		UInt8 mOutData[kBufferSize], mInData[kBufferSize]; 
		UInt16 mExpectedLen; 
		UInt32 mOutLen, mOldInLen, mTime, mStep; 
		UInt16 CRC(void * data, UInt32 len); 
		
};