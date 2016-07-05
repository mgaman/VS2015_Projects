#pragma once 

#include "Hybrid.h"

#include "Processes.h" 
#include "CommProtocol.h" 

#include "Types.h"

#define HYB_TST_TIME_SECS 200
#define HYB_TST_SMPL_RATE 10
#define HYB_TST_SMPL_SIZE HYB_TST_TIME_SECS * HYB_TST_SMPL_RATE
/*
Float32 pitch, roll, temperature;
Float32 heading, mag_x, mag_y, mag_z;
Boolean distortion, cal_status;



pkt_req_data[1] = CommProtocol::kHeading;

pkt_req_data[2] = CommProtocol::kDistortion;
pkt_req_data[3] = CommProtocol::kCalStatus;

pkt_req_data[9] = CommProtocol::kPAligned;
pkt_req_data[10] = CommProtocol::kRAligned;
pkt_req_data[11] = CommProtocol::kIZAligned;

pkt_req_data[4] = CommProtocol::kPAngle;
pkt_req_data[5] = CommProtocol::kRAngle;

pkt_req_data[6] = CommProtocol::KXAligned;
pkt_req_data[7] = CommProtocol::KYAligned;
pkt_req_data[8] = CommProtocol::KZAligned;

*/

#pragma pack(push, 1)
typedef struct
{
	Float32 Heading;

	Boolean Distortion;
	Boolean CalibStatus;

	Float32 GravityVector[3];

	Float32 Pitch;
	Float32 Roll;

	Float32 MagFieldVector[3];

}Prime_Output_Data_t;
#pragma pack(pop)

// 
// This file contains the object providing communication to the 
// Prime. It will set up the unit and parse packets received 
// Process is a base class that provides Prime with cooperative 
// parallel processing. The Control method will be 
// called by a process manager on a continuous basis. 
// 
class Prime :  public CommHandler //public Process,
{
	public: 
		Prime(SerPort * serPort); 
		void Control();
		void InitDevice();
		CommProtocol * mComm;

		Prime_Output_Data_t hyb_tst_all_samples[HYB_TST_SMPL_SIZE];//delete when no needed

		UInt32 hyb_tst_sample_indx = 0;
		void hyb_tst_add_sample();

		~Prime(); 
	protected: 
		
		UInt32 mStep, mTime, mResponseTime; 
		void HandleComm(UInt8 frameType, void * dataPtr = NULL, UInt16 dataLen = 0);
		void SendComm(UInt8 frameType, void * dataPtr = NULL, UInt16 dataLen = 0);
		Prime_Output_Data_t cur_out_data;
		
		
};