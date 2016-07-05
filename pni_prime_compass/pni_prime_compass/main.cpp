

//#include "pni_prime_intrfc.h"
#include <conio.h>
#include <stdlib.h>

#include "CommProtocol.h"
#include "TickGenerator.h"
#include "Prime.h"
#include "serial_handler.h"
#include "Types.h"
#include "SystemSerPort.h"


#define PRIME_COM_PORT_NUM 45
int COM_number = PRIME_COM_PORT_NUM;
SerPort *serial_con_handler;
Prime *prime_handler;



void InitPrime()
{
	prime_handler = new Prime(serial_con_handler);
	prime_handler->InitDevice();
}

void InitComPort()
{

	serial_con_handler = new CSerial();

	if (serial_con_handler->Open(COM_number, CBR_57600))
	{
		
		Message("The port opened successfully\n");
	}
		
	else
	{
		delete serial_con_handler;
		Message("Problem when opening the port ");
	}
		
}

void DisposeObjects()
{

	delete serial_con_handler;
	delete prime_handler;
}

int getch_noblock() {
	if (_kbhit())
		return _getch();
	else
		return -1;
}



void hybrid_pni_init()
{
	atexit(DisposeObjects);

	TicksInit();
	InitComPort();
	InitPrime();


}

void hybrid_pni_sample()
{
	prime_handler->mComm->Control();

}

void main()
{
	atexit(DisposeObjects);

	bool exit_f = 1;
	char c;
	UInt32 last_time;
	uint8_t pause_f = 0;

	hybrid_pni_init();


	last_time = Ticks();

	while (1)
	{
		c = getch_noblock();
		if (c == 'p')
			pause_f = ~pause_f;

		if (c == 's')
			break;

		if (pause_f == 0)
		{
			if ((Ticks() - last_time) >= 100)
			{

				Message("Querying...\n");
				//prime_handler->Control();
				last_time = Ticks();

			}

			prime_handler->mComm->Control();

		}
		


	}


	//DisposeObjects();



}




