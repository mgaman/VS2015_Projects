// stdafx.h : include file for standard system include files,
//  or project specific include files that are used frequently, but
//      are changed infrequently
//

#if !defined(AFX_STDAFX_H__F235D78F_23DB_4A8D_A167_9B91283B8DC0__INCLUDED_)
#define AFX_STDAFX_H__F235D78F_23DB_4A8D_A167_9B91283B8DC0__INCLUDED_


#ifndef WINVER
#define WINVER 0x601
#endif


#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#define STRICT
#ifndef _WIN32_WINNT
#define _WIN32_WINNT 0x601//0x0400
#endif

#define VC_EXTRALEAN		// Exclude rarely-used stuff from Windows headers

#include <afxwin.h>         // MFC core and standard components
#include <afxext.h>         // MFC extensions
#include <afxdisp.h>        // MFC Automation classes
#include <afxdtctl.h>		// MFC support for Internet Explorer 4 Common Controls
#ifndef _AFX_NO_AFXCMN_SUPPORT
#include <afxcmn.h>			// MFC support for Windows Common Controls
#endif // _AFX_NO_AFXCMN_SUPPORT
#define ATLTRACE6 m_DebugFile.Dump
#define USE_DEBUGDUMP extern CDebugDump m_DebugFile;


//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_STDAFX_H__F235D78F_23DB_4A8D_A167_9B91283B8DC0__INCLUDED_)
