#pragma once
enum eFrameID
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
	kFactoryInclCalDone // 37
};
enum eDataComponent {
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
	kZAligned // 29 - type Float32
};
