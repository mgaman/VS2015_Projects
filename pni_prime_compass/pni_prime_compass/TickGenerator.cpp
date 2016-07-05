
#include "TickGenerator.h"

LARGE_INTEGER s_frequency;
BOOL s_use_qpc;


void TicksInit()
{
	s_use_qpc = QueryPerformanceFrequency(&s_frequency);
}


UInt32 Ticks()
{
	if (s_use_qpc)
	{
		LARGE_INTEGER now;
		QueryPerformanceCounter(&now);
		return ((100LL * now.QuadPart) / s_frequency.QuadPart);
	}
	else
	{
		return (UInt32)(GetTickCount() / 10);
	}

}

