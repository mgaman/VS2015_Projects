/*
https://www.yumpu.com/en/document/view/53682397/-/29
http://www.manualsdir.com/manuals/373139/pni-compasspoint-prime.html?page=69


*/

#include "Types.h"

#pragma pack(push, 1)

typedef struct
{
	UInt8 pollingMode, flushFilter;
	Float32 sensorAcqTime, intervalRespTime;
} AcqParams;	//__attribute__((packed)) AcqParams;

typedef struct
{
	Float32 stdDevErr;
	Float32 xCoverage;
	Float32 yCoverage;
	Float32 zCoverage;
	Float32 xyzAccelCoverage;
	Float32 accelStdDevErr;
} CalScore; // __attribute__((packed)) CalScore;


#pragma pack(pop)

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
	kDistortion = 8, // 8 - type boolean 
	kPAligned = 21, // 21 - type Float32 
	kRAligned, // 22 - type Float32 
	kIZAligned, // 23 - type Float32 
	kPAngle, // 24 - type Float32 
	kRAngle, // 25 - type Float32 
	kXAligned = 27, // 27 - type Float32 
	kYAligned, // 28 - type Float32 
	kZAligned, // 29 - type Float32 

	// Configuration Parameter IDs 
	kDeclination = 1, // 1 - type Float32 
	kTrueNorth, // 2 - type boolean 
	kMountingRef = 10, // 10 - type UInt8 
	kUserCalStableCheck, // 11 - type boolean 
	kUserCalNumPoints, // 12 - type UInt32 
	kUserCalAutoSampling, // 13 � type boolean 
	kBaudRate, // 14 � UInt8 

	// Mounting Reference IDs 
	kMountedStandard = 1, // 1 
	kMountedXUp, // 2 
	kMountedYUp, // 3 
	kMountedStdPlus90, // 4 
	kMountedStdPlus180, // 5 
	kMountedStdPlus270, // 6 // Result IDs 
	kErrNone = 0, // 0 
	kErrSave, // 1 
};

// function to calculate CRC-16 
UInt16 CRC(void * data, UInt32 len)
{
	UInt8 * dataPtr = (UInt8 *)data; 
	
	UInt32 index = 0;
	// Update the CRC for transmitted and received data using 
	// the CCITT 16bit algorithm (X^16 + X^12 + X^5 + 1). 

	UInt16 crc = 0;
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