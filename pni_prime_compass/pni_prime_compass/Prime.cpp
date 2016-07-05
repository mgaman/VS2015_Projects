
#include <stdio.h>
#include <string.h>

#include "Prime.h" 
#include "TickGenerator.h" 
#include "Types.h"




const UInt8 kDataCount = 11;
// We will be requesting 4 components 
//(Heading, pitch, roll, temperature) // 
// This object polls the Prime unit once a second for heading, 
// pitch, roll and temperature. 
// 

void Move(void *src, void *dst, size_t size)
{
	memcpy(dst, src, size);
}

void SwapBytes(uint8_t *p_v, size_t n)
{
	uint8_t tmp;
	uint8_t *p = p_v;
	size_t lo, hi;
	for (lo = 0, hi = n - 1; hi>lo; lo++, hi--)
	{
		tmp = p[lo];
		p[lo] = p[hi];
		p[hi] = tmp;
	}
}


Prime::Prime(SerPort * serPort) //: Process("Prime")
{
	// Let the CommProtocol know this object will handle any 
	// serial data returned by the unit 
	mComm = new CommProtocol(this, serPort); 
	mTime = 0; 
	mStep = 1; 
} 

Prime::~Prime()
{
	delete mComm;
}

// 
// Called by the CommProtocol object when a frame is completely 
// received 
// 
void Prime::HandleComm(UInt8 frameType, void * dataPtr, UInt16 dataLen)
{
	UInt8 * data = (UInt8 *)dataPtr;

	switch (frameType)
	{
		case CommProtocol::kDataResp:
		{
			// Parse the data response 
			UInt8 count = data[0];
			// The number of data 
			// elements returned 
			UInt32 pntr = 1;
			// Used to retrieve the 
			// returned elements 
			// The data elements we requested 

			if (count != kDataCount)
			{
				// Message is a function that displays a C 
				// formatted string (similar to printf) 
				Message("Received %u data elements instead of the %u requested\r\n", (UInt16)count, (UInt16)kDataCount);
				return;
			}



			// read the type and 
			// go to the first 
			// byte of the data 

			// Only handling the 4 elements we are 
			// looking for 
			// loop through and collect the elements 
			while (count)
			{
				// The elements are received as {type (ie. 
				// kHeading), data} 
				switch (data[pntr++])
				{
				case CommProtocol::kHeading:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the heading. 
					Move(&(data[pntr]), &cur_out_data.Heading, sizeof(cur_out_data.Heading));

					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.Heading);
					break;
				}
				case CommProtocol::kDistortion:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the pitch. 
					Move(&(data[pntr]), &cur_out_data.Distortion, sizeof(cur_out_data.Distortion));

					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.Distortion);
					break;
				}
				case CommProtocol::kCalStatus:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the roll. 
					Move(&(data[pntr]), &cur_out_data.CalibStatus, sizeof(cur_out_data.CalibStatus));
					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.CalibStatus);
					break;
				}
				case CommProtocol::KXAligned: //CommProtocol::kTemperature:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the heading. 
					Move(&(data[pntr]), &cur_out_data.MagFieldVector[0], sizeof(cur_out_data.MagFieldVector[0]));
					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.MagFieldVector[0]);
					break;
				}
				case CommProtocol::KYAligned: //CommProtocol::kTemperature:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the heading. 
					Move(&(data[pntr]), &cur_out_data.MagFieldVector[1], sizeof(cur_out_data.MagFieldVector[1]));
					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.MagFieldVector[1]);
					break;
				}
				case CommProtocol::KZAligned: //CommProtocol::kTemperature:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the heading. 
					Move(&(data[pntr]), &cur_out_data.MagFieldVector[2], sizeof(cur_out_data.MagFieldVector[2]));
					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.MagFieldVector[2]);
					break;
				}
				case CommProtocol::kPAngle:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the pitch. 
					Move(&(data[pntr]), &cur_out_data.Pitch, sizeof(cur_out_data.Pitch));

					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.Pitch);
					break;
				}
				case CommProtocol::kRAngle:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the roll. 
					Move(&(data[pntr]), &cur_out_data.Roll, sizeof(cur_out_data.Roll));
					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.Roll);
					break;
				}
				case CommProtocol::kPAligned:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the roll. 
					Move(&(data[pntr]), &cur_out_data.GravityVector[0], sizeof(cur_out_data.GravityVector[0]));
					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.GravityVector[0]);
					break;
				}
				case CommProtocol::kRAligned:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the roll. 
					Move(&(data[pntr]), &cur_out_data.GravityVector[1], sizeof(cur_out_data.GravityVector[1]));
					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.GravityVector[1]);
					break;
				}
				case CommProtocol::kIZAligned:
				{
					// Move(source, destination, size 
					// (bytes)). Move copies the 
					// specified number of bytes from 
					// the source pointer to the 
					// destination pointer. 
					// Store the roll. 
					Move(&(data[pntr]), &cur_out_data.GravityVector[2], sizeof(cur_out_data.GravityVector[2]));
					// increase the pointer to point 
					// to the next data element type 
					pntr += sizeof(cur_out_data.GravityVector[2]);
					break;
				}


				default:
					// Message is a function that 
					// displays a formatted string 
					// (similar to printf) 
					Message("Unknown type: %02X\r\n", data[pntr - 1]);
					// unknown data type, so size is 
					// unknown, so skip everything 
					return;
					break;
				}

				count--; // One less element to read in 
				
			}
			// Message is a function that displays a formatted 
			// string (similar to printf) 


			Message("-----------------------------\r\n");
			Message("Heading: %f, \r\nDistortion: %d, \r\nCalibration Status: %d, \r\nMag Field X: %f, \r\nMag Field Y: %f, \r\nMag Field Z: %f\r\n", 
				cur_out_data.Heading, cur_out_data.Distortion, cur_out_data.CalibStatus, cur_out_data.MagFieldVector[0], cur_out_data.MagFieldVector[1], cur_out_data.MagFieldVector[2]);
			Message("Pitch: %f, \r\nRoll: %f, \r\nGrav X: %f, \r\nGrav Y: %f, \r\nGrav Z: %f", 
				cur_out_data.Pitch, cur_out_data.Roll, cur_out_data.GravityVector[0], cur_out_data.GravityVector[1], cur_out_data.GravityVector[2]);


			hyb_tst_add_sample();

			//SwapBytes((uint8_t*)&heading, sizeof(heading));
	
			mStep--; // send next data request
			break;
		}

		case CommProtocol::kSetConfigDone: //CommProtocol::kTemperature:
		{
			Message("Updated the configurations 1\r\n");
			break;
		}

		case CommProtocol::kAcqParamsDone: //CommProtocol::kTemperature:
		{
			Message("Updated the configurations 2\r\n");
			break;
		}

		default:
		{
			// Message is a function that displays a formatted 
			// string (similar to printf) 
			Message("Unknown frame %02X received\r\n", (UInt16)frameType);
			break;
		}
	}
}


// 
// Have the CommProtocol build and send the frame to the unit. 
// 
void Prime::SendComm(UInt8 frameType, void * dataPtr, UInt16 dataLen)
{
	if (mComm) 
		mComm->WriteBlock(frameType, dataPtr, dataLen); 
	
	// Ticks is a timer function. 1 tick = 10msec. 
		
	mResponseTime = Ticks() + 300; // Expect a response within // 3 seconds 
} 

// This is called each time this process gets a turn to execute. 
// 
void Prime::Control()
{
	switch (mStep)
	{
		case 1:
		{
			// the compents we are requesting, preceded by the 
			// number of... 
			UInt8 pkt[kDataCount + 1];
			// ...components being requested 
			/*

			pkt[0] = kDataCount;

			pkt[1] = CommProtocol::kHeading;

			pkt[2] = CommProtocol::kDistortion;
			pkt[3] = CommProtocol::kCalStatus;

			pkt[4] = CommProtocol::kPAngle;
			pkt[5] = CommProtocol::kRAngle;

			pkt[6] = CommProtocol::KXAligned;
			pkt[7] = CommProtocol::KYAligned;
			pkt[8] = CommProtocol::KZAligned;

			pkt[9] = CommProtocol::kPAligned;
			pkt[10] = CommProtocol::kRAligned;
			pkt[11] = CommProtocol::kIZAligned;
			*/

			

			SendComm(CommProtocol::kSetDataComponents, pkt, kDataCount + 1);

			// Ticks is a timer function. 1 tick = 10msec. 
			mTime = Ticks() + 100; // Taking a sample in 1s. 
			mStep++; // go to next step of process 
			break; 
		}
		case 2:
		{
			// Ticks is a timer function. 1 tick = 10msec. 
			if (Ticks() > mTime)
			{
				// tell the unit to take a sample 
				SendComm(CommProtocol::kGetData);
				mTime = Ticks() + 100;
				// take a sample every 
				// second 
				mStep++; 
			}
			break;
		}
		case 3:
		{
			// Ticks is a timer function. 1 tick = 10msec. 
			if (Ticks() > mResponseTime)
			{
				Message("No response from the unit. Check connection and try again\r\n");
				mStep = 1; //mStep = 0;//
			}
			break;
		}

		default:
			break;
		
	}
}



const UInt16 pkt_set_config_size = 2;

void Prime::InitDevice()
{
	UInt8 pkt_req_data[kDataCount + 1];
	UInt8 pkt_set_config[pkt_set_config_size];

	kSetAcqParams_t ksetacq_params;

	ksetacq_params.PollingMode = false;
	ksetacq_params.FlushFilter = false;
	ksetacq_params.IntervalRespTime = 0.01; 
	ksetacq_params.SensorAcqTime = 0.0;


	pkt_req_data[0] = kDataCount;

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


	
	pkt_set_config[0] = CommProtocol::kBigEndian;
	pkt_set_config[1] = false; //Arch depend


	SendComm(CommProtocol::kSetDataComponents, pkt_req_data, kDataCount + 1);
	SendComm(CommProtocol::kSetConfig, pkt_set_config, sizeof(pkt_set_config_size) + 1);
	SendComm(CommProtocol::kSetAcqParams, &ksetacq_params, sizeof(ksetacq_params) + 1);
	SendComm(CommProtocol::kStartIntervalMode);

}


void Prime::hyb_tst_add_sample()
{
	if (hyb_tst_sample_indx >= HYB_TST_SMPL_SIZE)
		return;

	memcpy(&hyb_tst_all_samples[hyb_tst_sample_indx], &cur_out_data, sizeof(cur_out_data));

	hyb_tst_sample_indx++; 
}