using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;
using Microsoft.SqlServer.XEvent;
using Microsoft.SqlServer.XEvent.Linq;
using Microsoft.SqlServer.XEvent.Linq.Internal;
using std;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x0000DBD8 File Offset: 0x0000DBD8
	internal unsafe static int StringCchCopyW(char* pszDest, ulong cchDest, char* pszSrc)
	{
		int num = 0;
		if (cchDest == 0UL || cchDest > 2147483647UL)
		{
			num = -2147024809;
		}
		int num2 = num;
		if (num >= 0)
		{
			num2 = <Module>.?A0xee63309e.StringCopyWorkerW(pszDest, cchDest, null, pszSrc, 2147483646UL);
		}
		else if (cchDest > 0UL)
		{
			*pszDest = '\0';
		}
		return num2;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000D6E0 File Offset: 0x0000D6E0
	internal unsafe static int ?A0xee63309e.StringCopyWorkerW(char* pszDest, ulong cchDest, ulong* pcchNewDestLength, char* pszSrc, ulong cchToCopy)
	{
		int num = 0;
		ulong num2 = 0UL;
		if (cchDest != 0UL)
		{
			ulong num3 = cchToCopy;
			long num4 = (long)(pszSrc - pszDest);
			while (num3 != 0UL)
			{
				ushort num5 = (ushort)(num4 / 2L)[pszDest];
				if (num5 == 0)
				{
					break;
				}
				*pszDest = (char)num5;
				pszDest += 2L / 2L;
				cchDest -= 1UL;
				num3 -= 1UL;
				num2 += 1UL;
				if (cchDest == 0UL)
				{
					goto IL_003D;
				}
			}
			if (cchDest != 0UL)
			{
				goto IL_004E;
			}
		}
		IL_003D:
		pszDest -= 2L / 2L;
		num2 -= 1UL;
		num = -2147024774;
		IL_004E:
		*pszDest = '\0';
		if (pcchNewDestLength != null)
		{
			*pcchNewDestLength = num2;
		}
		return num;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x0000D74C File Offset: 0x0000D74C
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool FeatureSwitchStub.Enabled(FeatureSwitchStub* A_0, XE_FeatureSwitchId id)
	{
		return 0;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000D760 File Offset: 0x0000D760
	internal unsafe static void* XEAPI_Stubs.MemAlloc(void* hsess, uint cbLen)
	{
		return <Module>.new[]((ulong)cbLen);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000D77C File Offset: 0x0000D77C
	internal unsafe static void XEAPI_Stubs.MemFree(void* pMem)
	{
		<Module>.delete[](pMem);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000D798 File Offset: 0x0000D798
	internal unsafe static void* XEAPI_Stubs.CreateSection(void* hFile, _SECURITY_ATTRIBUTES* lpFileMappingAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, char* lpName)
	{
		return <Module>.CreateFileMappingW(hFile, lpFileMappingAttributes, flProtect, dwMaximumSizeHigh, dwMaximumSizeLow, lpName);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000D7B8 File Offset: 0x0000D7B8
	internal unsafe static void* XEAPI_Stubs.MapSectionView(void* section, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, ulong dwNumberOfBytesToMap)
	{
		return <Module>.MapViewOfFile(section, dwDesiredAccess, dwFileOffsetHigh, dwFileOffsetLow, dwNumberOfBytesToMap);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000D7D8 File Offset: 0x0000D7D8
	internal unsafe static int XEAPI_Stubs.UnmapSectionView(void* section, void* pView, ulong dwNumberOfBytesToUnmap)
	{
		return <Module>.UnmapViewOfFile((void*)pView);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000D7F4 File Offset: 0x0000D7F4
	internal unsafe static void XEAPI_Stubs.DestroySection(void* section)
	{
		<Module>.CloseHandle(section);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000D810 File Offset: 0x0000D810
	internal static int XEAPI_Stubs.IsInitialized()
	{
		return 1;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000D824 File Offset: 0x0000D824
	internal unsafe static int XEAPI_Stubs.CreateXEMutex(void** pXeMutex)
	{
		int num = 0;
		void* ptr = <Module>.CreateMutexW(null, 0, null);
		if (ptr != null)
		{
			*(long*)pXeMutex = ptr;
			num = 1;
		}
		return num;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000D7F4 File Offset: 0x0000D7F4
	internal unsafe static void XEAPI_Stubs.DestroyMutex(void* pXeMutex)
	{
		<Module>.CloseHandle(pXeMutex);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000D84C File Offset: 0x0000D84C
	internal unsafe static XEWaitResult XEAPI_Stubs.EnterMutex(void* pXeMutex, uint timeout, XEWaitType waitType)
	{
		uint num = <Module>.WaitForSingleObject(pXeMutex, timeout);
		if (num != 0U)
		{
			return (num != 258U) ? ((XEWaitResult)2) : ((XEWaitResult)1);
		}
		return (XEWaitResult)0;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000D878 File Offset: 0x0000D878
	internal unsafe static void XEAPI_Stubs.ReleaseXEMutex(void* pXeMutex)
	{
		<Module>.ReleaseMutex(pXeMutex);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000D894 File Offset: 0x0000D894
	internal unsafe static int XEAPI_Stubs.CreateRWLock(void** pRWLock)
	{
		int num = 0;
		void* ptr = <Module>.CreateMutexW(null, 0, null);
		if (ptr != null)
		{
			*(long*)pRWLock = ptr;
			num = 1;
		}
		return num;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000D7F4 File Offset: 0x0000D7F4
	internal unsafe static void XEAPI_Stubs.DestroyRWLock(void* pRWLock)
	{
		<Module>.CloseHandle(pRWLock);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x0000D8BC File Offset: 0x0000D8BC
	internal unsafe static XEWaitResult XEAPI_Stubs.EnterRWLock(void* pRWLock, XERWMode lockMode, uint timeout)
	{
		uint num = <Module>.WaitForSingleObject(pRWLock, timeout);
		XEWaitResult xewaitResult;
		if (num != 0U)
		{
			xewaitResult = ((num != 258U) ? ((XEWaitResult)2) : ((XEWaitResult)1));
		}
		else
		{
			xewaitResult = (XEWaitResult)0;
		}
		return (XEWaitResult)xewaitResult;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000D8F0 File Offset: 0x0000D8F0
	internal unsafe static void XEAPI_Stubs.ReleaseRWLock(void* pRWLock, XERWMode lockMode)
	{
		<Module>.ReleaseMutex(pRWLock);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000D90C File Offset: 0x0000D90C
	internal unsafe static int XEAPI_Stubs.CreateEventManual(void** pEventManual, int isSignaled)
	{
		int num = 0;
		void* ptr = <Module>.CreateEventW(null, 1, isSignaled, null);
		if (ptr != null)
		{
			*(long*)pEventManual = ptr;
			num = 1;
		}
		return num;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000D7F4 File Offset: 0x0000D7F4
	internal unsafe static void XEAPI_Stubs.DestroyEventManual(void* pEventManual)
	{
		<Module>.CloseHandle(pEventManual);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000D934 File Offset: 0x0000D934
	internal unsafe static XEWaitResult XEAPI_Stubs.WaitEventManual(void* pEventManual, uint timeout)
	{
		uint num = <Module>.WaitForSingleObject(pEventManual, timeout);
		if (num != 0U)
		{
			return (num != 258U) ? ((XEWaitResult)2) : ((XEWaitResult)1);
		}
		return (XEWaitResult)0;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000D960 File Offset: 0x0000D960
	internal unsafe static void XEAPI_Stubs.ResetEventManual(void* pEventManual)
	{
		<Module>.ResetEvent(pEventManual);
	}

	// Token: 0x06000017 RID: 23 RVA: 0x0000D97C File Offset: 0x0000D97C
	internal unsafe static void XEAPI_Stubs.SignalEventManual(void* pEventManual)
	{
		<Module>.SetEvent(pEventManual);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x0000D998 File Offset: 0x0000D998
	internal unsafe static int XEAPI_Stubs.IsSignaledEventManual(void* pEventManual)
	{
		int num = 0;
		return (<Module>.WaitForSingleObject(pEventManual, 0) == 0) ? 1 : num;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x0000D9C0 File Offset: 0x0000D9C0
	internal unsafe static int XEAPI_Stubs.CreateAsyncIORequest(void** pIOReq)
	{
		XEAPI_Stubs.XEStubAsyncioReq* ptr = <Module>.@new(64UL);
		XEAPI_Stubs.XEStubAsyncioReq* ptr2;
		if (ptr != null)
		{
			initblk(ptr, 0, 64L);
			ptr2 = ptr;
		}
		else
		{
			ptr2 = null;
		}
		*(long*)pIOReq = ptr2;
		return 1;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000D9F4 File Offset: 0x0000D9F4
	internal unsafe static void XEAPI_Stubs.DestroyAsyncIORequest(void* pIOReq)
	{
		ulong num = (ulong)(*(long*)((byte*)pIOReq + 24L));
		if (num != 18446744073709551615UL)
		{
			<Module>.CloseHandle(num);
		}
		<Module>.delete(pIOReq);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x0000DA28 File Offset: 0x0000DA28
	internal unsafe static void XEAPI_Stubs.ResetAsyncIORequest(void* pIOReq, void* handle, ulong position, void* pUserData, delegate* unmanaged[Cdecl, Cdecl]<void*, void> pfnCompRoutine)
	{
		*(long*)((byte*)pIOReq + 32L) = handle;
		*(int*)((byte*)pIOReq + 16L) = (int)((uint)position);
		*(int*)((byte*)pIOReq + 20L) = (int)((uint)(position >> 32));
		*(long*)pIOReq = 0L;
		*(long*)((byte*)pIOReq + 8L) = 0L;
		ulong num = (ulong)(*(long*)((byte*)pIOReq + 24L));
		if (num != 18446744073709551615UL)
		{
			<Module>.CloseHandle(num);
		}
		*(long*)((byte*)pIOReq + 24L) = <Module>.CreateEventW(null, 1, 0, null);
		*(int*)((byte*)pIOReq + 40L) = 0;
		*(int*)((byte*)pIOReq + 44L) = 0;
		*(long*)((byte*)pIOReq + 56L) = pUserData;
		*(long*)((byte*)pIOReq + 48L) = pfnCompRoutine;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x0000DAA8 File Offset: 0x0000DAA8
	internal unsafe static _OVERLAPPED* XEAPI_Stubs.GetAsyncIORequestOverlappedPtr(void* pIOReq)
	{
		return pIOReq;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000DAA8 File Offset: 0x0000DAA8
	internal unsafe static void* XEAPI_Stubs.GetAsyncIORequestUserData(void* pIOReq)
	{
		return pIOReq;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000DABC File Offset: 0x0000DABC
	internal static uint XEAPI_Stubs.GetPartitionID()
	{
		return 0;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000D74C File Offset: 0x0000D74C
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool XEAPI_Stubs.HasHttpsPrefix(char* wchFileName, ulong cbFileName)
	{
		return 0;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x0000DC24 File Offset: 0x0000DC24
	internal unsafe static int XEAPI_Stubs.XEDefaultPathCanonicalize(char* lpCanonicalizedPath, char* lpSourcePath)
	{
		$ArrayType$$$BY0BAE@_W $ArrayType$$$BY0BAE@_W;
		cpblk(ref $ArrayType$$$BY0BAE@_W, ref <Module>.??_C@_11LOCGONAA@@, 2);
		initblk((ref $ArrayType$$$BY0BAE@_W) + 2, 0, 518L);
		int num = <Module>.PathCanonicalizeW(lpCanonicalizedPath, lpSourcePath);
		if (num != 0 && <Module>.PathIsRelativeW((char*)lpCanonicalizedPath) != null && 0L != <Module>._wfullpath((char*)(&$ArrayType$$$BY0BAE@_W), (char*)lpCanonicalizedPath, 260UL))
		{
			num = ((<Module>.StringCchCopyW(lpCanonicalizedPath, 260UL, (char*)(&$ArrayType$$$BY0BAE@_W)) >= 0) ? 1 : 0);
		}
		return num;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000DAD0 File Offset: 0x0000DAD0
	internal unsafe static int XEAPI_Stubs.XEDefaultPathRemoveExtension(char* pszPath, ulong cchPath)
	{
		<Module>.PathRemoveExtensionW(pszPath);
		return 1;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000DAEC File Offset: 0x0000DAEC
	internal unsafe static int XEAPI_Stubs.XEDefaultPathRemoveBackslash(char* pszPath, ulong cchPath)
	{
		<Module>.PathRemoveBackslashW(pszPath);
		return 1;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000DC90 File Offset: 0x0000DC90
	internal unsafe static int XEAPI_Stubs.XEDefaultPathRemoveFileSpec(char* pszPath, char* pszPathOut, ulong cchPathOut)
	{
		int num = 0;
		if (pszPath != null && pszPathOut != null)
		{
			if (<Module>.PathIsURLW(pszPath) != null)
			{
				char* ptr = <Module>.PathFindFileNameW(pszPath);
				if (ptr != pszPath)
				{
					ulong num2 = ptr - pszPath >> 1;
					int num3 = 0;
					if (cchPathOut == 0UL || cchPathOut > 2147483647UL)
					{
						num3 = -2147024809;
					}
					int num4 = num3;
					if (num3 >= 0)
					{
						if (num2 > 2147483646UL)
						{
							*pszPathOut = '\0';
							goto IL_0079;
						}
						num4 = <Module>.?A0xee63309e.StringCopyWorkerW(pszPathOut, cchPathOut, null, pszPath, num2);
					}
					else if (cchPathOut > 0UL)
					{
						*pszPathOut = '\0';
					}
					if (num4 >= 0)
					{
						return 1;
					}
					IL_0079:
					num = 0;
				}
			}
			else
			{
				int num5 = 0;
				if (cchPathOut == 0UL || cchPathOut > 2147483647UL)
				{
					num5 = -2147024809;
				}
				int num6 = num5;
				if (num5 >= 0)
				{
					num6 = <Module>.?A0xee63309e.StringCopyWorkerW(pszPathOut, cchPathOut, null, pszPath, 2147483646UL);
				}
				else if (cchPathOut > 0UL)
				{
					*pszPathOut = '\0';
				}
				if (num6 >= 0)
				{
					num = <Module>.PathRemoveFileSpecW(pszPathOut);
				}
			}
		}
		return num;
	}

	// Token: 0x06000024 RID: 36 RVA: 0x0000DD64 File Offset: 0x0000DD64
	internal unsafe static int XEAPI_Stubs.XEDefaultPathFindExtension(char* pszPath, char* pszPathOut, ulong cchPathOut)
	{
		int num = 0;
		if (pszPathOut != null && pszPath != null)
		{
			char* ptr = <Module>.PathFindExtensionW(pszPath);
			int num2 = 0;
			if (cchPathOut == 0UL || cchPathOut > 2147483647UL)
			{
				num2 = -2147024809;
			}
			int num3 = num2;
			if (num2 >= 0)
			{
				num3 = <Module>.?A0xee63309e.StringCopyWorkerW(pszPathOut, cchPathOut, null, (char*)ptr, 2147483646UL);
			}
			else if (cchPathOut > 0UL)
			{
				*pszPathOut = '\0';
			}
			num = ((num3 >= 0) ? 1 : 0);
		}
		return num;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000DDC8 File Offset: 0x0000DDC8
	internal unsafe static int XEAPI_Stubs.XEDefaultPathFindFileName(char* pszPath, char* pszPathOut, ulong cchPathOut)
	{
		int num = 0;
		if (pszPathOut != null && pszPath != null)
		{
			char* ptr = <Module>.PathFindFileNameW(pszPath);
			int num2 = 0;
			if (cchPathOut == 0UL || cchPathOut > 2147483647UL)
			{
				num2 = -2147024809;
			}
			int num3 = num2;
			if (num2 >= 0)
			{
				num3 = <Module>.?A0xee63309e.StringCopyWorkerW(pszPathOut, cchPathOut, null, (char*)ptr, 2147483646UL);
			}
			else if (cchPathOut > 0UL)
			{
				*pszPathOut = '\0';
			}
			num = ((num3 >= 0) ? 1 : 0);
		}
		return num;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000DB08 File Offset: 0x0000DB08
	internal unsafe static int XEAPI_Stubs.XEDefaultPathCombine(char* pszPathOut, ulong cchPathOut, char* pszPathIn, char* pszMore)
	{
		return (0L != <Module>.PathCombineW(pszPathOut, pszPathIn, pszMore)) ? 1 : 0;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000DB2C File Offset: 0x0000DB2C
	internal unsafe static int XEAPI_Stubs.XEDefaultURLCombine(char* pszPathOut, ulong cchPathOut, char* pszPathIn, char* pszMore)
	{
		if (cchPathOut <= 260UL)
		{
			uint num = (uint)cchPathOut;
			return (<Module>.UrlCombineW(pszPathIn, pszMore, pszPathOut, &num, 0) >= 0) ? 1 : 0;
		}
		return 0;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000DB60 File Offset: 0x0000DB60
	internal unsafe static int XEAPI_Stubs.XEDefaultPathIsURL(char* pszPath)
	{
		return <Module>.PathIsURLW(pszPath);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000DB7C File Offset: 0x0000DB7C
	internal unsafe static XE_IFeatureSwitches* XEAPI_Stubs.GetFeatureSwitches()
	{
		return ref <Module>.g_FeatureSwitchStub;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x0000DE2C File Offset: 0x0000DE2C
	internal unsafe static int XEAPI_Stubs.XEDefaultMultiTenantPathCanonicalize(char* lpCanonicalizedPath, char* lpSourcePath, int sharedRootEnabled, int isPrivate)
	{
		return <Module>.XEAPI_Stubs.XEDefaultPathCanonicalize(lpCanonicalizedPath, lpSourcePath);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x0000DB94 File Offset: 0x0000DB94
	internal unsafe static int XEAPI_Stubs.XEDefaultCheckSessionFilters(XESessionProperties* sessionProperties)
	{
		return 1;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000DABC File Offset: 0x0000DABC
	internal static int XEAPI_Stubs.IsOperationAborted()
	{
		return 0;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x0000DBA8 File Offset: 0x0000DBA8
	internal unsafe static int XEAPI_Stubs.IsStackAvailable(ulong cbRequired)
	{
		ulong num;
		ulong num2;
		<Module>.GetCurrentThreadStackLimits(&num, &num2);
		return (cbRequired + num + 65536L <= (ref num)) ? 1 : 0;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000DE48 File Offset: 0x0000DE48
	internal unsafe static void XE_MinApi.StaticInit()
	{
		if (<Module>.XE_OneTimeInit.InitializeBegin(ref <Module>.?sm_oti@XE_API@@2VXE_OneTimeInit@@A) != null)
		{
			<Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A = 18;
			*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 8) = ref <Module>.??_C@_1CE@DNHAICD@?$AAX?$AAE?$AAE?$AAn?$AAg?$AAi?$AAn?$AAe?$AAC?$AAl?$AAi?$AAe?$AAn?$AAt?$AAA@;
			<Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A = 20;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 8) = ref <Module>.??_C@_1CI@FENJONKI@?$AAX?$AAE?$AAE?$AAn?$AAg?$AAi?$AAn?$AAe?$AAS?$AAe?$AAr?$AAv?$AAi?$AAc?$AAe@;
			<Module>.?sm_RegistrationAPI@XE_API@@2UXEEngineRegisterAPI@@A = 10;
			*((ref <Module>.?sm_RegistrationAPI@XE_API@@2UXEEngineRegisterAPI@@A) + 8) = ref <Module>.??_C@_1CI@DIEFHCOF@?$AAX?$AAE?$AAE?$AAn?$AAg?$AAi?$AAn?$AAe?$AAR?$AAe?$AAg?$AAi?$AAs?$AAt?$AAe@;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 72) = <Module>.__unep@?MemAlloc@XEAPI_Stubs@@$$FSAPEAXPEAXI@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 80) = <Module>.__unep@?MemFree@XEAPI_Stubs@@$$FSAXQEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 88) = <Module>.__unep@?CreateFileW@@$$J0YAPEAXPEB_WKKPEAU_SECURITY_ATTRIBUTES@@KKPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 96) = <Module>.__unep@?CreateSection@XEAPI_Stubs@@$$FSAPEAXPEAXPEAU_SECURITY_ATTRIBUTES@@KKKPEB_W@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 104) = <Module>.__unep@?MapSectionView@XEAPI_Stubs@@$$FSAPEAXPEAXKKK_K@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 112) = <Module>.__unep@?UnmapSectionView@XEAPI_Stubs@@$$FSAHPEAX0_K@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 120) = <Module>.__unep@?DestroySection@XEAPI_Stubs@@$$FSAXPEAX@Z;
			*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 40) = <Module>.__unep@?IsInitialized@XEAPI_Stubs@@$$FSAHXZ;
			*((ref <Module>.?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A) + 440) = <Module>.__unep@?GetFeatureSwitches@XEAPI_Stubs@@$$FSAPEAVXE_IFeatureSwitches@@XZ;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 128) = <Module>.__unep@?CreateXEMutex@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 136) = <Module>.__unep@?DestroyMutex@XEAPI_Stubs@@$$FSAXPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 144) = <Module>.__unep@?EnterMutex@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXIW4XEWaitType@@@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 152) = <Module>.__unep@?ReleaseXEMutex@XEAPI_Stubs@@$$FSAXPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 160) = <Module>.__unep@?CreateRWLock@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 168) = <Module>.__unep@?DestroyRWLock@XEAPI_Stubs@@$$FSAXPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 176) = <Module>.__unep@?EnterRWLock@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXW4XERWMode@@I@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 184) = <Module>.__unep@?ReleaseRWLock@XEAPI_Stubs@@$$FSAXPEAXW4XERWMode@@@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 192) = <Module>.__unep@?CreateEventManual@XEAPI_Stubs@@$$FSAHPEAPEAXH@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 200) = <Module>.__unep@?DestroyEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 208) = <Module>.__unep@?WaitEventManual@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXI@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 216) = <Module>.__unep@?ResetEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 224) = <Module>.__unep@?SignalEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 232) = <Module>.__unep@?IsSignaledEventManual@XEAPI_Stubs@@$$FSAHPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 248) = <Module>.__unep@?CreateAsyncIORequest@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 256) = <Module>.__unep@?DestroyAsyncIORequest@XEAPI_Stubs@@$$FSAXPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 264) = <Module>.__unep@?ResetAsyncIORequest@XEAPI_Stubs@@$$FSAXPEAX0_K0P6AX0@Z@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 272) = <Module>.__unep@?GetAsyncIORequestOverlappedPtr@XEAPI_Stubs@@$$FSAPEAU_OVERLAPPED@@PEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 280) = <Module>.__unep@?GetAsyncIORequestUserData@XEAPI_Stubs@@$$FSAPEAXPEAX@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 400) = <Module>.__unep@?XEDefaultPathCanonicalize@XEAPI_Stubs@@$$FSAHPEA_WPEB_W@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 296) = <Module>.__unep@?GetPartitionID@XEAPI_Stubs@@$$FSAIXZ;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 416) = <Module>.__unep@?HasHttpsPrefix@XEAPI_Stubs@@$$FSA_NPEB_W_K@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 504) = <Module>.__unep@?XEDefaultPathRemoveExtension@XEAPI_Stubs@@$$FSAHPEA_W_K@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 512) = <Module>.__unep@?XEDefaultPathRemoveBackslash@XEAPI_Stubs@@$$FSAHPEA_W_K@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 520) = <Module>.__unep@?XEDefaultPathRemoveFileSpec@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 528) = <Module>.__unep@?XEDefaultPathFindExtension@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 536) = <Module>.__unep@?XEDefaultPathFindFileName@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 544) = <Module>.__unep@?XEDefaultPathCombine@XEAPI_Stubs@@$$FSAHPEA_W_KPEB_W2@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 552) = <Module>.__unep@?XEDefaultURLCombine@XEAPI_Stubs@@$$FSAHPEA_W_KPEB_W2@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 560) = <Module>.__unep@?XEDefaultPathIsURL@XEAPI_Stubs@@$$FSAHPEB_W@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 576) = <Module>.__unep@?XEDefaultMultiTenantPathCanonicalize@XEAPI_Stubs@@$$FSAHPEA_WPEB_WHH@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 584) = <Module>.__unep@?XEDefaultCheckSessionFilters@XEAPI_Stubs@@$$FSAHAEBUXESessionProperties@@@Z;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 592) = <Module>.__unep@?IsOperationAborted@XEAPI_Stubs@@$$FSAHXZ;
			*((ref <Module>.?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A) + 600) = <Module>.__unep@?IsStackAvailable@XEAPI_Stubs@@$$FSAH_K@Z;
		}
		<Module>.XE_OneTimeInit.InitializeEnd(ref <Module>.?sm_oti@XE_API@@2VXE_OneTimeInit@@A);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00012150 File Offset: 0x00012150
	internal unsafe static XEPackageMetadata* Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.GetEventPackageMd(XEventInteropMetadataAdapter* A_0, ushort packageId)
	{
		return <Module>.Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.GetPackageMd(A_0, packageId);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000117EC File Offset: 0x000117EC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.?A0xd4db69fe.DeserializePackageCallbackHoop(void* cookie, XE_LogDeserializedPackage* deser)
	{
		XEventInteropMetadataManager.DeserializeMetadataCallback(cookie, deser);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000EE08 File Offset: 0x0000EE08
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool Microsoft.SqlServer.XEvent.Linq.Internal.XE_CompareManaged(XERelativeObjectId r1, XERelativeObjectId r2)
	{
		int num;
		if (((r2 ^ r1) & 268434432) == null && ((r2 ^ r1) & 1023) == null && ((r2 ^ r1) & -268435456) == null)
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00010048 File Offset: 0x00010048
	internal unsafe static XEventInteropMetadataAdapter* Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.{ctor}(XEventInteropMetadataAdapter* A_0, XEventInteropMetadataGeneration activeGen)
	{
		<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.{ctor}(A_0);
		try
		{
			XEventInteropMetadataAdapter* ptr = A_0 + 8L;
			<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.{ctor}(ptr);
			try
			{
				XEventInteropMetadataAdapter* ptr2 = A_0 + 16L;
				<Module>.std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.{ctor}(ptr2);
				try
				{
					<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.=(A_0, null);
					<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.=(ptr, activeGen);
					<Module>.std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.clear(ptr2);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.{dtor}), (void*)(A_0 + (byte*)16L));
					throw;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.{dtor}), (void*)(A_0 + (byte*)8L));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00010114 File Offset: 0x00010114
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.SetActiveGeneration(XEventInteropMetadataAdapter* A_0, XEventInteropMetadataGeneration activeGen)
	{
		<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.=(A_0, null);
		<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.=(A_0 + 8L, activeGen);
		<Module>.std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.clear(A_0 + 16L);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00010144 File Offset: 0x00010144
	internal unsafe static XEPackageMetadata* Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.GetPackageMdManaged(XEventInteropMetadataAdapter* A_0, ushort packageId)
	{
		long num = (long)stackalloc byte[<Module>.__CxxQueryExceptionSize()];
		uint exceptionCode;
		try
		{
			return <Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.->(A_0 + 8L).GetPackage(packageId).GetPackageMd();
		}
		catch when (delegate
		{
			// Failed to create a 'catch-when' expression
			exceptionCode = (uint)Marshal.GetExceptionCode();
			endfilter(<Module>.__CxxExceptionFilter(Marshal.GetExceptionPointers(), null, 0, null) != null);
		})
		{
			uint num2 = 0U;
			<Module>.__CxxRegisterExceptionObject(Marshal.GetExceptionPointers(), num);
			try
			{
				try
				{
					return 0L;
				}
				catch when (delegate
				{
					// Failed to create a 'catch-when' expression
					num2 = <Module>.__CxxDetectRethrow(Marshal.GetExceptionPointers());
					endfilter(num2 != 0U);
				})
				{
				}
				if (num2 != 0U)
				{
					throw;
				}
			}
			finally
			{
				<Module>.__CxxUnregisterExceptionObject(num, (int)num2);
			}
		}
		return 0L;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x000102BC File Offset: 0x000102BC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.GetTicksConfig(XEventInteropMetadataAdapter* A_0, XETicksConfig* pConfig)
	{
		<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.->(A_0 + 8L).GetTicksConfig(pConfig);
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000102E0 File Offset: 0x000102E0
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.LocateMetadata(XEventInteropMetadataAdapter* A_0, XE_LogDefaultMetadataHeader* metadataHeader)
	{
		bool flag = false;
		if (<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>..PE$AAVXEventInteropMetadataManager@Internal@Linq@XEvent@SqlServer@Microsoft@@(A_0) != null)
		{
			XEventInteropMetadataAdapter* ptr = A_0 + 8L;
			<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.=(ptr, <Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.->(A_0).GetGeneration((_GUID*)metadataHeader, *(ushort*)(metadataHeader + 16L / (long)sizeof(XE_LogDefaultMetadataHeader))));
			<Module>.std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.clear(A_0 + 16L);
			flag = ((<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>..PE$AAVXEventInteropMetadataGeneration@Internal@Linq@XEvent@SqlServer@Microsoft@@(ptr) != null) ? 1 : 0) != 0;
		}
		else
		{
			XEventInteropMetadataAdapter* ptr = A_0 + 8L;
			if (<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.->(ptr).GenerationId.Value == *(ushort*)(metadataHeader + 16L / (long)sizeof(XE_LogDefaultMetadataHeader)))
			{
				Guid guid = (Guid)Marshal.PtrToStructure((IntPtr)((void*)metadataHeader), typeof(Guid));
				flag = <Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.->(ptr).GenerationId.Key == guid || flag;
			}
		}
		return flag;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x000103A0 File Offset: 0x000103A0
	internal unsafe static XEventInteropMetadataAdapter.PackageEnumerator* Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.PackageEnumerator.{ctor}(XEventInteropMetadataAdapter.PackageEnumerator* A_0, XEventInteropMetadataAdapter* metadataSource)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		try
		{
			*(A_0 + 8L) = metadataSource;
			*(A_0 + 16L) = 0L;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00011368 File Offset: 0x00011368
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.PackageEnumerator.{dtor}(XEventInteropMetadataAdapter.PackageEnumerator* A_0)
	{
		try
		{
			XEventInteropMetadataAdapter.PackageEnumerator* ptr = A_0 + 16L;
			ulong num = (ulong)(*ptr);
			if (num != 0UL)
			{
				XE_PackageFilter* ptr2 = num;
				<Module>.XE_PackageFilter.{dtor}(ptr2);
				<Module>.delete((void*)ptr2, 104UL);
				*ptr = 0L;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.{dtor}), A_0);
			throw;
		}
		<Module>.gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.{dtor}(A_0);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00010404 File Offset: 0x00010404
	internal unsafe static void XE_PackageFilter.{dtor}(XE_PackageFilter* A_0)
	{
		try
		{
			try
			{
				try
				{
					XE_PackageFilter.FilterHolder* ptr = A_0 + 80L;
					<Module>.XE_AutoRg<unsigned\u0020int>.{dtor}(ptr + 16L);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(XE_PackageFilter.FilterHolder.{dtor}), (void*)(A_0 + (byte*)56L));
					throw;
				}
				XE_PackageFilter.FilterHolder* ptr2 = A_0 + 56L;
				<Module>.XE_AutoRg<unsigned\u0020int>.{dtor}(ptr2 + 16L);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(XE_PackageFilter.FilterHolder.{dtor}), (void*)(A_0 + (byte*)32L));
				throw;
			}
			XE_PackageFilter.FilterHolder* ptr3 = A_0 + 32L;
			<Module>.XE_AutoRg<unsigned\u0020int>.{dtor}(ptr3 + 16L);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_PackageFilter.FilterHolder.{dtor}), (void*)(A_0 + (byte*)8L));
			throw;
		}
		XE_PackageFilter.FilterHolder* ptr4 = A_0 + 8L;
		<Module>.XE_AutoRg<unsigned\u0020int>.{dtor}(ptr4 + 16L);
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000EEB8 File Offset: 0x0000EEB8
	internal unsafe static void XE_PackageFilter.FilterHolder.{dtor}(XE_PackageFilter.FilterHolder* A_0)
	{
		<Module>.XE_AutoRg<unsigned\u0020int>.{dtor}(A_0 + 16L);
	}

	// Token: 0x0600003B RID: 59 RVA: 0x000104DC File Offset: 0x000104DC
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.PackageEnumerator.Begin(XEventInteropMetadataAdapter.PackageEnumerator* A_0)
	{
		if (<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>..PE$AAVXEventInteropMetadataGeneration@Internal@Linq@XEvent@SqlServer@Microsoft@@(*(A_0 + 8L) + 8L) != null)
		{
			<Module>.gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.=(A_0, <Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.->(*(A_0 + 8L) + 8L).Packages.GetEnumerator());
			return (<Module>.gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>..PE$AAU?$IEnumerator@PE$AAUIPackage@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@(A_0) != null) ? 1 : 0;
		}
		return 1;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0001052C File Offset: 0x0001052C
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.PackageEnumerator.GetNextPackage(XEventInteropMetadataAdapter.PackageEnumerator* A_0, XEPackageMetadata** metadata)
	{
		if (<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>..PE$AAVXEventInteropMetadataGeneration@Internal@Linq@XEvent@SqlServer@Microsoft@@(*(A_0 + 8L) + 8L) != null && <Module>.gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.->(A_0).MoveNext())
		{
			XEventInteropPackage xeventInteropPackage = <Module>.gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.->(A_0).Current;
			*metadata = xeventInteropPackage.GetPackageMd();
		}
		else
		{
			*metadata = 0L;
		}
		return 1;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00010C40 File Offset: 0x00010C40
	internal unsafe static XE_PackageFilter* Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.PackageEnumerator.GetPackageFilter(XEventInteropMetadataAdapter.PackageEnumerator* A_0)
	{
		XE_PackageFilter* ptr = *(A_0 + 16L);
		XE_PackageFilter* ptr2 = <Module>.@new(104UL);
		XE_PackageFilter* ptr3;
		try
		{
			if (ptr2 != null)
			{
				ptr3 = <Module>.XE_PackageFilter.{ctor}(ptr2);
			}
			else
			{
				ptr3 = null;
			}
		}
		catch
		{
			<Module>.delete((void*)ptr2, 104UL);
			throw;
		}
		*(A_0 + 16L) = ptr3;
		if (ptr != null)
		{
			<Module>.XE_PackageFilter.{dtor}(ptr);
			<Module>.delete((void*)ptr, 104UL);
		}
		XEPackageMetadata* packageMd = <Module>.gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.->(A_0).Current.GetPackageMd();
		<Module>.XE_PackageFilter.Init(*(A_0 + 16L), packageMd);
		<Module>.XE_PackageFilter.Clear(*(A_0 + 16L));
		XEPackageMetadata* ptr4 = packageMd + 8L / (long)sizeof(XEPackageMetadata);
		ulong num = 8UL;
		do
		{
			XE_TCollection<1,0> xe_TCollection<1,0>;
			<Module>.XE_TCollection<1,0>.{ctor}(ref xe_TCollection<1,0>, *(long*)ptr4);
			uint num2 = 0U;
			if (0 < <Module>.XE_TCollection<1,0>.GetCount(ref xe_TCollection<1,0>))
			{
				do
				{
					XEObject* ptr5 = <Module>.XE_TCollection<1,0>.Get(ref xe_TCollection<1,0>, num2);
					if (ptr5 != null)
					{
						<Module>.XE_PackageFilter.Include(*(A_0 + 16L), *(XERelativeObjectId*)(ptr5 + 4L / (long)sizeof(XEObject)));
					}
					num2 += 1U;
				}
				while (num2 < <Module>.XE_TCollection<1,0>.GetCount(ref xe_TCollection<1,0>));
			}
			ptr4 += 8L / (long)sizeof(XEPackageMetadata);
			num -= 1UL;
		}
		while (num > 0UL);
		return *(A_0 + 16L);
	}

	// Token: 0x0600003E RID: 62 RVA: 0x00010D4C File Offset: 0x00010D4C
	internal unsafe static void* Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.__delDtor(XEventInteropMetadataAdapter* A_0, uint A_0)
	{
		try
		{
			try
			{
				<Module>.std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.{dtor}(A_0 + 16L);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.{dtor}), (void*)(A_0 + (byte*)8L));
				throw;
			}
			<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.{dtor}(A_0 + 8L);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.{dtor}), A_0);
			throw;
		}
		<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 40UL);
		}
		return A_0;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x00011FA4 File Offset: 0x00011FA4
	internal unsafe static void* Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.__vecDelDtor(XEventFileReaderMessageHandler* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XEventFileReaderMessageHandler* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 16UL, (ulong)(*ptr), ldftn(Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XEventFileReaderMessageHandler* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)(*ptr2 * 16L + 8L));
			}
			return ptr;
		}
		try
		{
			<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.{dtor}(A_0 + 8L);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_ILogReadMessageHandler.{dtor}), A_0);
			throw;
		}
		<Module>.XE_ILogRWMessageHandler.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 16UL);
		}
		return A_0;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00012034 File Offset: 0x00012034
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.{dtor}(XEventFileReaderMessageHandler* A_0)
	{
		try
		{
			<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.{dtor}(A_0 + 8L);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_ILogReadMessageHandler.{dtor}), A_0);
			throw;
		}
		<Module>.XE_ILogRWMessageHandler.{dtor}(A_0);
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000F0A0 File Offset: 0x0000F0A0
	internal unsafe static void XE_ILogReadMessageHandler.{dtor}(XE_ILogReadMessageHandler* A_0)
	{
		<Module>.XE_ILogRWMessageHandler.{dtor}(A_0);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00011ACC File Offset: 0x00011ACC
	internal unsafe static void* XE_ILogReadMessageHandler.__vecDelDtor(XE_ILogReadMessageHandler* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XE_ILogReadMessageHandler* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 8UL, (ulong)(*ptr), ldftn(XE_ILogReadMessageHandler.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XE_ILogReadMessageHandler* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)((*ptr2 + 1L) * 8L));
			}
			return ptr;
		}
		<Module>.XE_ILogRWMessageHandler.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 8UL);
		}
		return A_0;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000111D8 File Offset: 0x000111D8
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyPackageDeserialize(XEventFileReaderMessageHandler* A_0, XEPackageMetadata* packageMd, XE_LogDefaultMetadataPackageHeader* mdHeader)
	{
		<Module>.gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.->(A_0 + 8L).OnPackageDeserialize(packageMd, mdHeader);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0000F0BC File Offset: 0x0000F0BC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyOutOfMemory(XEventFileReaderMessageHandler* A_0)
	{
		throw new OutOfMemoryException();
	}

	// Token: 0x06000045 RID: 69 RVA: 0x0000F0D8 File Offset: 0x0000F0D8
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyInvalidParameter(XEventFileReaderMessageHandler* A_0, char* paramName, ulong value)
	{
		throw new ArgumentException(Marshal.PtrToStringUni((IntPtr)paramName));
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0000F0FC File Offset: 0x0000F0FC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyCanonicalizePathError(XEventFileReaderMessageHandler* A_0, char* paramName, ulong value)
	{
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt64), A_0, paramName, value, (IntPtr)(*(*A_0 + 16L)));
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000F120 File Offset: 0x0000F120
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyFileOpenError(XEventFileReaderMessageHandler* A_0, char* filePathName, uint error)
	{
		string text = Marshal.PtrToStringUni((IntPtr)filePathName);
		throw new EventFileIOException(string.Format(Resources.GetString("FileReadExceptionString"), error, text), error, text);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000F160 File Offset: 0x0000F160
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyFileAccessError(XEventFileReaderMessageHandler* A_0, uint error, char* fileName)
	{
		string text = Marshal.PtrToStringUni((IntPtr)fileName);
		throw new EventFileIOException(string.Format(Resources.GetString("FileReadExceptionString"), error, text), error, text);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000F1A0 File Offset: 0x0000F1A0
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyMetadataDeserializationError(XEventFileReaderMessageHandler* A_0, char* fileName)
	{
		string text = "(null)";
		if (fileName != null)
		{
			text = Marshal.PtrToStringUni((IntPtr)fileName);
		}
		throw new EventFileInvalidException(string.Format(Resources.GetString("MetadataDeserializeExceptionString"), text), text);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000F1E0 File Offset: 0x0000F1E0
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyBufferCorrupt(XEventFileReaderMessageHandler* A_0, XE_LogBufferPosition* pos)
	{
		string text = Marshal.PtrToStringUni((IntPtr)(*pos));
		throw new EventFileInvalidException(string.Format(Resources.GetString("CorruptBufferExceptionString"), text, <Module>.XE_LogBufferPosition.GetLocalOffset(pos)), text);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x0000F224 File Offset: 0x0000F224
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventFileReaderMessageHandler.NotifyInvalidFile(XEventFileReaderMessageHandler* A_0, char* filePathName)
	{
		string text = Marshal.PtrToStringUni((IntPtr)filePathName);
		throw new EventFileInvalidException(string.Format(Resources.GetString("FileInvalidExceptionString"), text), text);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x0000F284 File Offset: 0x0000F284
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.?A0xd4db69fe.AssertionHandler(sbyte* failing_expression, sbyte* filename, int linenumber, sbyte* szDesc)
	{
		string text = Marshal.PtrToStringAnsi((IntPtr)((void*)failing_expression));
		string text2 = null;
		if (szDesc != null)
		{
			Marshal.PtrToStringAnsi((IntPtr)((void*)szDesc));
		}
		string text3 = Marshal.PtrToStringAnsi((IntPtr)((void*)filename));
		object[] array = new object[4];
		array[0] = text;
		array[1] = text3;
		array[2] = linenumber;
		array[1] = text2;
		Debug.Assert(false, string.Format("Assertion Failed {0} at {1}:{2}\r\n{3}", new object[] { text, text3, linenumber, text2 }));
	}

	// Token: 0x0600004D RID: 77 RVA: 0x0000F40C File Offset: 0x0000F40C
	[SecuritySafeCritical]
	internal unsafe static XEventInteropFileReader gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.->(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000F438 File Offset: 0x0000F438
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.=(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>* A_0, XEventInteropFileReader t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x0000F464 File Offset: 0x0000F464
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.{dtor}(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x0000F494 File Offset: 0x0000F494
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>.{ctor}(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropFileReader\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x0000F40C File Offset: 0x0000F40C
	[SecuritySafeCritical]
	internal unsafe static XEventInteropMetadataGeneration gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.->(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x0000F40C File Offset: 0x0000F40C
	[SecuritySafeCritical]
	internal unsafe static XEventInteropMetadataGeneration gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>..PE$AAVXEventInteropMetadataGeneration@Internal@Linq@XEvent@SqlServer@Microsoft@@(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x0000F438 File Offset: 0x0000F438
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.=(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>* A_0, XEventInteropMetadataGeneration t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0000F4C0 File Offset: 0x0000F4C0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.{dtor}(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000F494 File Offset: 0x0000F494
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>.{ctor}(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataGeneration\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x0000F40C File Offset: 0x0000F40C
	[SecuritySafeCritical]
	internal unsafe static XEventInteropMetadataManager gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.->(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x0000F40C File Offset: 0x0000F40C
	[SecuritySafeCritical]
	internal unsafe static XEventInteropMetadataManager gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>..PE$AAVXEventInteropMetadataManager@Internal@Linq@XEvent@SqlServer@Microsoft@@(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x0000F438 File Offset: 0x0000F438
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.=(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>* A_0, XEventInteropMetadataManager t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x0000F4F0 File Offset: 0x0000F4F0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.{dtor}(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x0000F494 File Offset: 0x0000F494
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>* gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>.{ctor}(gcroot<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataManager\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000F40C File Offset: 0x0000F40C
	[SecuritySafeCritical]
	internal unsafe static IEnumerator<IPackage> gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.->(gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x0000F40C File Offset: 0x0000F40C
	[SecuritySafeCritical]
	internal unsafe static IEnumerator<IPackage> gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>..PE$AAU?$IEnumerator@PE$AAUIPackage@XEvent@SqlServer@Microsoft@@@Generic@Collections@System@@(gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000F438 File Offset: 0x0000F438
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>* gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.=(gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>* A_0, IEnumerator<IPackage> t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x0000F464 File Offset: 0x0000F464
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>.{dtor}(gcroot<System::Collections::Generic::IEnumerator<Microsoft::SqlServer::XEvent::IPackage\u0020^>\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x000131A4 File Offset: 0x000131A4
	internal unsafe static void* XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.__vecDelDtor(XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 8UL, (ulong)(*ptr), ldftn(XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)((*ptr2 + 1L) * 8L));
			}
			return ptr;
		}
		<Module>.XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 8UL);
		}
		return A_0;
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00012CB0 File Offset: 0x00012CB0
	internal unsafe static void* XE_FileReader<XE_FileReaderDefaultPolicy>.__vecDelDtor(XE_FileReader<XE_FileReaderDefaultPolicy>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XE_FileReader<XE_FileReaderDefaultPolicy>* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 72UL, (ulong)(*ptr), ldftn(XE_FileReader<XE_FileReaderDefaultPolicy>.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XE_FileReader<XE_FileReaderDefaultPolicy>* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)(*ptr2 * 72L + 8L));
			}
			return ptr;
		}
		try
		{
			try
			{
				<Module>.XE_AutoP<XE_FileSetMemoryMap>.{dtor}(A_0 + 48L);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(XE_FileReaderDefaultPolicy.{dtor}), (void*)(A_0 + (byte*)32L));
				throw;
			}
			XE_FileReaderDefaultPolicy* ptr3 = A_0 + 32L;
			<Module>.XE_AutoP<XE_FileSet>.{dtor}(ptr3);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_IDecoder.{dtor}), A_0);
			throw;
		}
		<Module>.XE_IDecoder.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 72UL);
		}
		return A_0;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00012D78 File Offset: 0x00012D78
	internal unsafe static void XE_FileReader<XE_FileReaderDefaultPolicy>.{dtor}(XE_FileReader<XE_FileReaderDefaultPolicy>* A_0)
	{
		try
		{
			try
			{
				<Module>.XE_AutoP<XE_FileSetMemoryMap>.{dtor}(A_0 + 48L);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(XE_FileReaderDefaultPolicy.{dtor}), (void*)(A_0 + (byte*)32L));
				throw;
			}
			XE_FileReaderDefaultPolicy* ptr = A_0 + 32L;
			<Module>.XE_AutoP<XE_FileSet>.{dtor}(ptr);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(XE_IDecoder.{dtor}), A_0);
			throw;
		}
		<Module>.XE_IDecoder.{dtor}(A_0);
	}

	// Token: 0x06000062 RID: 98 RVA: 0x0001209C File Offset: 0x0001209C
	internal unsafe static void XE_FileReaderDefaultPolicy.{dtor}(XE_FileReaderDefaultPolicy* A_0)
	{
		<Module>.XE_AutoP<XE_FileSet>.{dtor}(A_0);
	}

	// Token: 0x06000063 RID: 99 RVA: 0x00012634 File Offset: 0x00012634
	internal unsafe static void* XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig_v0>.__vecDelDtor(XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig_v0>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig_v0>* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 48UL, (ulong)(*ptr), ldftn(XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig_v0>.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig_v0>* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)(*ptr2 * 48L + 8L));
			}
			return ptr;
		}
		<Module>.XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 48UL);
		}
		return A_0;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000122EC File Offset: 0x000122EC
	internal unsafe static void XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig_v0>.{dtor}(XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig_v0>* A_0)
	{
		<Module>.XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(A_0);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00012274 File Offset: 0x00012274
	internal unsafe static XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{ctor}(XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* A_0)
	{
		*A_0 = ref <Module>.??_7?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@6B@;
		return A_0;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x00012290 File Offset: 0x00012290
	internal unsafe static void* XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig>.__vecDelDtor(XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig>* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 48UL, (ulong)(*ptr), ldftn(XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig>.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig>* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)(*ptr2 * 48L + 8L));
			}
			return ptr;
		}
		<Module>.XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 48UL);
		}
		return A_0;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000122EC File Offset: 0x000122EC
	internal unsafe static void XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig>.{dtor}(XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig>* A_0)
	{
		<Module>.XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(A_0);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x00014FFC File Offset: 0x00014FFC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.SetBufferWriter(XEventInteropMetadataAdapter* A_0, XE_IBufferWriter* pWriter)
	{
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00015788 File Offset: 0x00015788
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.OnLogCreate(XEventInteropMetadataAdapter* A_0, void* cookie)
	{
		return 1;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00014500 File Offset: 0x00014500
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyRWLockError(XEventSerializerMessageHandler* A_0, XERWMode mode)
	{
		Debug.Assert(false, "Should never be called");
	}

	// Token: 0x0600006B RID: 107 RVA: 0x0000F0BC File Offset: 0x0000F0BC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyOutOfMemory(XEventSerializerMessageHandler* A_0)
	{
		throw new OutOfMemoryException();
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000F0BC File Offset: 0x0000F0BC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyInitError(XEventSerializerMessageHandler* A_0)
	{
		throw new OutOfMemoryException();
	}

	// Token: 0x0600006D RID: 109 RVA: 0x0000F0D8 File Offset: 0x0000F0D8
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyInvalidParameter(XEventSerializerMessageHandler* A_0, char* paramName, ulong value)
	{
		throw new ArgumentException(Marshal.PtrToStringUni((IntPtr)paramName));
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000F0FC File Offset: 0x0000F0FC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyCanonicalizePathError(XEventSerializerMessageHandler* A_0, char* paramName, ulong value)
	{
		calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt64), A_0, paramName, value, (IntPtr)(*(*A_0 + 16L)));
	}

	// Token: 0x0600006F RID: 111 RVA: 0x000145A0 File Offset: 0x000145A0
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyLogWriteError(XEventSerializerMessageHandler* A_0, uint error, ulong offset, uint length, char* fileName)
	{
		IntPtr intPtr = (IntPtr)fileName;
		throw new EventFileIOException(string.Format(Resources.GetString("FileWriteExceptionString"), error, Marshal.PtrToStringUni(intPtr)));
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00014520 File Offset: 0x00014520
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyLogCreateError(XEventSerializerMessageHandler* A_0, uint error, char* filePathName, ushort errorState)
	{
		IntPtr intPtr = (IntPtr)filePathName;
		throw new EventFileIOException(string.Format(Resources.GetString("FileWriteExceptionString"), error, Marshal.PtrToStringUni(intPtr)), error);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x0001455C File Offset: 0x0001455C
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyLogSetSizeError(XEventSerializerMessageHandler* A_0, uint error, ulong length, char* fileName)
	{
		IntPtr intPtr = (IntPtr)fileName;
		throw new EventFileIOException(string.Format(Resources.GetString("FileSizeExceptionString"), error, Marshal.PtrToStringUni(intPtr), length));
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000145DC File Offset: 0x000145DC
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.NotifyMetadataSerializationError(XEventSerializerMessageHandler* A_0, XERelativeObjectId packageId)
	{
		object[] array = new object[0];
		throw new EventFileIOException(string.Format(Resources.GetString("MetadataSerializationExceptionString"), array));
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00013A10 File Offset: 0x00013A10
	internal unsafe static void XE_MetadataSerializer.{dtor}(XE_MetadataSerializer* A_0)
	{
		<Module>.XE_AutoRg<unsigned\u0020char>.{dtor}(A_0 + 8L);
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000144A8 File Offset: 0x000144A8
	internal unsafe static void* Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.__vecDelDtor(XEventSerializerMessageHandler* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XEventSerializerMessageHandler* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 8UL, (ulong)(*ptr), ldftn(Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XEventSerializerMessageHandler* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)((*ptr2 + 1L) * 8L));
			}
			return ptr;
		}
		<Module>.XE_ILogRWMessageHandler.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 8UL);
		}
		return A_0;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000F0A0 File Offset: 0x0000F0A0
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerMessageHandler.{dtor}(XEventSerializerMessageHandler* A_0)
	{
		<Module>.XE_ILogRWMessageHandler.{dtor}(A_0);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000F0A0 File Offset: 0x0000F0A0
	internal unsafe static void XE_ILogWriteMessageHandler.{dtor}(XE_ILogWriteMessageHandler* A_0)
	{
		<Module>.XE_ILogRWMessageHandler.{dtor}(A_0);
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00014188 File Offset: 0x00014188
	internal unsafe static void* XE_ILogWriteMessageHandler.__vecDelDtor(XE_ILogWriteMessageHandler* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XE_ILogWriteMessageHandler* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 8UL, (ulong)(*ptr), ldftn(XE_ILogWriteMessageHandler.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XE_ILogWriteMessageHandler* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)((*ptr2 + 1L) * 8L));
			}
			return ptr;
		}
		<Module>.XE_ILogRWMessageHandler.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 8UL);
		}
		return A_0;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0001487C File Offset: 0x0001487C
	internal unsafe static void* XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.__delDtor(XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>* A_0, uint A_0)
	{
		<Module>.XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(A_0 + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 3552UL);
		}
		return A_0;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000DB94 File Offset: 0x0000DB94
	internal unsafe static uint Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetMaxAffinity(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return 1;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00014A6C File Offset: 0x00014A6C
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.{dtor}(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		<Module>.XE_AutoP<XE_LogSpecs>.{dtor}(A_0 + 24L);
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00014A8C File Offset: 0x00014A8C
	internal unsafe static void XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.LogWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.LogWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* A_0)
	{
		<Module>.XE_AutoP<XE_Log>.{dtor}(A_0 + 16L);
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00013A30 File Offset: 0x00013A30
	internal unsafe static void* XE_AutoP<XE_Log>.__vecDelDtor(XE_AutoP<XE_Log>* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			XE_AutoP<XE_Log>* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 8UL, (ulong)(*ptr), ldftn(XE_AutoP<XE_Log>.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				XE_AutoP<XE_Log>* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)((*ptr2 + 1L) * 8L));
			}
			return ptr;
		}
		<Module>.XE_AutoP<XE_Log>.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 8UL);
		}
		return A_0;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00014FE0 File Offset: 0x00014FE0
	internal unsafe static void XE_AutoP<XE_Log>.__dflt_ctor_closure(XE_AutoP<XE_Log>* A_0)
	{
		<Module>.XE_AutoP<XE_Log>.{ctor}(A_0, null);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00015178 File Offset: 0x00015178
	internal unsafe static ulong* Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetLogMaxSizeInBytes(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return <Module>.XE_FileWriterDefaultPolicy<1,0>.GetLogMaxSizeInBytes(A_0);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x0001515C File Offset: 0x0001515C
	internal unsafe static uint* Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetLogMaxRollover(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return <Module>.XE_FileWriterDefaultPolicy<1,0>.GetLogMaxRollover(A_0);
	}

	// Token: 0x06000080 RID: 128 RVA: 0x00015140 File Offset: 0x00015140
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.IsAsyncIo(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return <Module>.XE_FileWriterDefaultPolicy<1,0>.IsAsyncIo(A_0);
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00015124 File Offset: 0x00015124
	internal unsafe static uint Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetSectorSize(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, uint affinity)
	{
		return <Module>.XE_FileWriterDefaultPolicy<1,0>.GetSectorSize(A_0, affinity);
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00015108 File Offset: 0x00015108
	internal unsafe static uint Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetCurrentLogCount(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, uint affinity)
	{
		return <Module>.XE_FileWriterDefaultPolicy<1,0>.GetCurrentLogCount(A_0, affinity);
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00016590 File Offset: 0x00016590
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.OnRetry(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000156F8 File Offset: 0x000156F8
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.IsRetryNeeded(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return 0;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x000150E4 File Offset: 0x000150E4
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.UpdateLogTime(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, XE_Log* log, XEBufferHeader* buffer)
	{
		if (log != null)
		{
			_SYSTEMTIME systemtime;
			<Module>.GetSystemTime(&systemtime);
			<Module>.XE_Log.SetLogTime(log, systemtime);
		}
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000DB94 File Offset: 0x0000DB94
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.IsAccessAllowed(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return 1;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x0001657C File Offset: 0x0001657C
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.IsRolloverNeeded(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, XE_Log* log, XEBufferHeader* buffer)
	{
		return 0;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00015010 File Offset: 0x00015010
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.Initialize(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, ushort nCAttr, XECustomizableAttribute* cAttrs, XE_ILogWriteMessageHandler* msgHandler, [MarshalAs(UnmanagedType.U1)] bool isLocalSecondaryReplica)
	{
		*(A_0 + 3184L) = 0;
		XE_CustomizableAttributes xe_CustomizableAttributes;
		<Module>.XE_CustomizableAttributes.{ctor}(ref xe_CustomizableAttributes, cAttrs, nCAttr);
		*(A_0 + 3192L) = <Module>.XE_CustomizableAttributes.GetValue<void\u0020*>(ref xe_CustomizableAttributes, ref <Module>.??_C@_1DA@KBKPOKBL@?$AAS?$AAe?$AAr?$AAi?$AAa?$AAl?$AAi?$AAz?$AAe?$AAr?$AAP?$AAo?$AAl?$AAi?$AAc@, null);
		char* ptr = <Module>.XE_CustomizableAttributes.GetValue<wchar_t\u0020const\u0020*\u0020const>(ref xe_CustomizableAttributes, <Module>.?XE_File_LogFileName@XE_FileTargetParams@@2QEB_WEB, 0L);
		XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* ptr2 = A_0 + 1624L;
		if (<Module>.StringCchCopyW(ptr2, 260UL, ptr) >= 0 && <Module>.PathRemoveFileSpecW(ptr2) != null)
		{
			char* ptr3 = <Module>.PathFindFileNameW(ptr);
			XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* ptr4 = A_0 + 2144L;
			if (<Module>.StringCchCopyW(ptr4, 260UL, ptr3) >= 0)
			{
				char* ptr5 = <Module>.PathFindExtensionW(ptr4);
				bool flag = <Module>.StringCchCopyW(A_0 / 2 + 2664L, 260UL, ptr5) >= 0;
				<Module>.PathRemoveExtensionW(ptr4);
				if (flag && <Module>.XE_FileWriterDefaultPolicy<1,0>.Initialize(A_0, nCAttr, cAttrs, msgHandler, isLocalSecondaryReplica, (XE_FileSourceType)1) != null)
				{
					return 1;
				}
			}
		}
		return 0;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00014A30 File Offset: 0x00014A30
	internal unsafe static XE_IBufferWriter* XE_IBufferWriter.{ctor}(XE_IBufferWriter* A_0)
	{
		*A_0 = ref <Module>.??_7XE_IBufferWriter@@6B@;
		return A_0;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00014A4C File Offset: 0x00014A4C
	internal unsafe static XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.{ctor}(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		<Module>.XE_FileWriterDefaultPolicy<1,0>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x0001572C File Offset: 0x0001572C
	internal unsafe static ulong* Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetLogGrowthSizeInBytes(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return <Module>.XE_FileWriterDefaultPolicy<1,0>.GetLogGrowthSizeInBytes(A_0);
	}

	// Token: 0x0600008C RID: 140 RVA: 0x00015800 File Offset: 0x00015800
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.CanCreateFile(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, uint affinity, int isDiskFull)
	{
		return <Module>.XE_FileWriterDefaultPolicy<1,0>.CanCreateFile(A_0, affinity, isDiskFull);
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0001581C File Offset: 0x0001581C
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetLogPathToDelete(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, uint affinity, char* logPathName, uint logPathNameLengthInWChars, uint* pFileCount)
	{
		return 0;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00015844 File Offset: 0x00015844
	internal unsafe static char* Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetLogPath(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return 0L;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x00015788 File Offset: 0x00015788
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.OnLogRollover(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, long currentLogIndex)
	{
		return 1;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00015788 File Offset: 0x00015788
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.OnLogDelete(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, char* logPathName)
	{
		return 1;
	}

	// Token: 0x06000091 RID: 145 RVA: 0x00015830 File Offset: 0x00015830
	internal unsafe static void Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.OnLogClose(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, uint affinity, XE_Log* log)
	{
	}

	// Token: 0x06000092 RID: 146 RVA: 0x000156F8 File Offset: 0x000156F8
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.IsForcedUnitAccess(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return 0;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x000156F8 File Offset: 0x000156F8
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetLogFileOverwrite(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return 0;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000DB94 File Offset: 0x0000DB94
	internal unsafe static uint Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetBlobShareModeOnCreation(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0)
	{
		return 1;
	}

	// Token: 0x06000095 RID: 149 RVA: 0x0001570C File Offset: 0x0001570C
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetIndexFilePathName(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, char* logPathName, uint logPathNameLengthInWChars, _SYSTEMTIME* logTime, XE_FileSourceType fileSourceType)
	{
		return <Module>.XE_FileWriterDefaultPolicy<1,0>.GetIndexFilePathName(A_0, logPathName, logPathNameLengthInWChars, logTime, fileSourceType);
	}

	// Token: 0x06000096 RID: 150 RVA: 0x0001579C File Offset: 0x0001579C
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.GetLogPathName(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, uint affinity, uint index, char* logPathName, uint logPathNameLengthInWChars, _SYSTEMTIME* logTime)
	{
		int num;
		if (<Module>.StringCchPrintfW(logPathName, (ulong)logPathNameLengthInWChars, (char*)(&<Module>.??_C@_1BG@BKMFGADG@?$AA?$CF?$AAs?$AA?2?$AA?$CF?$AAs?$AA_?$AA?$CF?$AAd?$AA?$CF?$AAs@), __arglist(A_0 / sizeof(ushort) + 1624L, A_0 / sizeof(ushort) + 2144L, (uint)(*(A_0 + 3184L)), A_0 / sizeof(ushort) + 2664L)) >= 0)
		{
			num = 1;
			*(A_0 + 3184L) = *(A_0 + 3184L) + 1;
		}
		else
		{
			num = 0;
		}
		return num;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x00015748 File Offset: 0x00015748
	internal unsafe static int Microsoft.SqlServer.XEvent.Linq.Internal.XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>.OnLogCreate(XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>* A_0, uint affinity, uint index, char* logPathName, XE_Log* log)
	{
		ulong num = (ulong)(*(A_0 + 3192L));
		int num2;
		if (num != 0UL && calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.Char modopt(System.Runtime.CompilerServices.IsConst)*), logPathName, (IntPtr)num) != null && <Module>.XE_FileWriterDefaultPolicy<1,0>.OnLogCreate(A_0, affinity, index, logPathName, null) != null)
		{
			num2 = 1;
		}
		else
		{
			num2 = 0;
		}
		return num2;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0001533C File Offset: 0x0001533C
	internal unsafe static XE_ILogWriter* XE_ILogWriter.{ctor}(XE_ILogWriter* A_0)
	{
		*A_0 = ref <Module>.??_7XE_ILogWriter@@6B@;
		return A_0;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00015BBC File Offset: 0x00015BBC
	internal unsafe static void* XE_AutoP<XE_Log>.__delDtor(XE_AutoP<XE_Log>* A_0, uint A_0)
	{
		<Module>.XE_AutoP<XE_Log>.{dtor}(A_0);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 8UL);
		}
		return A_0;
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00019330 File Offset: 0x00019330
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(Exception innerException, Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x0600009B RID: 155 RVA: 0x00018D08 File Offset: 0x00018D08
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00018D24 File Offset: 0x00018D24
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00018E54 File Offset: 0x00018E54
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00018E74 File Offset: 0x00018E74
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid guid2 = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return guid2;
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00018ECC File Offset: 0x00018ECC
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		ICorRuntimeHost* ptr = null;
		int num;
		try
		{
			Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), guid).ToPointer();
			goto IL_0036;
		}
		catch (Exception ex)
		{
			num = Marshal.GetHRForException(ex);
		}
		if (num < 0)
		{
			return num;
		}
		IL_0036:
		long num2 = *(*(long*)ptr + 104L);
		num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr, ppUnk, (IntPtr)num2);
		ICorRuntimeHost* ptr2 = ptr;
		uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		return num;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00018F54 File Offset: 0x00018F54
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, (IntPtr)(*(*(long*)ppUnk + 16L)));
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00018F78 File Offset: 0x00018F78
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* ptr = null;
		int num = <Module>.__get_default_appdomain(&ptr);
		if (num >= 0)
		{
			try
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				return (AppDomain)Marshal.GetObjectForIUnknown(intPtr);
			}
			finally
			{
				<Module>.__release_appdomain(ptr);
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00018FE0 File Offset: 0x00018FE0
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(delegate* unmanaged[Cdecl, Cdecl]<void*, int> function, void* cookie)
	{
		Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), guid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num = *(*(long*)ptr + 64L);
			uint id = (uint)appDomain.Id;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, id, function, cookie, (IntPtr)num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr2 = ptr;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		}
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x00019074 File Offset: 0x00019074
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		uint _scrt_native_dllmain_reason = <Module>.__scrt_native_dllmain_reason;
		if (_scrt_native_dllmain_reason != 0U && _scrt_native_dllmain_reason != 1U)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000190B4 File Offset: 0x000190B4
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000190D8 File Offset: 0x000190D8
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xc_mp_a);
			if ((ref <Module>.__xc_mp_a) < (ref <Module>.__xc_mp_z))
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xc_mp_z))
					{
						goto IL_0035;
					}
				}
				<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0035:
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00019134 File Offset: 0x00019134
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xi_a);
			if ((ref <Module>.__xi_a) < (ref <Module>.__xi_z))
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xi_z))
					{
						goto IL_0035;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0035:
			void** ptr2 = (void**)(&<Module>.__xc_a);
			if ((ref <Module>.__xc_a) < (ref <Module>.__xc_z))
			{
				while (*(long*)ptr2 == 0L)
				{
					ptr2 += 8L / (long)sizeof(void*);
					if (ptr2 >= (void**)(&<Module>.__xc_z))
					{
						goto IL_0062;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0062:
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x000191BC File Offset: 0x000191BC
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int num;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() != null && !<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) || (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() != null && !<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA && <Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0))
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x000191FC File Offset: 0x000191FC
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00019214 File Offset: 0x00019214
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x060000AA RID: 170 RVA: 0x000010F4 File Offset: 0x000010F4
	internal static void ?A0xba6ca20d.??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x00001110 File Offset: 0x00001110
	internal static void ?A0xba6ca20d.??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000112C File Offset: 0x0000112C
	internal static void ?A0xba6ca20d.??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00001148 File Offset: 0x00001148
	internal static void ?A0xba6ca20d.??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x00001164 File Offset: 0x00001164
	internal static void ?A0xba6ca20d.??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060000AF RID: 175 RVA: 0x00001180 File Offset: 0x00001180
	internal static void ?A0xba6ca20d.??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x0000119C File Offset: 0x0000119C
	internal static void ?A0xba6ca20d.??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x0001938C File Offset: 0x0001938C
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xi_vt_a), (delegate*<void*>*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x000193C8 File Offset: 0x000193C8
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x060000B3 RID: 179 RVA: 0x000193EC File Offset: 0x000193EC
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during native initialization.\n");
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.__scrt_is_safe_for_managed_code() == null)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)1)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)1;
			if (<Module>._initterm_e((delegate* unmanaged[Cdecl, Cdecl]<int>*)(&<Module>.__xi_a), (delegate* unmanaged[Cdecl, Cdecl]<int>*)(&<Module>.__xi_z)) != 0)
			{
				<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0));
			}
			<Module>._initterm((delegate* unmanaged[Cdecl, Cdecl]<void>*)(&<Module>.__xc_a), (delegate* unmanaged[Cdecl, Cdecl]<void>*)(&<Module>.__xc_z));
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		}
	}

	// Token: 0x060000B4 RID: 180 RVA: 0x00019480 File Offset: 0x00019480
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xc_mp_a), (delegate*<void*>*)(&<Module>.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x060000B5 RID: 181 RVA: 0x000194C8 File Offset: 0x000194C8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xc_ma_a), (delegate*<void*>*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x00019508 File Offset: 0x00019508
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00019538 File Offset: 0x00019538
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = <Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA || <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					void* ptr2 = Interlocked.CompareExchange(ref <Module>.__scrt_native_startup_lock, ptr, 0L);
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
				{
					<Module>.Sleep(1000);
				}
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
			if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerProcess(A_0);
			}
			else
			{
				num3 = ((<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != 0) ? 1 : num3);
			}
		}
		finally
		{
			if (num == 0)
			{
				Interlocked.Exchange(ref <Module>.__scrt_native_startup_lock, 0L);
			}
		}
		if (num3 != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x00019234 File Offset: 0x00019234
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x0001924C File Offset: 0x0001924C
	[SecurityCritical]
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x0001928C File Offset: 0x0001928C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsUninitialization() != null)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(null);
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
			}
		}
	}

	// Token: 0x060000BB RID: 187 RVA: 0x000192C8 File Offset: 0x000192C8
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[PrePrepareMethod]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object A_0, EventArgs A_1)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			byte b = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0);
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (b != 0)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00019644 File Offset: 0x00019644
	[DebuggerStepThrough]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, Exception innerException)
	{
		try
		{
			bool flag = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0) != 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (Exception ex)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, ex);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x060000BD RID: 189 RVA: 0x000196C0 File Offset: 0x000196C0
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x060000BE RID: 190 RVA: 0x000196E0 File Offset: 0x000196E0
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x060000BF RID: 191 RVA: 0x000196FC File Offset: 0x000196FC
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA);
				flag = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (Exception ex)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, ex);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), ex);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x060000C0 RID: 192 RVA: 0x000197C0 File Offset: 0x000197C0
	[DebuggerStepThrough]
	[SecurityCritical]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x060000C1 RID: 193 RVA: 0x0000F40C File Offset: 0x0000F40C
	[SecuritySafeCritical]
	internal unsafe static string gcroot<System::String\u0020^>..PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x060000C2 RID: 194 RVA: 0x0000F438 File Offset: 0x0000F438
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.=(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x0000F464 File Offset: 0x0000F464
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<System::String\u0020^>.{dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000F494 File Offset: 0x0000F494
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.{ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x00019840 File Offset: 0x00019840
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[HandleProcessCorruptedStateExceptions]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(delegate*<void*, void> pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x00019928 File Offset: 0x00019928
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void __ehvec_dtor(void* ptr, ulong size, ulong count, delegate*<void*, void> destructor)
	{
		bool flag = false;
		ptr = (void*)(size * count + (byte*)ptr);
		try
		{
			for (;;)
			{
				long num = (long)count;
				count -= 1UL;
				if (num == 0L)
				{
					break;
				}
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
			flag = true;
		}
		finally
		{
			if (!flag)
			{
				<Module>.__ArrayUnwind(ptr, size, count, destructor);
			}
		}
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x0001988C File Offset: 0x0001988C
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static int ?A0x0b550e27.ArrayUnwindFilter(_EXCEPTION_POINTERS* pExPtrs)
	{
		EHExceptionRecord* ptr = *(long*)pExPtrs;
		if (*(int*)ptr != -529697949)
		{
			return 0;
		}
		*<Module>.__current_exception() = ptr;
		long num = *(long*)(pExPtrs + 8L / (long)sizeof(_EXCEPTION_POINTERS));
		*<Module>.__current_exception_context() = num;
		<Module>.terminate();
		return 0;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x000198C8 File Offset: 0x000198C8
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void __ArrayUnwind(void* ptr, ulong size, ulong count, delegate*<void*, void> destructor)
	{
		try
		{
			for (ulong num = 0UL; num != count; num += 1UL)
			{
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
		}
		catch when (endfilter(<Module>.?A0x0b550e27.ArrayUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00019990 File Offset: 0x00019990
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr intPtr = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(intPtr);
		}
		return null;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x00019C80 File Offset: 0x00019C80
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x060000CB RID: 203 RVA: 0x000199C4 File Offset: 0x000199C4
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00019A18 File Offset: 0x00019A18
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00019A44 File Offset: 0x00019A44
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x060000CE RID: 206 RVA: 0x00019A74 File Offset: 0x00019A74
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00019CA0 File Offset: 0x00019CA0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x00019A94 File Offset: 0x00019A94
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x00019CD8 File Offset: 0x00019CD8
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0x8e112e2a.__alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x00019AC0 File Offset: 0x00019AC0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void ?A0x8e112e2a.__dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00019AD8 File Offset: 0x00019AD8
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0x8e112e2a.__exit_list_size != 0UL)
		{
			delegate*<void>* ptr = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x8e112e2a.__onexitbegin_m);
			delegate*<void>* ptr2 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x8e112e2a.__onexitend_m);
			if (ptr != -1L && ptr != null && ptr2 != null)
			{
				delegate*<void>* ptr3 = ptr;
				delegate*<void>* ptr4 = ptr2;
				for (;;)
				{
					ptr2 -= 8L / (long)sizeof(delegate*<void>);
					if (ptr2 < ptr)
					{
						break;
					}
					if (*(long*)ptr2 != <Module>.EncodePointer(null))
					{
						IntPtr intPtr = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), intPtr);
						delegate*<void>* ptr5 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x8e112e2a.__onexitbegin_m);
						delegate*<void>* ptr6 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x8e112e2a.__onexitend_m);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
				IntPtr intPtr2 = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(intPtr2);
			}
			<Module>.?A0x8e112e2a.__dealloc_global_lock();
		}
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x00019CF8 File Offset: 0x00019CF8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_m()
	{
		int num = 0;
		if (<Module>.?A0x8e112e2a.__alloc_global_lock() == 1)
		{
			<Module>.?A0x8e112e2a.__onexitbegin_m = (delegate*<void>*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0x8e112e2a.__onexitend_m = <Module>.?A0x8e112e2a.__onexitbegin_m;
			<Module>.?A0x8e112e2a.__exit_list_size = 32UL;
			num = 1;
		}
		return num;
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x00019D44 File Offset: 0x00019D44
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0x8e112e2a.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (delegate*<void>*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x00019B8C File Offset: 0x00019B8C
	[HandleProcessCorruptedStateExceptions]
	[SecurityCritical]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			delegate*<void>* ptr = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			delegate*<void>* ptr2 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					delegate*<void>* ptr3 = ptr;
					delegate*<void>* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 8L / (long)sizeof(delegate*<void>);
						}
						while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						delegate*<void> system.Void_u0020() = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), system.Void_u0020());
						delegate*<void>* ptr5 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						delegate*<void>* ptr6 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
			}
			finally
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(intPtr);
				<Module>.?A0x8e112e2a.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x060000D7 RID: 215
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x060000D8 RID: 216
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x060000D9 RID: 217 RVA: 0x00019D90 File Offset: 0x00019D90
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initterm_e(delegate* unmanaged[Cdecl, Cdecl]<int>* pfbegin, delegate* unmanaged[Cdecl, Cdecl]<int>* pfend)
	{
		int num = 0;
		if (pfbegin < pfend)
		{
			while (num == 0)
			{
				ulong num2 = (ulong)(*(long*)pfbegin);
				if (num2 != 0UL)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)num2);
				}
				pfbegin += 8L / (long)sizeof(delegate* unmanaged[Cdecl, Cdecl]<int>);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00019DC4 File Offset: 0x00019DC4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void _initterm(delegate* unmanaged[Cdecl, Cdecl]<void>* pfbegin, delegate* unmanaged[Cdecl, Cdecl]<void>* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)num);
				}
				pfbegin += 8L / (long)sizeof(delegate* unmanaged[Cdecl, Cdecl]<void>);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00019DF4 File Offset: 0x00019DF4
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00019E50 File Offset: 0x00019E50
	[SecurityCritical]
	[DebuggerStepThrough]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(delegate*<void*>* pfbegin, delegate*<void*>* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					void* ptr = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(num));
				}
				pfbegin += 8L / (long)sizeof(delegate*<void*>);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x060000DD RID: 221 RVA: 0x00019E1C File Offset: 0x00019E1C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static delegate*<void*> <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(delegate*<void*> methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer()
			.ToPointer();
	}

	// Token: 0x060000DE RID: 222 RVA: 0x0001A03D File Offset: 0x0001A03D
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* PathRemoveBackslashW(char*);

	// Token: 0x060000DF RID: 223 RVA: 0x0001A09D File Offset: 0x0001A09D
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* MapViewOfFile(void*, uint, uint, uint, ulong);

	// Token: 0x060000E0 RID: 224 RVA: 0x0001A091 File Offset: 0x0001A091
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* CreateFileMappingW(void*, _SECURITY_ATTRIBUTES*, uint, uint, uint, char*);

	// Token: 0x060000E1 RID: 225 RVA: 0x0001A085 File Offset: 0x0001A085
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* PathFindExtensionW(char*);

	// Token: 0x060000E2 RID: 226 RVA: 0x0001A16B File Offset: 0x0001A16B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* _wfullpath(char*, char*, ulong);

	// Token: 0x060000E3 RID: 227 RVA: 0x0001A118 File Offset: 0x0001A118
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* new[](ulong);

	// Token: 0x060000E4 RID: 228 RVA: 0x0001A055 File Offset: 0x0001A055
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int UrlCombineW(char*, char*, char*, uint*, uint);

	// Token: 0x060000E5 RID: 229 RVA: 0x0001A0E5 File Offset: 0x0001A0E5
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int ResetEvent(void*);

	// Token: 0x060000E6 RID: 230 RVA: 0x0001773B File Offset: 0x0001773B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int CloseHandle(void*);

	// Token: 0x060000E7 RID: 231 RVA: 0x0001A079 File Offset: 0x0001A079
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int PathIsRelativeW(char*);

	// Token: 0x060000E8 RID: 232 RVA: 0x0001A06D File Offset: 0x0001A06D
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int PathCanonicalizeW(char*, char*);

	// Token: 0x060000E9 RID: 233 RVA: 0x0001A0FD File Offset: 0x0001A0FD
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void GetCurrentThreadStackLimits(ulong*, ulong*);

	// Token: 0x060000EA RID: 234 RVA: 0x0001A0F1 File Offset: 0x0001A0F1
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int SetEvent(void*);

	// Token: 0x060000EB RID: 235 RVA: 0x00017B80 File Offset: 0x00017B80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete(void*);

	// Token: 0x060000EC RID: 236 RVA: 0x0001A0D9 File Offset: 0x0001A0D9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* CreateEventW(_SECURITY_ATTRIBUTES*, int, int, char*);

	// Token: 0x060000ED RID: 237 RVA: 0x0001A0A9 File Offset: 0x0001A0A9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int UnmapViewOfFile(void*);

	// Token: 0x060000EE RID: 238 RVA: 0x0001A0CD File Offset: 0x0001A0CD
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int ReleaseMutex(void*);

	// Token: 0x060000EF RID: 239 RVA: 0x0001A0C1 File Offset: 0x0001A0C1
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint WaitForSingleObject(void*, uint);

	// Token: 0x060000F0 RID: 240 RVA: 0x0001A0B5 File Offset: 0x0001A0B5
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* CreateMutexW(_SECURITY_ATTRIBUTES*, int, char*);

	// Token: 0x060000F1 RID: 241 RVA: 0x0001A031 File Offset: 0x0001A031
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void PathRemoveExtensionW(char*);

	// Token: 0x060000F2 RID: 242 RVA: 0x0001A061 File Offset: 0x0001A061
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int PathIsURLW(char*);

	// Token: 0x060000F3 RID: 243 RVA: 0x0001A00F File Offset: 0x0001A00F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int PathRemoveFileSpecW(char*);

	// Token: 0x060000F4 RID: 244 RVA: 0x0001A003 File Offset: 0x0001A003
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* PathFindFileNameW(char*);

	// Token: 0x060000F5 RID: 245 RVA: 0x00017B3C File Offset: 0x00017B3C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* @new(ulong);

	// Token: 0x060000F6 RID: 246 RVA: 0x0001A049 File Offset: 0x0001A049
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* PathCombineW(char*, char*, char*);

	// Token: 0x060000F7 RID: 247 RVA: 0x000015D0 File Offset: 0x000015D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_OneTimeInit.InitializeBegin(XE_OneTimeInit*);

	// Token: 0x060000F8 RID: 248 RVA: 0x00001600 File Offset: 0x00001600
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_OneTimeInit.InitializeEnd(XE_OneTimeInit*);

	// Token: 0x060000F9 RID: 249 RVA: 0x00017B30 File Offset: 0x00017B30
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*);

	// Token: 0x060000FA RID: 250 RVA: 0x00001210 File Offset: 0x00001210
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int IsEqualGUID(_GUID*, _GUID*);

	// Token: 0x060000FB RID: 251 RVA: 0x00002170 File Offset: 0x00002170
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ListBase* ListBase.{ctor}(ListBase*);

	// Token: 0x060000FC RID: 252 RVA: 0x00001350 File Offset: 0x00001350
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEngineServicesAPI* XE_API.ServiceAPI();

	// Token: 0x060000FD RID: 253 RVA: 0x00002190 File Offset: 0x00002190
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong XE_VariantLoad<wchar_t\u0020const\u0020*>(char*);

	// Token: 0x060000FE RID: 254 RVA: 0x000021A0 File Offset: 0x000021A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* @new(ulong, XEEngineServicesAPI*, void*);

	// Token: 0x060000FF RID: 255 RVA: 0x000021C0 File Offset: 0x000021C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete(void*, XEEngineServicesAPI*, void*);

	// Token: 0x06000100 RID: 256 RVA: 0x00002220 File Offset: 0x00002220
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_LogDefaultMetadataPackageHeader* XE_LogDeserializedPackage.GetHeader(XE_LogDeserializedPackage*);

	// Token: 0x06000101 RID: 257 RVA: 0x00002310 File Offset: 0x00002310
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_IDecoder.{dtor}(XE_IDecoder*);

	// Token: 0x06000102 RID: 258 RVA: 0x00010230 File Offset: 0x00010230
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEPackageMetadata* Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.GetPackageMd(XEventInteropMetadataAdapter*, ushort);

	// Token: 0x06000103 RID: 259 RVA: 0x0000EE50 File Offset: 0x0000EE50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEObject* Microsoft.SqlServer.XEvent.Linq.Internal.XEventInteropMetadataAdapter.GetObj(XEventInteropMetadataAdapter*, XERelativeObjectId);

	// Token: 0x06000104 RID: 260 RVA: 0x000120D0 File Offset: 0x000120D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint* XE_FileReader<XE_FileReaderDefaultPolicy>.GetFileCount(XE_FileReader<XE_FileReaderDefaultPolicy>*);

	// Token: 0x06000105 RID: 261 RVA: 0x000120C0 File Offset: 0x000120C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_FileReader<XE_FileReaderDefaultPolicy>.SetIsSequentialScan(XE_FileReader<XE_FileReaderDefaultPolicy>*, int);

	// Token: 0x06000106 RID: 262 RVA: 0x00011990 File Offset: 0x00011990
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* XE_ParserFactory<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.Create(XE_ParserFactory<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>*, void*, XEEventBufferHeader*);

	// Token: 0x06000107 RID: 263 RVA: 0x00011AC0 File Offset: 0x00011AC0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEvent* XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventMd(XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x06000108 RID: 264 RVA: 0x00011980 File Offset: 0x00011980
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEventBufferHeader* XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventHeader(XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x06000109 RID: 265 RVA: 0x00011DC0 File Offset: 0x00011DC0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.clear(vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>*);

	// Token: 0x0600010A RID: 266 RVA: 0x00011DB0 File Offset: 0x00011DB0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.{dtor}(vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>*);

	// Token: 0x0600010B RID: 267 RVA: 0x00011D90 File Offset: 0x00011D90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>* std.vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>.{ctor}(vector<XEPackageMetadata\u0020*,std::allocator<XEPackageMetadata\u0020*>\u0020>*);

	// Token: 0x0600010C RID: 268 RVA: 0x00001850 File Offset: 0x00001850
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_TCollection<1,0>* XE_TCollection<1,0>.{ctor}(XE_TCollection<1,0>*, XEObjectCollection*);

	// Token: 0x0600010D RID: 269 RVA: 0x00001900 File Offset: 0x00001900
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XE_TCollection<1,0>.GetCount(XE_TCollection<1,0>*);

	// Token: 0x0600010E RID: 270 RVA: 0x00001920 File Offset: 0x00001920
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEObject* XE_TCollection<1,0>.Get(XE_TCollection<1,0>*, uint);

	// Token: 0x0600010F RID: 271 RVA: 0x000082B0 File Offset: 0x000082B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_TCollection<0,1>* XE_TCollection<0,1>.{ctor}(XE_TCollection<0,1>*, XEObjectCollection*);

	// Token: 0x06000110 RID: 272 RVA: 0x00012100 File Offset: 0x00012100
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void SEList<XE_LogDeserializedPackage,0>.Insert(SEList<XE_LogDeserializedPackage,0>*, XE_LogDeserializedPackage*);

	// Token: 0x06000111 RID: 273 RVA: 0x00009C50 File Offset: 0x00009C50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void SEList<XE_LogDeserializedPackage,0>.Delete(XE_LogDeserializedPackage*);

	// Token: 0x06000112 RID: 274 RVA: 0x000120E0 File Offset: 0x000120E0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_LogDeserializedPackage* SEList<XE_LogDeserializedPackage,0>.GetNext(SEList<XE_LogDeserializedPackage,0>*, XE_LogDeserializedPackage*);

	// Token: 0x06000113 RID: 275 RVA: 0x00009C70 File Offset: 0x00009C70
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_LogDeserializedPackage* SEList<XE_LogDeserializedPackage,0>.Head(SEList<XE_LogDeserializedPackage,0>*);

	// Token: 0x06000114 RID: 276 RVA: 0x000117C0 File Offset: 0x000117C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int SEList<XE_LogDeserializedPackage,0>.IsEmpty(SEList<XE_LogDeserializedPackage,0>*);

	// Token: 0x06000115 RID: 277 RVA: 0x000087D0 File Offset: 0x000087D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoRg<unsigned\u0020int>.{dtor}(XE_AutoRg<unsigned\u0020int>*);

	// Token: 0x06000116 RID: 278 RVA: 0x00011E40 File Offset: 0x00011E40
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>* XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{ctor}(XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x06000117 RID: 279 RVA: 0x00011970 File Offset: 0x00011970
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.HasNext(XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x06000118 RID: 280 RVA: 0x00011E70 File Offset: 0x00011E70
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.ResetSafe(XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*, XEventInteropMetadataAdapter*, XEBuffer*);

	// Token: 0x06000119 RID: 281 RVA: 0x00011EA0 File Offset: 0x00011EA0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.SetCurrentSafe(XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*, XEventInteropMetadataAdapter*, XEBuffer*, uint);

	// Token: 0x0600011A RID: 282 RVA: 0x000118A0 File Offset: 0x000118A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.MoveNextSafe(XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x0600011B RID: 283 RVA: 0x00011F90 File Offset: 0x00011F90
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.GetEventOffset(XE_BufferWalker<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x0600011C RID: 284 RVA: 0x00012960 File Offset: 0x00012960
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x0600011D RID: 285 RVA: 0x0000A240 File Offset: 0x0000A240
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_FileReader<XE_FileReaderDefaultPolicy>* XE_FileReader<XE_FileReaderDefaultPolicy>.{ctor}(XE_FileReader<XE_FileReaderDefaultPolicy>*, XE_IBufferMap*);

	// Token: 0x0600011E RID: 286 RVA: 0x00009B80 File Offset: 0x00009B80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_Delete<class\u0020XE_LogDeserializedPackage>(XE_LogDeserializedPackage*);

	// Token: 0x0600011F RID: 287 RVA: 0x00011810 File Offset: 0x00011810
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_AutoP<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>* XE_AutoP<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.{ctor}(XE_AutoP<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>*, XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x06000120 RID: 288 RVA: 0x00011840 File Offset: 0x00011840
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoP<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.{dtor}(XE_AutoP<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>*);

	// Token: 0x06000121 RID: 289 RVA: 0x00011F60 File Offset: 0x00011F60
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_TicksUtil* XE_TicksUtil.{ctor}<class\u0020Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>(XE_TicksUtil*, XEventInteropMetadataAdapter*);

	// Token: 0x06000122 RID: 290 RVA: 0x00012090 File Offset: 0x00012090
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern ulong XE_VariantLoad<enum\u0020XE_BufferMap::SortOptions>(XE_BufferMap.SortOptions);

	// Token: 0x06000123 RID: 291 RVA: 0x000082C0 File Offset: 0x000082C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XE_TCollection<0,1>.GetCount(XE_TCollection<0,1>*);

	// Token: 0x06000124 RID: 292 RVA: 0x000085C0 File Offset: 0x000085C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoP<XE_FileSetMemoryMap>.{dtor}(XE_AutoP<XE_FileSetMemoryMap>*);

	// Token: 0x06000125 RID: 293 RVA: 0x000086A0 File Offset: 0x000086A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoP<XE_FileSet>.{dtor}(XE_AutoP<XE_FileSet>*);

	// Token: 0x06000126 RID: 294 RVA: 0x00011D00 File Offset: 0x00011D00
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEEvent* XE_TCollection<0,1>.GetTyped<struct\u0020XEEvent>(XE_TCollection<0,1>*, uint);

	// Token: 0x06000127 RID: 295 RVA: 0x00011D30 File Offset: 0x00011D30
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEAction* XE_TCollection<0,1>.GetTyped<struct\u0020XEAction>(XE_TCollection<0,1>*, uint);

	// Token: 0x06000128 RID: 296 RVA: 0x00011D60 File Offset: 0x00011D60
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEMap* XE_TCollection<0,1>.GetTyped<struct\u0020XEMap>(XE_TCollection<0,1>*, uint);

	// Token: 0x06000129 RID: 297 RVA: 0x00008370 File Offset: 0x00008370
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoRg<XE_FileSet::Entry>.{dtor}(XE_AutoRg<XE_FileSet::Entry>*);

	// Token: 0x0600012A RID: 298 RVA: 0x00001580 File Offset: 0x00001580
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong XE_PublishedDescriptor.GetData(XE_PublishedDescriptor*);

	// Token: 0x0600012B RID: 299 RVA: 0x00003290 File Offset: 0x00003290
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_FileSetMemoryMap.{dtor}(XE_FileSetMemoryMap*);

	// Token: 0x0600012C RID: 300 RVA: 0x00007230 File Offset: 0x00007230
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_LogDefaultMetadataDecoder* XE_LogDefaultMetadataDecoder.{ctor}(XE_LogDefaultMetadataDecoder*, XE_IDecoder*);

	// Token: 0x0600012D RID: 301 RVA: 0x0001A123 File Offset: 0x0001A123
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern int __CxxQueryExceptionSize();

	// Token: 0x0600012E RID: 302 RVA: 0x00005CF0 File Offset: 0x00005CF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_LogDeserializedPackage.{dtor}(XE_LogDeserializedPackage*);

	// Token: 0x0600012F RID: 303 RVA: 0x00005F20 File Offset: 0x00005F20
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XEPackageMetadata* XE_LogDeserializedPackage.GetMetadata(XE_LogDeserializedPackage*);

	// Token: 0x06000130 RID: 304 RVA: 0x00005D50 File Offset: 0x00005D50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_LogDeserializedPackage.DeserializeMetadataBlock(byte*, uint, void*, delegate* unmanaged[Cdecl, Cdecl]<void*, XE_LogDeserializedPackage*, int>);

	// Token: 0x06000131 RID: 305 RVA: 0x00007EC0 File Offset: 0x00007EC0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_PackageFilter* XE_PackageFilter.{ctor}(XE_PackageFilter*);

	// Token: 0x06000132 RID: 306 RVA: 0x00007F80 File Offset: 0x00007F80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_PackageFilter.Init(XE_PackageFilter*, XEPackageMetadata*);

	// Token: 0x06000133 RID: 307 RVA: 0x000080D0 File Offset: 0x000080D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_PackageFilter.Clear(XE_PackageFilter*);

	// Token: 0x06000134 RID: 308 RVA: 0x00008110 File Offset: 0x00008110
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_PackageFilter.Include(XE_PackageFilter*, XERelativeObjectId);

	// Token: 0x06000135 RID: 309 RVA: 0x00006D80 File Offset: 0x00006D80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_EventLocation* XE_EventLocation.{ctor}(XE_EventLocation*);

	// Token: 0x06000136 RID: 310 RVA: 0x00006DA0 File Offset: 0x00006DA0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ushort XE_EventLocation.GetFileIndex(XE_EventLocation*);

	// Token: 0x06000137 RID: 311 RVA: 0x00006DC0 File Offset: 0x00006DC0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_EventLocation.SetFileIndex(XE_EventLocation*, ushort);

	// Token: 0x06000138 RID: 312 RVA: 0x00006DE0 File Offset: 0x00006DE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XE_EventLocation.GetEventOffset(XE_EventLocation*);

	// Token: 0x06000139 RID: 313 RVA: 0x00006DF0 File Offset: 0x00006DF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_EventLocation.SetEventOffset(XE_EventLocation*, uint);

	// Token: 0x0600013A RID: 314 RVA: 0x00006E10 File Offset: 0x00006E10
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong XE_EventLocation.GetBufferByteOffset(XE_EventLocation*);

	// Token: 0x0600013B RID: 315 RVA: 0x00006E30 File Offset: 0x00006E30
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong XE_EventLocation.GetBufferNumber(XE_EventLocation*);

	// Token: 0x0600013C RID: 316 RVA: 0x00006E50 File Offset: 0x00006E50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_EventLocation.SetBufferNumber(XE_EventLocation*, ulong);

	// Token: 0x0600013D RID: 317 RVA: 0x0001A147 File Offset: 0x0001A147
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxDetectRethrow(void*);

	// Token: 0x0600013E RID: 318 RVA: 0x0001A153 File Offset: 0x0001A153
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __CxxUnregisterExceptionObject(void*, int);

	// Token: 0x0600013F RID: 319 RVA: 0x0001A12F File Offset: 0x0001A12F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxExceptionFilter(void*, void*, int, void*);

	// Token: 0x06000140 RID: 320 RVA: 0x00017747 File Offset: 0x00017747
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern uint GetLastError();

	// Token: 0x06000141 RID: 321 RVA: 0x00017B24 File Offset: 0x00017B24
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete(void*, ulong);

	// Token: 0x06000142 RID: 322 RVA: 0x000016B0 File Offset: 0x000016B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_TicksUtil.ConvertToFileTime(XE_TicksUtil*, ulong, _FILETIME*);

	// Token: 0x06000143 RID: 323 RVA: 0x00007780 File Offset: 0x00007780
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_MetadataStore* XE_MetadataStore.{ctor}(XE_MetadataStore*, XE_ILogReadMessageHandler*);

	// Token: 0x06000144 RID: 324 RVA: 0x000077D0 File Offset: 0x000077D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_MetadataStore.{dtor}(XE_MetadataStore*);

	// Token: 0x06000145 RID: 325 RVA: 0x00001680 File Offset: 0x00001680
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern int XE_API.IsEnginePresent();

	// Token: 0x06000146 RID: 326 RVA: 0x00007DA0 File Offset: 0x00007DA0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern DeserializePackageAddResult XE_DeserializedMetadata.Merge(XEPackageMetadata*, XEPackageMetadata*);

	// Token: 0x06000147 RID: 327 RVA: 0x0001A13B File Offset: 0x0001A13B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxRegisterExceptionObject(void*, void*);

	// Token: 0x06000148 RID: 328 RVA: 0x00006E80 File Offset: 0x00006E80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong XE_LogBufferPosition.GetLocalOffset(XE_LogBufferPosition*);

	// Token: 0x06000149 RID: 329 RVA: 0x000036C0 File Offset: 0x000036C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_IBufferMap* XE_BufferMap.Create();

	// Token: 0x0600014A RID: 330 RVA: 0x00002410 File Offset: 0x00002410
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_DeserializedBuffer.GetXEBuffer(byte*, uint, XEBuffer*);

	// Token: 0x0600014B RID: 331 RVA: 0x00002480 File Offset: 0x00002480
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_LogBufferHeader* XE_DeserializedBuffer.GetLogBufferHeader(XEBuffer*);

	// Token: 0x0600014C RID: 332 RVA: 0x000024B0 File Offset: 0x000024B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_LogDefaultMetadataHeader* XE_DeserializedBuffer.GetLogMetadataHeader(XEBuffer*);

	// Token: 0x0600014D RID: 333 RVA: 0x00001790 File Offset: 0x00001790
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_Compare(XEPackageMetadata*, XEPackageMetadata*);

	// Token: 0x0600014E RID: 334 RVA: 0x00001760 File Offset: 0x00001760
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern int XE_Compare(XERelativeObjectId, XERelativeObjectId);

	// Token: 0x0600014F RID: 335 RVA: 0x000023B0 File Offset: 0x000023B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_ILogRWMessageHandler.{dtor}(XE_ILogRWMessageHandler*);

	// Token: 0x06000150 RID: 336 RVA: 0x00017B18 File Offset: 0x00017B18
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*, ulong);

	// Token: 0x06000151 RID: 337 RVA: 0x00001CB0 File Offset: 0x00001CB0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int StringCchPrintfW(char*, ulong, char*, __arglist);

	// Token: 0x06000152 RID: 338 RVA: 0x00014610 File Offset: 0x00014610
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong XE_VariantLoad<void\u0020*>(void*);

	// Token: 0x06000153 RID: 339 RVA: 0x00016360 File Offset: 0x00016360
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_Log.SetLogTime(XE_Log*, _SYSTEMTIME);

	// Token: 0x06000154 RID: 340 RVA: 0x00002340 File Offset: 0x00002340
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint* XE_MetadataSerializer.GetDataLength(XE_MetadataSerializer*);

	// Token: 0x06000155 RID: 341 RVA: 0x00014810 File Offset: 0x00014810
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ushort ?A0x938327a2.FormatLogBufferHeader(XEBuffer*);

	// Token: 0x06000156 RID: 342 RVA: 0x000147D0 File Offset: 0x000147D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void PrepareBufferForSerializer(XEBuffer*, XEBufferHeader*, uint);

	// Token: 0x06000157 RID: 343 RVA: 0x000083F0 File Offset: 0x000083F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoRg<unsigned\u0020char>.{dtor}(XE_AutoRg<unsigned\u0020char>*);

	// Token: 0x06000158 RID: 344 RVA: 0x00014620 File Offset: 0x00014620
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>* XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.Create(void*, ushort, XECustomizableAttribute*, XE_ILogWriteMessageHandler*, int, XEventInteropMetadataAdapter*, XE_IFileTargetIndexEncoder*);

	// Token: 0x06000159 RID: 345 RVA: 0x00014150 File Offset: 0x00014150
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>.WriteBufferDirect(XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>*, XEBufferHeader*, uint);

	// Token: 0x0600015A RID: 346 RVA: 0x00014850 File Offset: 0x00014850
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_Delete<class\u0020XE_LogWriter<class\u0020XE_FileWriter<class\u0020Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<class\u0020XE_FileWriterDefaultPolicy<1,0>\u0020>,class\u0020Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>\u0020>(XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>*);

	// Token: 0x0600015B RID: 347 RVA: 0x000141E0 File Offset: 0x000141E0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_MetadataSerializer.SerializeAllToBuffer<class\u0020Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>(XE_MetadataSerializer*, uint, XE_LogDefaultMetadataHeader*, XEventInteropMetadataAdapter*, XEBufferHeader**, ushort*);

	// Token: 0x0600015C RID: 348 RVA: 0x00015200 File Offset: 0x00015200
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.{dtor}(XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*);

	// Token: 0x0600015D RID: 349 RVA: 0x000164E0 File Offset: 0x000164E0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong* XE_FileWriterDefaultPolicy<1,0>.GetLogMaxSizeInBytes(XE_FileWriterDefaultPolicy<1,0>*);

	// Token: 0x0600015E RID: 350 RVA: 0x000164D0 File Offset: 0x000164D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint* XE_FileWriterDefaultPolicy<1,0>.GetLogMaxRollover(XE_FileWriterDefaultPolicy<1,0>*);

	// Token: 0x0600015F RID: 351 RVA: 0x00008430 File Offset: 0x00008430
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_AutoP<XE_Log>* XE_AutoP<XE_Log>.{ctor}(XE_AutoP<XE_Log>*, XE_Log*);

	// Token: 0x06000160 RID: 352 RVA: 0x00008460 File Offset: 0x00008460
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoP<XE_Log>.{dtor}(XE_AutoP<XE_Log>*);

	// Token: 0x06000161 RID: 353 RVA: 0x00008640 File Offset: 0x00008640
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoP<XE_LogSpecs>.{dtor}(XE_AutoP<XE_LogSpecs>*);

	// Token: 0x06000162 RID: 354 RVA: 0x00016020 File Offset: 0x00016020
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_FileWriterDefaultPolicy<1,0>.Initialize(XE_FileWriterDefaultPolicy<1,0>*, ushort, XECustomizableAttribute*, XE_ILogWriteMessageHandler*, [MarshalAs(UnmanagedType.U1)] bool, XE_FileSourceType);

	// Token: 0x06000163 RID: 355 RVA: 0x00016370 File Offset: 0x00016370
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XE_FileWriterDefaultPolicy<1,0>.GetCurrentLogCount(XE_FileWriterDefaultPolicy<1,0>*, uint);

	// Token: 0x06000164 RID: 356 RVA: 0x00016490 File Offset: 0x00016490
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint XE_FileWriterDefaultPolicy<1,0>.GetSectorSize(XE_FileWriterDefaultPolicy<1,0>*, uint);

	// Token: 0x06000165 RID: 357 RVA: 0x000164C0 File Offset: 0x000164C0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_FileWriterDefaultPolicy<1,0>.IsAsyncIo(XE_FileWriterDefaultPolicy<1,0>*);

	// Token: 0x06000166 RID: 358 RVA: 0x00015FF0 File Offset: 0x00015FF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* XE_CustomizableAttributes.GetValue<void\u0020*>(XE_CustomizableAttributes*, char*, void*);

	// Token: 0x06000167 RID: 359 RVA: 0x00009AE0 File Offset: 0x00009AE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* XE_CustomizableAttributes.GetValue<wchar_t\u0020const\u0020*\u0020const>(XE_CustomizableAttributes*, char*, char*);

	// Token: 0x06000168 RID: 360 RVA: 0x00016AC0 File Offset: 0x00016AC0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_FileWriterDefaultPolicy<1,0>.OnLogCreate(XE_FileWriterDefaultPolicy<1,0>*, uint, uint, char*, XE_Log*);

	// Token: 0x06000169 RID: 361 RVA: 0x00016AB0 File Offset: 0x00016AB0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ulong* XE_FileWriterDefaultPolicy<1,0>.GetLogGrowthSizeInBytes(XE_FileWriterDefaultPolicy<1,0>*);

	// Token: 0x0600016A RID: 362 RVA: 0x000156B0 File Offset: 0x000156B0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_FileWriterDefaultPolicy<1,0>* XE_FileWriterDefaultPolicy<1,0>.{ctor}(XE_FileWriterDefaultPolicy<1,0>*);

	// Token: 0x0600016B RID: 363 RVA: 0x00016AD0 File Offset: 0x00016AD0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_FileWriterDefaultPolicy<1,0>.CanCreateFile(XE_FileWriterDefaultPolicy<1,0>*, uint, int);

	// Token: 0x0600016C RID: 364 RVA: 0x00016A80 File Offset: 0x00016A80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int XE_FileWriterDefaultPolicy<1,0>.GetIndexFilePathName(XE_FileWriterDefaultPolicy<1,0>*, char*, uint, _SYSTEMTIME*, XE_FileSourceType);

	// Token: 0x0600016D RID: 365 RVA: 0x00008290 File Offset: 0x00008290
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CAutoHandleInternal<-1,&CloseHandle>.{dtor}(CAutoHandleInternal<-1,&CloseHandle>*);

	// Token: 0x0600016E RID: 366 RVA: 0x000082E0 File Offset: 0x000082E0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoResource<void\u0020*,168>.{dtor}(XE_AutoResource<void\u0020*,168>*);

	// Token: 0x0600016F RID: 367 RVA: 0x00008310 File Offset: 0x00008310
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoResource<void\u0020*,200>.{dtor}(XE_AutoResource<void\u0020*,200>*);

	// Token: 0x06000170 RID: 368 RVA: 0x00008340 File Offset: 0x00008340
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoResource<void\u0020*,256>.{dtor}(XE_AutoResource<void\u0020*,256>*);

	// Token: 0x06000171 RID: 369 RVA: 0x00008600 File Offset: 0x00008600
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void XE_AutoRg<XE_LogSpecs::LogSpec>.{dtor}(XE_AutoRg<XE_LogSpecs::LogSpec>*);

	// Token: 0x06000172 RID: 370 RVA: 0x0001A01B File Offset: 0x0001A01B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void GetSystemTime(_SYSTEMTIME*);

	// Token: 0x06000173 RID: 371 RVA: 0x00001460 File Offset: 0x00001460
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_CustomizableAttributes* XE_CustomizableAttributes.{ctor}(XE_CustomizableAttributes*, XECustomizableAttribute*, ushort);

	// Token: 0x06000174 RID: 372 RVA: 0x000054E0 File Offset: 0x000054E0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern XE_MetadataSerializer* XE_MetadataSerializer.{ctor}(XE_MetadataSerializer*);

	// Token: 0x06000175 RID: 373 RVA: 0x000190A0 File Offset: 0x000190A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000176 RID: 374 RVA: 0x00019FF7 File Offset: 0x00019FF7
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _cexit();

	// Token: 0x06000177 RID: 375 RVA: 0x0001A109 File Offset: 0x0001A109
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void Sleep(uint);

	// Token: 0x06000178 RID: 376 RVA: 0x0001A177 File Offset: 0x0001A177
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void abort();

	// Token: 0x06000179 RID: 377 RVA: 0x00018650 File Offset: 0x00018650
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void __security_init_cookie();

	// Token: 0x0600017A RID: 378 RVA: 0x0001A15F File Offset: 0x0001A15F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x0600017B RID: 379 RVA: 0x00019F07 File Offset: 0x00019F07
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void** __current_exception_context();

	// Token: 0x0600017C RID: 380 RVA: 0x00019F5B File Offset: 0x00019F5B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void terminate();

	// Token: 0x0600017D RID: 381 RVA: 0x00019EFB File Offset: 0x00019EFB
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void** __current_exception();

	// Token: 0x04000001 RID: 1 RVA: 0x000223E0 File Offset: 0x000213E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00$$CB_W ??_C@_11LOCGONAA@@;

	// Token: 0x04000002 RID: 2 RVA: 0x000214F0 File Offset: 0x000204F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BC@$$CB_W ??_C@_1CE@DNHAICD@?$AAX?$AAE?$AAE?$AAn?$AAg?$AAi?$AAn?$AAe?$AAC?$AAl?$AAi?$AAe?$AAn?$AAt?$AAA@;

	// Token: 0x04000003 RID: 3 RVA: 0x00021518 File Offset: 0x00020518
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BE@$$CB_W ??_C@_1CI@FENJONKI@?$AAX?$AAE?$AAE?$AAn?$AAg?$AAi?$AAn?$AAe?$AAS?$AAe?$AAr?$AAv?$AAi?$AAc?$AAe@;

	// Token: 0x04000004 RID: 4 RVA: 0x00021540 File Offset: 0x00020540
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BE@$$CB_W ??_C@_1CI@DIEFHCOF@?$AAX?$AAE?$AAE?$AAn?$AAg?$AAi?$AAn?$AAe?$AAR?$AAe?$AAg?$AAi?$AAs?$AAt?$AAe@;

	// Token: 0x04000005 RID: 5 RVA: 0x00044108 File Offset: 0x00043108
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2XE_IFeatureSwitches@@8;

	// Token: 0x04000006 RID: 6 RVA: 0x000223E8 File Offset: 0x000213E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_INT32;

	// Token: 0x04000007 RID: 7 RVA: 0x00049020 File Offset: 0x00048020
	// Note: this field is marked with 'hasfieldrva'.
	internal static XECollectedActionData ?A0xee63309e.NULLCAD;

	// Token: 0x04000008 RID: 8 RVA: 0x00049BD8 File Offset: 0x00048BD8
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor_v0 NULLADD_v0;

	// Token: 0x04000009 RID: 9 RVA: 0x00049C90 File Offset: 0x00048C90
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_24 ??_R0?AVFeatureSwitchStub@@@8;

	// Token: 0x0400000A RID: 10 RVA: 0x00044130 File Offset: 0x00043130
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XE_IFeatureSwitches@@8;

	// Token: 0x0400000B RID: 11 RVA: 0x00049010 File Offset: 0x00048010
	// Note: this field is marked with 'hasfieldrva'.
	internal static FeatureSwitchStub g_FeatureSwitchStub;

	// Token: 0x0400000C RID: 12 RVA: 0x000440E0 File Offset: 0x000430E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@FeatureSwitchStub@@8;

	// Token: 0x0400000D RID: 13 RVA: 0x00049CB8 File Offset: 0x00048CB8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_26 ??_R0?AVXE_IFeatureSwitches@@@8;

	// Token: 0x0400000E RID: 14 RVA: 0x000223D8 File Offset: 0x000213D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_PTR;

	// Token: 0x0400000F RID: 15 RVA: 0x00044170 File Offset: 0x00043170
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3FeatureSwitchStub@@8;

	// Token: 0x04000010 RID: 16 RVA: 0x000223E4 File Offset: 0x000213E4
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_VLD_CALLSTACK;

	// Token: 0x04000011 RID: 17 RVA: 0x00044188 File Offset: 0x00043188
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4FeatureSwitchStub@@6B@;

	// Token: 0x04000012 RID: 18 RVA: 0x00049018 File Offset: 0x00048018
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor ?A0xee63309e.NULLADD;

	// Token: 0x04000013 RID: 19 RVA: 0x00049008 File Offset: 0x00048008
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7FeatureSwitchStub@@6B@;

	// Token: 0x04000014 RID: 20 RVA: 0x00044118 File Offset: 0x00043118
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XE_IFeatureSwitches@@8;

	// Token: 0x04000015 RID: 21 RVA: 0x000223DC File Offset: 0x000213DC
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_WSTR;

	// Token: 0x04000016 RID: 22 RVA: 0x00044158 File Offset: 0x00043158
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2FeatureSwitchStub@@8;

	// Token: 0x04000017 RID: 23 RVA: 0x000223EC File Offset: 0x000213EC
	// Note: this field is marked with 'hasfieldrva'.
	internal static XERelativeObjectId XET_NONE;

	// Token: 0x04000018 RID: 24 RVA: 0x00049030 File Offset: 0x00048030
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<FeatureSwitchStub*, XE_FeatureSwitchId, bool> __m2mep@?Enabled@FeatureSwitchStub@@$$FEEAA_NW4XE_FeatureSwitchId@@@Z;

	// Token: 0x04000019 RID: 25 RVA: 0x00049040 File Offset: 0x00048040
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, uint, void*> __m2mep@?MemAlloc@XEAPI_Stubs@@$$FSAPEAXPEAXI@Z;

	// Token: 0x0400001A RID: 26 RVA: 0x00049050 File Offset: 0x00048050
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?MemFree@XEAPI_Stubs@@$$FSAXQEAX@Z;

	// Token: 0x0400001B RID: 27 RVA: 0x00049060 File Offset: 0x00048060
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, _SECURITY_ATTRIBUTES*, uint, uint, uint, char*, void*> __m2mep@?CreateSection@XEAPI_Stubs@@$$FSAPEAXPEAXPEAU_SECURITY_ATTRIBUTES@@KKKPEB_W@Z;

	// Token: 0x0400001C RID: 28 RVA: 0x00049070 File Offset: 0x00048070
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, uint, uint, uint, ulong, void*> __m2mep@?MapSectionView@XEAPI_Stubs@@$$FSAPEAXPEAXKKK_K@Z;

	// Token: 0x0400001D RID: 29 RVA: 0x00049080 File Offset: 0x00048080
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void*, ulong, int> __m2mep@?UnmapSectionView@XEAPI_Stubs@@$$FSAHPEAX0_K@Z;

	// Token: 0x0400001E RID: 30 RVA: 0x00049090 File Offset: 0x00048090
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?DestroySection@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x0400001F RID: 31 RVA: 0x000490A0 File Offset: 0x000480A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<int> __m2mep@?IsInitialized@XEAPI_Stubs@@$$FSAHXZ;

	// Token: 0x04000020 RID: 32 RVA: 0x000490B0 File Offset: 0x000480B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void**, int> __m2mep@?CreateXEMutex@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;

	// Token: 0x04000021 RID: 33 RVA: 0x000490C0 File Offset: 0x000480C0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?DestroyMutex@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000022 RID: 34 RVA: 0x000490D0 File Offset: 0x000480D0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, uint, XEWaitType, XEWaitResult> __m2mep@?EnterMutex@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXIW4XEWaitType@@@Z;

	// Token: 0x04000023 RID: 35 RVA: 0x000490E0 File Offset: 0x000480E0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?ReleaseXEMutex@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000024 RID: 36 RVA: 0x000490F0 File Offset: 0x000480F0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void**, int> __m2mep@?CreateRWLock@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;

	// Token: 0x04000025 RID: 37 RVA: 0x00049100 File Offset: 0x00048100
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?DestroyRWLock@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000026 RID: 38 RVA: 0x00049110 File Offset: 0x00048110
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, XERWMode, uint, XEWaitResult> __m2mep@?EnterRWLock@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXW4XERWMode@@I@Z;

	// Token: 0x04000027 RID: 39 RVA: 0x00049120 File Offset: 0x00048120
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, XERWMode, void> __m2mep@?ReleaseRWLock@XEAPI_Stubs@@$$FSAXPEAXW4XERWMode@@@Z;

	// Token: 0x04000028 RID: 40 RVA: 0x00049130 File Offset: 0x00048130
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void**, int, int> __m2mep@?CreateEventManual@XEAPI_Stubs@@$$FSAHPEAPEAXH@Z;

	// Token: 0x04000029 RID: 41 RVA: 0x00049140 File Offset: 0x00048140
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?DestroyEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x0400002A RID: 42 RVA: 0x00049150 File Offset: 0x00048150
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, uint, XEWaitResult> __m2mep@?WaitEventManual@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXI@Z;

	// Token: 0x0400002B RID: 43 RVA: 0x00049160 File Offset: 0x00048160
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?ResetEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x0400002C RID: 44 RVA: 0x00049170 File Offset: 0x00048170
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?SignalEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x0400002D RID: 45 RVA: 0x00049180 File Offset: 0x00048180
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?IsSignaledEventManual@XEAPI_Stubs@@$$FSAHPEAX@Z;

	// Token: 0x0400002E RID: 46 RVA: 0x00049190 File Offset: 0x00048190
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void**, int> __m2mep@?CreateAsyncIORequest@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;

	// Token: 0x0400002F RID: 47 RVA: 0x000491A0 File Offset: 0x000481A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void> __m2mep@?DestroyAsyncIORequest@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000030 RID: 48 RVA: 0x000491B0 File Offset: 0x000481B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void*, ulong, void*, delegate* unmanaged[Cdecl, Cdecl]<void*, void>, void> __m2mep@?ResetAsyncIORequest@XEAPI_Stubs@@$$FSAXPEAX0_K0P6AX0@Z@Z;

	// Token: 0x04000031 RID: 49 RVA: 0x000491C0 File Offset: 0x000481C0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, _OVERLAPPED*> __m2mep@?GetAsyncIORequestOverlappedPtr@XEAPI_Stubs@@$$FSAPEAU_OVERLAPPED@@PEAX@Z;

	// Token: 0x04000032 RID: 50 RVA: 0x000491D0 File Offset: 0x000481D0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, void*> __m2mep@?GetAsyncIORequestUserData@XEAPI_Stubs@@$$FSAPEAXPEAX@Z;

	// Token: 0x04000033 RID: 51 RVA: 0x000491E0 File Offset: 0x000481E0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<uint> __m2mep@?GetPartitionID@XEAPI_Stubs@@$$FSAIXZ;

	// Token: 0x04000034 RID: 52 RVA: 0x000491F0 File Offset: 0x000481F0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, ulong, bool> __m2mep@?HasHttpsPrefix@XEAPI_Stubs@@$$FSA_NPEB_W_K@Z;

	// Token: 0x04000035 RID: 53 RVA: 0x00049290 File Offset: 0x00048290
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, char*, int> __m2mep@?XEDefaultPathCanonicalize@XEAPI_Stubs@@$$FSAHPEA_WPEB_W@Z;

	// Token: 0x04000036 RID: 54 RVA: 0x00049200 File Offset: 0x00048200
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, ulong, int> __m2mep@?XEDefaultPathRemoveExtension@XEAPI_Stubs@@$$FSAHPEA_W_K@Z;

	// Token: 0x04000037 RID: 55 RVA: 0x00049210 File Offset: 0x00048210
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, ulong, int> __m2mep@?XEDefaultPathRemoveBackslash@XEAPI_Stubs@@$$FSAHPEA_W_K@Z;

	// Token: 0x04000038 RID: 56 RVA: 0x000492A0 File Offset: 0x000482A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, char*, ulong, int> __m2mep@?XEDefaultPathRemoveFileSpec@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;

	// Token: 0x04000039 RID: 57 RVA: 0x000492B0 File Offset: 0x000482B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, char*, ulong, int> __m2mep@?XEDefaultPathFindExtension@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;

	// Token: 0x0400003A RID: 58 RVA: 0x000492C0 File Offset: 0x000482C0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, char*, ulong, int> __m2mep@?XEDefaultPathFindFileName@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;

	// Token: 0x0400003B RID: 59 RVA: 0x00049220 File Offset: 0x00048220
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, ulong, char*, char*, int> __m2mep@?XEDefaultPathCombine@XEAPI_Stubs@@$$FSAHPEA_W_KPEB_W2@Z;

	// Token: 0x0400003C RID: 60 RVA: 0x00049230 File Offset: 0x00048230
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, ulong, char*, char*, int> __m2mep@?XEDefaultURLCombine@XEAPI_Stubs@@$$FSAHPEA_W_KPEB_W2@Z;

	// Token: 0x0400003D RID: 61 RVA: 0x00049240 File Offset: 0x00048240
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, int> __m2mep@?XEDefaultPathIsURL@XEAPI_Stubs@@$$FSAHPEB_W@Z;

	// Token: 0x0400003E RID: 62 RVA: 0x00049250 File Offset: 0x00048250
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_IFeatureSwitches*> __m2mep@?GetFeatureSwitches@XEAPI_Stubs@@$$FSAPEAVXE_IFeatureSwitches@@XZ;

	// Token: 0x0400003F RID: 63 RVA: 0x000492D0 File Offset: 0x000482D0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<char*, char*, int, int, int> __m2mep@?XEDefaultMultiTenantPathCanonicalize@XEAPI_Stubs@@$$FSAHPEA_WPEB_WHH@Z;

	// Token: 0x04000040 RID: 64 RVA: 0x00049260 File Offset: 0x00048260
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XESessionProperties*, int> __m2mep@?XEDefaultCheckSessionFilters@XEAPI_Stubs@@$$FSAHAEBUXESessionProperties@@@Z;

	// Token: 0x04000041 RID: 65 RVA: 0x00049270 File Offset: 0x00048270
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<int> __m2mep@?IsOperationAborted@XEAPI_Stubs@@$$FSAHXZ;

	// Token: 0x04000042 RID: 66 RVA: 0x00049280 File Offset: 0x00048280
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<ulong, int> __m2mep@?IsStackAvailable@XEAPI_Stubs@@$$FSAH_K@Z;

	// Token: 0x04000043 RID: 67 RVA: 0x00021568 File Offset: 0x00020568
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?MemAlloc@XEAPI_Stubs@@$$FSAPEAXPEAXI@Z;

	// Token: 0x04000044 RID: 68 RVA: 0x00021570 File Offset: 0x00020570
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?MemFree@XEAPI_Stubs@@$$FSAXQEAX@Z;

	// Token: 0x04000045 RID: 69 RVA: 0x00021580 File Offset: 0x00020580
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?CreateSection@XEAPI_Stubs@@$$FSAPEAXPEAXPEAU_SECURITY_ATTRIBUTES@@KKKPEB_W@Z;

	// Token: 0x04000046 RID: 70 RVA: 0x00021588 File Offset: 0x00020588
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?MapSectionView@XEAPI_Stubs@@$$FSAPEAXPEAXKKK_K@Z;

	// Token: 0x04000047 RID: 71 RVA: 0x00021590 File Offset: 0x00020590
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?UnmapSectionView@XEAPI_Stubs@@$$FSAHPEAX0_K@Z;

	// Token: 0x04000048 RID: 72 RVA: 0x00021598 File Offset: 0x00020598
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DestroySection@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000049 RID: 73 RVA: 0x000215A0 File Offset: 0x000205A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?IsInitialized@XEAPI_Stubs@@$$FSAHXZ;

	// Token: 0x0400004A RID: 74 RVA: 0x000215B0 File Offset: 0x000205B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?CreateXEMutex@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;

	// Token: 0x0400004B RID: 75 RVA: 0x000215B8 File Offset: 0x000205B8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DestroyMutex@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x0400004C RID: 76 RVA: 0x000215C0 File Offset: 0x000205C0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?EnterMutex@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXIW4XEWaitType@@@Z;

	// Token: 0x0400004D RID: 77 RVA: 0x000215C8 File Offset: 0x000205C8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?ReleaseXEMutex@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x0400004E RID: 78 RVA: 0x000215D0 File Offset: 0x000205D0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?CreateRWLock@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;

	// Token: 0x0400004F RID: 79 RVA: 0x000215D8 File Offset: 0x000205D8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DestroyRWLock@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000050 RID: 80 RVA: 0x000215E0 File Offset: 0x000205E0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?EnterRWLock@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXW4XERWMode@@I@Z;

	// Token: 0x04000051 RID: 81 RVA: 0x000215E8 File Offset: 0x000205E8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?ReleaseRWLock@XEAPI_Stubs@@$$FSAXPEAXW4XERWMode@@@Z;

	// Token: 0x04000052 RID: 82 RVA: 0x000215F0 File Offset: 0x000205F0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?CreateEventManual@XEAPI_Stubs@@$$FSAHPEAPEAXH@Z;

	// Token: 0x04000053 RID: 83 RVA: 0x000215F8 File Offset: 0x000205F8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DestroyEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000054 RID: 84 RVA: 0x00021600 File Offset: 0x00020600
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?WaitEventManual@XEAPI_Stubs@@$$FSA?AW4XEWaitResult@@PEAXI@Z;

	// Token: 0x04000055 RID: 85 RVA: 0x00021608 File Offset: 0x00020608
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?ResetEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000056 RID: 86 RVA: 0x00021610 File Offset: 0x00020610
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?SignalEventManual@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x04000057 RID: 87 RVA: 0x00021618 File Offset: 0x00020618
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?IsSignaledEventManual@XEAPI_Stubs@@$$FSAHPEAX@Z;

	// Token: 0x04000058 RID: 88 RVA: 0x00021620 File Offset: 0x00020620
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?CreateAsyncIORequest@XEAPI_Stubs@@$$FSAHPEAPEAX@Z;

	// Token: 0x04000059 RID: 89 RVA: 0x00021628 File Offset: 0x00020628
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DestroyAsyncIORequest@XEAPI_Stubs@@$$FSAXPEAX@Z;

	// Token: 0x0400005A RID: 90 RVA: 0x00021630 File Offset: 0x00020630
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?ResetAsyncIORequest@XEAPI_Stubs@@$$FSAXPEAX0_K0P6AX0@Z@Z;

	// Token: 0x0400005B RID: 91 RVA: 0x00021638 File Offset: 0x00020638
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?GetAsyncIORequestOverlappedPtr@XEAPI_Stubs@@$$FSAPEAU_OVERLAPPED@@PEAX@Z;

	// Token: 0x0400005C RID: 92 RVA: 0x00021640 File Offset: 0x00020640
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?GetAsyncIORequestUserData@XEAPI_Stubs@@$$FSAPEAXPEAX@Z;

	// Token: 0x0400005D RID: 93 RVA: 0x00021650 File Offset: 0x00020650
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?GetPartitionID@XEAPI_Stubs@@$$FSAIXZ;

	// Token: 0x0400005E RID: 94 RVA: 0x00021658 File Offset: 0x00020658
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?HasHttpsPrefix@XEAPI_Stubs@@$$FSA_NPEB_W_K@Z;

	// Token: 0x0400005F RID: 95 RVA: 0x00021660 File Offset: 0x00020660
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultPathRemoveExtension@XEAPI_Stubs@@$$FSAHPEA_W_K@Z;

	// Token: 0x04000060 RID: 96 RVA: 0x00021668 File Offset: 0x00020668
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultPathRemoveBackslash@XEAPI_Stubs@@$$FSAHPEA_W_K@Z;

	// Token: 0x04000061 RID: 97 RVA: 0x00021688 File Offset: 0x00020688
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultPathCombine@XEAPI_Stubs@@$$FSAHPEA_W_KPEB_W2@Z;

	// Token: 0x04000062 RID: 98 RVA: 0x00021690 File Offset: 0x00020690
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultURLCombine@XEAPI_Stubs@@$$FSAHPEA_W_KPEB_W2@Z;

	// Token: 0x04000063 RID: 99 RVA: 0x00021698 File Offset: 0x00020698
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultPathIsURL@XEAPI_Stubs@@$$FSAHPEB_W@Z;

	// Token: 0x04000064 RID: 100 RVA: 0x000215A8 File Offset: 0x000205A8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?GetFeatureSwitches@XEAPI_Stubs@@$$FSAPEAVXE_IFeatureSwitches@@XZ;

	// Token: 0x04000065 RID: 101 RVA: 0x000216A8 File Offset: 0x000206A8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultCheckSessionFilters@XEAPI_Stubs@@$$FSAHAEBUXESessionProperties@@@Z;

	// Token: 0x04000066 RID: 102 RVA: 0x000216B0 File Offset: 0x000206B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?IsOperationAborted@XEAPI_Stubs@@$$FSAHXZ;

	// Token: 0x04000067 RID: 103 RVA: 0x000216B8 File Offset: 0x000206B8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?IsStackAvailable@XEAPI_Stubs@@$$FSAH_K@Z;

	// Token: 0x04000068 RID: 104 RVA: 0x00021648 File Offset: 0x00020648
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultPathCanonicalize@XEAPI_Stubs@@$$FSAHPEA_WPEB_W@Z;

	// Token: 0x04000069 RID: 105 RVA: 0x00021670 File Offset: 0x00020670
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultPathRemoveFileSpec@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;

	// Token: 0x0400006A RID: 106 RVA: 0x00021678 File Offset: 0x00020678
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultPathFindExtension@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;

	// Token: 0x0400006B RID: 107 RVA: 0x00021680 File Offset: 0x00020680
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultPathFindFileName@XEAPI_Stubs@@$$FSAHPEB_WPEA_W_K@Z;

	// Token: 0x0400006C RID: 108 RVA: 0x000216A0 File Offset: 0x000206A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?XEDefaultMultiTenantPathCanonicalize@XEAPI_Stubs@@$$FSAHPEA_WPEB_WHH@Z;

	// Token: 0x0400006D RID: 109 RVA: 0x00021578 File Offset: 0x00020578
	// Note: this field is marked with 'hasfieldrva'.
	unsafe static int** __unep@?CreateFileW@@$$J0YAPEAXPEB_WKKPEAU_SECURITY_ATTRIBUTES@@KKPEAX@Z;

	// Token: 0x0400006E RID: 110 RVA: 0x000223E2 File Offset: 0x000213E2
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00$$CBD ??_C@_00CNPNBAHC@@;

	// Token: 0x0400006F RID: 111 RVA: 0x00021730 File Offset: 0x00020730
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@EOODALEL@Unknown?5exception@;

	// Token: 0x04000070 RID: 112 RVA: 0x000218C0 File Offset: 0x000208C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BF@$$CBD ??_C@_0BF@KINCDENJ@bad?5array?5new?5length@;

	// Token: 0x04000071 RID: 113 RVA: 0x00021888 File Offset: 0x00020888
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BA@$$CBD ??_C@_0BA@FOIKENOD@vector?5too?5long@;

	// Token: 0x04000072 RID: 114 RVA: 0x000496B8 File Offset: 0x000486B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY06Q6AXXZ ??_7XE_IDecoder@@6B@;

	// Token: 0x04000073 RID: 115 RVA: 0x00044558 File Offset: 0x00043558
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig@@@@6B@;

	// Token: 0x04000074 RID: 116 RVA: 0x00044200 File Offset: 0x00043200
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4exception@std@@6B@;

	// Token: 0x04000075 RID: 117 RVA: 0x000442D0 File Offset: 0x000432D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2bad_array_new_length@std@@8;

	// Token: 0x04000076 RID: 118 RVA: 0x00049C08 File Offset: 0x00048C08
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVbad_alloc@std@@@8;

	// Token: 0x04000077 RID: 119 RVA: 0x00044528 File Offset: 0x00043528
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig@@@@8;

	// Token: 0x04000078 RID: 120 RVA: 0x00044650 File Offset: 0x00043650
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XE_IDecoder@@8;

	// Token: 0x04000079 RID: 121 RVA: 0x000445C0 File Offset: 0x000435C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig_v0@@@@8;

	// Token: 0x0400007A RID: 122 RVA: 0x000443D0 File Offset: 0x000433D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2XE_ILogReadMessageHandler@@8;

	// Token: 0x0400007B RID: 123 RVA: 0x00049700 File Offset: 0x00048700
	// Note: this field is marked with 'hasfieldrva'.
	internal static XECollectedActionData ?A0xd4db69fe.NULLCAD;

	// Token: 0x0400007C RID: 124 RVA: 0x00049630 File Offset: 0x00048630
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY08Q6AXXZ ??_7?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@6B@;

	// Token: 0x0400007D RID: 125 RVA: 0x00049358 File Offset: 0x00048358
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0M@Q6AXXZ ??_7XE_ILogReadMessageHandler@@6B@;

	// Token: 0x0400007E RID: 126 RVA: 0x000444E8 File Offset: 0x000434E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@8;

	// Token: 0x0400007F RID: 127 RVA: 0x00044250 File Offset: 0x00043250
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2bad_alloc@std@@8;

	// Token: 0x04000080 RID: 128 RVA: 0x00049518 File Offset: 0x00048518
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY08Q6AXXZ ??_7?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig@@@@6B@;

	// Token: 0x04000081 RID: 129 RVA: 0x000445A8 File Offset: 0x000435A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig_v0@@@@8;

	// Token: 0x04000082 RID: 130 RVA: 0x00044600 File Offset: 0x00043600
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$XE_FileReader@VXE_FileReaderDefaultPolicy@@@@8;

	// Token: 0x04000083 RID: 131 RVA: 0x00049488 File Offset: 0x00048488
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0M@Q6AXXZ ??_7XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@6B@;

	// Token: 0x04000084 RID: 132 RVA: 0x000479A8 File Offset: 0x000469A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__CatchableTypeArray$_extraBytes_24 _CTA3?AVbad_array_new_length@std@@;

	// Token: 0x04000085 RID: 133 RVA: 0x00044368 File Offset: 0x00043368
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XE_ILogRWMessageHandler@@8;

	// Token: 0x04000086 RID: 134 RVA: 0x00049318 File Offset: 0x00048318
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_array_new_length@std@@6B@;

	// Token: 0x04000087 RID: 135 RVA: 0x00049F98 File Offset: 0x00048F98
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_18 ??_R0?AVXE_IDecoder@@@8;

	// Token: 0x04000088 RID: 136 RVA: 0x000445D8 File Offset: 0x000435D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig_v0@@@@6B@;

	// Token: 0x04000089 RID: 137 RVA: 0x000441B0 File Offset: 0x000431B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@exception@std@@8;

	// Token: 0x0400008A RID: 138 RVA: 0x00044380 File Offset: 0x00043380
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XE_ILogRWMessageHandler@@6B@;

	// Token: 0x0400008B RID: 139 RVA: 0x000444D8 File Offset: 0x000434D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@8;

	// Token: 0x0400008C RID: 140 RVA: 0x000443E8 File Offset: 0x000433E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XE_ILogReadMessageHandler@@8;

	// Token: 0x0400008D RID: 141 RVA: 0x00049CE8 File Offset: 0x00048CE8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_30 ??_R0?AVXE_ILogRWMessageHandler@@@8;

	// Token: 0x0400008E RID: 142 RVA: 0x00044228 File Offset: 0x00043228
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_alloc@std@@8;

	// Token: 0x0400008F RID: 143 RVA: 0x000495D8 File Offset: 0x000485D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY06Q6AXXZ ??_7?$XE_FileReader@VXE_FileReaderDefaultPolicy@@@@6B@;

	// Token: 0x04000090 RID: 144 RVA: 0x00049DB0 File Offset: 0x00048DB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_119 ??_R0?AV?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig@@@@@8;

	// Token: 0x04000091 RID: 145 RVA: 0x00044678 File Offset: 0x00043678
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2?$XE_FileReader@VXE_FileReaderDefaultPolicy@@@@8;

	// Token: 0x04000092 RID: 146 RVA: 0x00049300 File Offset: 0x00048300
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7bad_alloc@std@@6B@;

	// Token: 0x04000093 RID: 147 RVA: 0x000441D8 File Offset: 0x000431D8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2exception@std@@8;

	// Token: 0x04000094 RID: 148 RVA: 0x00044628 File Offset: 0x00043628
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2XE_IDecoder@@8;

	// Token: 0x04000095 RID: 149 RVA: 0x00049D18 File Offset: 0x00048D18
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_32 ??_R0?AVXE_ILogReadMessageHandler@@@8;

	// Token: 0x04000096 RID: 150 RVA: 0x00044450 File Offset: 0x00043450
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x04000097 RID: 151 RVA: 0x00044470 File Offset: 0x00043470
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x04000098 RID: 152 RVA: 0x000216C8 File Offset: 0x000206C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID ?A0xd4db69fe._GUID_NULL;

	// Token: 0x04000099 RID: 153 RVA: 0x00044580 File Offset: 0x00043580
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig_v0@@@@8;

	// Token: 0x0400009A RID: 154 RVA: 0x000492E8 File Offset: 0x000482E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7exception@std@@6B@;

	// Token: 0x0400009B RID: 155 RVA: 0x000446F8 File Offset: 0x000436F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XE_IDecoder@@6B@;

	// Token: 0x0400009C RID: 156 RVA: 0x00044428 File Offset: 0x00043428
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x0400009D RID: 157 RVA: 0x00049EC0 File Offset: 0x00048EC0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_122 ??_R0?AV?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig_v0@@@@@8;

	// Token: 0x0400009E RID: 158 RVA: 0x00044400 File Offset: 0x00043400
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XE_ILogReadMessageHandler@@6B@;

	// Token: 0x0400009F RID: 159 RVA: 0x000441E8 File Offset: 0x000431E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3exception@std@@8;

	// Token: 0x040000A0 RID: 160 RVA: 0x00044500 File Offset: 0x00043500
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@8;

	// Token: 0x040000A1 RID: 161 RVA: 0x00049E40 File Offset: 0x00048E40
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_101 ??_R0?AV?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@@8;

	// Token: 0x040000A2 RID: 162 RVA: 0x00049560 File Offset: 0x00048560
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY08Q6AXXZ ??_7?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig_v0@@@@6B@;

	// Token: 0x040000A3 RID: 163 RVA: 0x000446A8 File Offset: 0x000436A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4?$XE_FileReader@VXE_FileReaderDefaultPolicy@@@@6B@;

	// Token: 0x040000A4 RID: 164 RVA: 0x00044638 File Offset: 0x00043638
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XE_IDecoder@@8;

	// Token: 0x040000A5 RID: 165 RVA: 0x000496F8 File Offset: 0x000486F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor ?A0xd4db69fe.NULLADD;

	// Token: 0x040000A6 RID: 166 RVA: 0x00047930 File Offset: 0x00046930
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVbad_array_new_length@std@@@8??0bad_array_new_length@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x040000A7 RID: 167 RVA: 0x000446D0 File Offset: 0x000436D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@6B@;

	// Token: 0x040000A8 RID: 168 RVA: 0x00044330 File Offset: 0x00043330
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XE_ILogRWMessageHandler@@8;

	// Token: 0x040000A9 RID: 169 RVA: 0x000216C0 File Offset: 0x000206C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _Fake_allocator std.?A0xd4db69fe._Fake_alloc;

	// Token: 0x040000AA RID: 170 RVA: 0x00049330 File Offset: 0x00048330
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY04Q6AXXZ ??_7XE_ILogRWMessageHandler@@6B@;

	// Token: 0x040000AB RID: 171 RVA: 0x00044280 File Offset: 0x00043280
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_alloc@std@@6B@;

	// Token: 0x040000AC RID: 172 RVA: 0x000442F0 File Offset: 0x000432F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_array_new_length@std@@8;

	// Token: 0x040000AD RID: 173 RVA: 0x00044358 File Offset: 0x00043358
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2XE_ILogRWMessageHandler@@8;

	// Token: 0x040000AE RID: 174 RVA: 0x00044268 File Offset: 0x00043268
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3bad_alloc@std@@8;

	// Token: 0x040000AF RID: 175 RVA: 0x00049D50 File Offset: 0x00048D50
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_78 ??_R0?AVXEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@@8;

	// Token: 0x040000B0 RID: 176 RVA: 0x00049F50 File Offset: 0x00048F50
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_52 ??_R0?AV?$XE_FileReader@VXE_FileReaderDefaultPolicy@@@@@8;

	// Token: 0x040000B1 RID: 177 RVA: 0x00044308 File Offset: 0x00043308
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4bad_array_new_length@std@@6B@;

	// Token: 0x040000B2 RID: 178 RVA: 0x000443A8 File Offset: 0x000433A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XE_ILogReadMessageHandler@@8;

	// Token: 0x040000B3 RID: 179 RVA: 0x000442A8 File Offset: 0x000432A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@bad_array_new_length@std@@8;

	// Token: 0x040000B4 RID: 180 RVA: 0x00047958 File Offset: 0x00046958
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVbad_alloc@std@@@8??0bad_alloc@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x040000B5 RID: 181 RVA: 0x00047980 File Offset: 0x00046980
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVexception@std@@@8??0exception@std@@$$FQEAA@AEBV01@@Z24;

	// Token: 0x040000B6 RID: 182 RVA: 0x000479C8 File Offset: 0x000469C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__ThrowInfo _TI3?AVbad_array_new_length@std@@;

	// Token: 0x040000B7 RID: 183 RVA: 0x00049BE0 File Offset: 0x00048BE0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVexception@std@@@8;

	// Token: 0x040000B8 RID: 184 RVA: 0x00044488 File Offset: 0x00043488
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@6B@;

	// Token: 0x040000B9 RID: 185 RVA: 0x00044540 File Offset: 0x00043540
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig@@@@8;

	// Token: 0x040000BA RID: 186 RVA: 0x00044690 File Offset: 0x00043690
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$XE_FileReader@VXE_FileReaderDefaultPolicy@@@@8;

	// Token: 0x040000BB RID: 187 RVA: 0x00049C30 File Offset: 0x00048C30
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_31 ??_R0?AVbad_array_new_length@std@@@8;

	// Token: 0x040000BC RID: 188 RVA: 0x000444B0 File Offset: 0x000434B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig@@@@8;

	// Token: 0x040000BD RID: 189 RVA: 0x00049618 File Offset: 0x00048618
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter*, ushort, XEPackageMetadata*> __m2mep@?GetEventPackageMd@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAQEBUXEPackageMetadata@@G@Z;

	// Token: 0x040000BE RID: 190 RVA: 0x00049608 File Offset: 0x00048608
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, XE_LogDeserializedPackage*, void> __m2mep@?DeserializePackageCallbackHoop@?A0xd4db69fe@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FYAXPEAXPEAVXE_LogDeserializedPackage@@@Z;

	// Token: 0x040000BF RID: 191 RVA: 0x00049430 File Offset: 0x00048430
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter*, ushort, XEPackageMetadata*> __m2mep@?GetPackageMdManaged@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAQEBUXEPackageMetadata@@G@Z;

	// Token: 0x040000C0 RID: 192 RVA: 0x00049440 File Offset: 0x00048440
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter*, XETicksConfig*, void> __m2mep@?GetTicksConfig@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAXPEAUXETicksConfig@@@Z;

	// Token: 0x040000C1 RID: 193 RVA: 0x00049450 File Offset: 0x00048450
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter.PackageEnumerator*, XEventInteropMetadataAdapter*, XEventInteropMetadataAdapter.PackageEnumerator*> __m2mep@??0PackageEnumerator@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAA@PEAV123456@@Z;

	// Token: 0x040000C2 RID: 194 RVA: 0x00049500 File Offset: 0x00048500
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter.PackageEnumerator*, void> __m2mep@??1PackageEnumerator@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAA@XZ;

	// Token: 0x040000C3 RID: 195 RVA: 0x00049460 File Offset: 0x00048460
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter.PackageEnumerator*, int> __m2mep@?Begin@PackageEnumerator@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHXZ;

	// Token: 0x040000C4 RID: 196 RVA: 0x00049470 File Offset: 0x00048470
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter.PackageEnumerator*, XEPackageMetadata**, int> __m2mep@?GetNextPackage@PackageEnumerator@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHQEAPEBUXEPackageMetadata@@@Z;

	// Token: 0x040000C5 RID: 197 RVA: 0x000494E0 File Offset: 0x000484E0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter.PackageEnumerator*, XE_PackageFilter*> __m2mep@?GetPackageFilter@PackageEnumerator@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAPEBVXE_PackageFilter@@XZ;

	// Token: 0x040000C6 RID: 198 RVA: 0x000495C0 File Offset: 0x000485C0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, uint, void*> __m2mep@??_EXEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAPEAXI@Z;

	// Token: 0x040000C7 RID: 199 RVA: 0x000495A0 File Offset: 0x000485A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_ILogReadMessageHandler*, uint, void*> __m2mep@??_EXE_ILogReadMessageHandler@@$$FUEAAPEAXI@Z;

	// Token: 0x040000C8 RID: 200 RVA: 0x000494F0 File Offset: 0x000484F0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, XEPackageMetadata*, XE_LogDefaultMetadataPackageHeader*, void> __m2mep@?NotifyPackageDeserialize@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEBUXEPackageMetadata@@QEBUXE_LogDefaultMetadataPackageHeader@@@Z;

	// Token: 0x040000C9 RID: 201 RVA: 0x000493B0 File Offset: 0x000483B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, void> __m2mep@?NotifyOutOfMemory@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXXZ;

	// Token: 0x040000CA RID: 202 RVA: 0x000493C0 File Offset: 0x000483C0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, char*, ulong, void> __m2mep@?NotifyInvalidParameter@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEB_W_K@Z;

	// Token: 0x040000CB RID: 203 RVA: 0x000493D0 File Offset: 0x000483D0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, char*, ulong, void> __m2mep@?NotifyCanonicalizePathError@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEB_W_K@Z;

	// Token: 0x040000CC RID: 204 RVA: 0x000493E0 File Offset: 0x000483E0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, char*, uint, void> __m2mep@?NotifyFileOpenError@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEB_WK@Z;

	// Token: 0x040000CD RID: 205 RVA: 0x000493F0 File Offset: 0x000483F0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, uint, char*, void> __m2mep@?NotifyFileAccessError@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXKQEB_W@Z;

	// Token: 0x040000CE RID: 206 RVA: 0x00049400 File Offset: 0x00048400
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, char*, void> __m2mep@?NotifyMetadataDeserializationError@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEB_W@Z;

	// Token: 0x040000CF RID: 207 RVA: 0x00049410 File Offset: 0x00048410
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, XE_LogBufferPosition*, void> __m2mep@?NotifyBufferCorrupt@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEBUXE_LogBufferPosition@@@Z;

	// Token: 0x040000D0 RID: 208 RVA: 0x00049420 File Offset: 0x00048420
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventFileReaderMessageHandler*, char*, void> __m2mep@?NotifyInvalidFile@XEventFileReaderMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEB_W@Z;

	// Token: 0x040000D1 RID: 209 RVA: 0x000495B0 File Offset: 0x000485B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<sbyte*, sbyte*, int, sbyte*, void> __m2mep@?AssertionHandler@?A0xd4db69fe@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FYAXPEBD0H0@Z;

	// Token: 0x040000D2 RID: 210 RVA: 0x000496E8 File Offset: 0x000486E8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*, uint, void*> __m2mep@??_E?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@$$FUEAAPEAXI@Z;

	// Token: 0x040000D3 RID: 211 RVA: 0x000496A0 File Offset: 0x000486A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_FileReader<XE_FileReaderDefaultPolicy>*, uint, void*> __m2mep@??_E?$XE_FileReader@VXE_FileReaderDefaultPolicy@@@@$$FUEAAPEAXI@Z;

	// Token: 0x040000D4 RID: 212 RVA: 0x00049690 File Offset: 0x00048690
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig_v0>*, uint, void*> __m2mep@??_E?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig_v0@@@@$$FUEAAPEAXI@Z;

	// Token: 0x040000D5 RID: 213 RVA: 0x00049670 File Offset: 0x00048670
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*, XE_ISerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*> __m2mep@??0?$XE_ISerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@@@$$FQEAA@XZ;

	// Token: 0x040000D6 RID: 214 RVA: 0x00049680 File Offset: 0x00048680
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_SerializedEvent<Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter,XE_VersionConfig>*, uint, void*> __m2mep@??_E?$XE_SerializedEvent@VXEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@VXE_VersionConfig@@@@$$FUEAAPEAXI@Z;

	// Token: 0x040000D7 RID: 215 RVA: 0x00021728 File Offset: 0x00020728
	// Note: this field is marked with 'hasfieldrva'.
	unsafe static int** __unep@?DeserializePackageCallback@?A0xd4db69fe@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FYAHPEAXPEAVXE_LogDeserializedPackage@@@Z;

	// Token: 0x040000D8 RID: 216 RVA: 0x00021900 File Offset: 0x00020900
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BI@$$CB_W ??_C@_1DA@KBKPOKBL@?$AAS?$AAe?$AAr?$AAi?$AAa?$AAl?$AAi?$AAz?$AAe?$AAr?$AAP?$AAo?$AAl?$AAi?$AAc@;

	// Token: 0x040000D9 RID: 217 RVA: 0x000219C0 File Offset: 0x000209C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0EF@$$CB_W ??_C@_1IK@KGGBKFLL@?$AAX?$AAE?$AAS?$AAe?$AAs?$AAs?$AAi?$AAo?$AAn?$AA?5?$AAT?$AAa?$AAr?$AAg?$AAe@;

	// Token: 0x040000DA RID: 218 RVA: 0x00021B30 File Offset: 0x00020B30
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0LA@$$CB_W ??_C@_1BGA@BIJJPFO@?$AA?$FL?$AAE?$AAR?$AAR?$AAO?$AAR?$AA?$FN?$AA?5?$AAD?$AAe?$AAl?$AAe?$AAt?$AAi?$AAo@;

	// Token: 0x040000DB RID: 219 RVA: 0x00021C90 File Offset: 0x00020C90
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0EM@$$CB_W ??_C@_1JI@KGHHHIJ@?$AA?$FL?$AAI?$AAN?$AAF?$AAO?$AA?$FN?$AA?5?$AAD?$AAe?$AAl?$AAe?$AAt?$AAi?$AAo?$AAn@;

	// Token: 0x040000DC RID: 220 RVA: 0x00021D30 File Offset: 0x00020D30
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0EA@$$CB_W ??_C@_1IA@MOODEAK@?$AA?$FL?$AAE?$AAr?$AAr?$AAo?$AAr?$AA?$FN?$AA?5?$AAX?$AAE?$AAS?$AAe?$AAs?$AAs?$AAi@;

	// Token: 0x040000DD RID: 221 RVA: 0x00021B18 File Offset: 0x00020B18
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0L@$$CB_W ??_C@_1BG@BKMFGADG@?$AA?$CF?$AAs?$AA?2?$AA?$CF?$AAs?$AA_?$AA?$CF?$AAd?$AA?$CF?$AAs@;

	// Token: 0x040000DE RID: 222 RVA: 0x000498F0 File Offset: 0x000488F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7XE_IBufferWriter@@6B@;

	// Token: 0x040000DF RID: 223 RVA: 0x00049B58 File Offset: 0x00048B58
	// Note: this field is marked with 'hasfieldrva'.
	internal static XECollectedActionData ?A0x938327a2.NULLCAD;

	// Token: 0x040000E0 RID: 224 RVA: 0x000447E8 File Offset: 0x000437E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x040000E1 RID: 225 RVA: 0x000498A8 File Offset: 0x000488A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@6B@;

	// Token: 0x040000E2 RID: 226 RVA: 0x00044860 File Offset: 0x00043860
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XE_IBufferWriter@@8;

	// Token: 0x040000E3 RID: 227 RVA: 0x00044748 File Offset: 0x00043748
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2XE_ILogWriteMessageHandler@@8;

	// Token: 0x040000E4 RID: 228 RVA: 0x00044850 File Offset: 0x00043850
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2XE_IBufferWriter@@8;

	// Token: 0x040000E5 RID: 229 RVA: 0x0004A000 File Offset: 0x00049000
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_78 ??_R0?AVXEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@@8;

	// Token: 0x040000E6 RID: 230 RVA: 0x000449A0 File Offset: 0x000439A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4?$LogWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@6B@;

	// Token: 0x040000E7 RID: 231 RVA: 0x000449F0 File Offset: 0x000439F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XE_ILogWriter@@6B@;

	// Token: 0x040000E8 RID: 232 RVA: 0x000498C0 File Offset: 0x000488C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY05Q6AXXZ ??_7?$LogWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@6B@;

	// Token: 0x040000E9 RID: 233 RVA: 0x00044778 File Offset: 0x00043778
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XE_ILogWriteMessageHandler@@6B@;

	// Token: 0x040000EA RID: 234 RVA: 0x00044878 File Offset: 0x00043878
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XE_IBufferWriter@@8;

	// Token: 0x040000EB RID: 235 RVA: 0x000448B8 File Offset: 0x000438B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@8;

	// Token: 0x040000EC RID: 236 RVA: 0x00049788 File Offset: 0x00048788
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0M@Q6AXXZ ??_7XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@6B@;

	// Token: 0x040000ED RID: 237 RVA: 0x0004A118 File Offset: 0x00049118
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_23 ??_R0?AVXE_IBufferWriter@@@8;

	// Token: 0x040000EE RID: 238 RVA: 0x000448A0 File Offset: 0x000438A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@8;

	// Token: 0x040000EF RID: 239 RVA: 0x00044800 File Offset: 0x00043800
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@6B@;

	// Token: 0x040000F0 RID: 240 RVA: 0x00044760 File Offset: 0x00043760
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XE_ILogWriteMessageHandler@@8;

	// Token: 0x040000F1 RID: 241 RVA: 0x00021938 File Offset: 0x00020938
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID ?A0x938327a2._GUID_NULL;

	// Token: 0x040000F2 RID: 242 RVA: 0x000448D0 File Offset: 0x000438D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@6B@;

	// Token: 0x040000F3 RID: 243 RVA: 0x00044920 File Offset: 0x00043920
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2XE_ILogWriter@@8;

	// Token: 0x040000F4 RID: 244 RVA: 0x00044930 File Offset: 0x00043930
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3XE_ILogWriter@@8;

	// Token: 0x040000F5 RID: 245 RVA: 0x000448F8 File Offset: 0x000438F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$LogWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@8;

	// Token: 0x040000F6 RID: 246 RVA: 0x00044970 File Offset: 0x00043970
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2?$LogWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@8;

	// Token: 0x040000F7 RID: 247 RVA: 0x000447C8 File Offset: 0x000437C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x040000F8 RID: 248 RVA: 0x00049FC0 File Offset: 0x00048FC0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_33 ??_R0?AVXE_ILogWriteMessageHandler@@@8;

	// Token: 0x040000F9 RID: 249 RVA: 0x00044988 File Offset: 0x00043988
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3?$LogWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@8;

	// Token: 0x040000FA RID: 250 RVA: 0x000449C8 File Offset: 0x000439C8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4XE_IBufferWriter@@6B@;

	// Token: 0x040000FB RID: 251 RVA: 0x00049B50 File Offset: 0x00048B50
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor ?A0x938327a2.NULLADD;

	// Token: 0x040000FC RID: 252 RVA: 0x00049718 File Offset: 0x00048718
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0M@Q6AXXZ ??_7XE_ILogWriteMessageHandler@@6B@;

	// Token: 0x040000FD RID: 253 RVA: 0x00044828 File Offset: 0x00043828
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@8;

	// Token: 0x040000FE RID: 254 RVA: 0x00044948 File Offset: 0x00043948
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XE_ILogWriter@@8;

	// Token: 0x040000FF RID: 255 RVA: 0x00021930 File Offset: 0x00020930
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* Microsoft.SqlServer.XEvent.Linq.Internal.?A0x938327a2.SerializerPolicyPointerParam;

	// Token: 0x04000100 RID: 256 RVA: 0x00049948 File Offset: 0x00048948
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY05Q6AXXZ ??_7XE_ILogWriter@@6B@;

	// Token: 0x04000101 RID: 257 RVA: 0x00044720 File Offset: 0x00043720
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XE_ILogWriteMessageHandler@@8;

	// Token: 0x04000102 RID: 258 RVA: 0x0004A298 File Offset: 0x00049298
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVXE_ILogWriter@@@8;

	// Token: 0x04000103 RID: 259 RVA: 0x000447A0 File Offset: 0x000437A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@8;

	// Token: 0x04000104 RID: 260 RVA: 0x0004A140 File Offset: 0x00049140
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_324 ??_R0?AV?$LogWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@@8;

	// Token: 0x04000105 RID: 261 RVA: 0x0004A060 File Offset: 0x00049060
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_167 ??_R0?AV?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@@8;

	// Token: 0x04000106 RID: 262 RVA: 0x00049980 File Offset: 0x00048980
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter*, XE_IBufferWriter*, void> __m2mep@?SetBufferWriter@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAXPEAVXE_IBufferWriter@@@Z;

	// Token: 0x04000107 RID: 263 RVA: 0x00049B40 File Offset: 0x00048B40
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventInteropMetadataAdapter*, void*, int> __m2mep@?OnLogCreate@XEventInteropMetadataAdapter@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHPEAX@Z;

	// Token: 0x04000108 RID: 264 RVA: 0x00049840 File Offset: 0x00048840
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, XERWMode, void> __m2mep@?NotifyRWLockError@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXW4XERWMode@@@Z;

	// Token: 0x04000109 RID: 265 RVA: 0x00049800 File Offset: 0x00048800
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, void> __m2mep@?NotifyOutOfMemory@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXXZ;

	// Token: 0x0400010A RID: 266 RVA: 0x00049830 File Offset: 0x00048830
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, void> __m2mep@?NotifyInitError@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXXZ;

	// Token: 0x0400010B RID: 267 RVA: 0x00049810 File Offset: 0x00048810
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, char*, ulong, void> __m2mep@?NotifyInvalidParameter@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEB_W_K@Z;

	// Token: 0x0400010C RID: 268 RVA: 0x00049820 File Offset: 0x00048820
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, char*, ulong, void> __m2mep@?NotifyCanonicalizePathError@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXQEB_W_K@Z;

	// Token: 0x0400010D RID: 269 RVA: 0x00049870 File Offset: 0x00048870
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, uint, ulong, uint, char*, void> __m2mep@?NotifyLogWriteError@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXK_KIQEB_W@Z;

	// Token: 0x0400010E RID: 270 RVA: 0x00049850 File Offset: 0x00048850
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, uint, char*, ushort, void> __m2mep@?NotifyLogCreateError@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXKQEB_WG@Z;

	// Token: 0x0400010F RID: 271 RVA: 0x00049860 File Offset: 0x00048860
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, uint, ulong, char*, void> __m2mep@?NotifyLogSetSizeError@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXK_KQEB_W@Z;

	// Token: 0x04000110 RID: 272 RVA: 0x00049880 File Offset: 0x00048880
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, XERelativeObjectId, void> __m2mep@?NotifyMetadataSerializationError@XEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAXUXERelativeObjectId@@@Z;

	// Token: 0x04000111 RID: 273 RVA: 0x000497F0 File Offset: 0x000487F0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerMessageHandler*, uint, void*> __m2mep@??_EXEventSerializerMessageHandler@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FUEAAPEAXI@Z;

	// Token: 0x04000112 RID: 274 RVA: 0x000497E0 File Offset: 0x000487E0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_ILogWriteMessageHandler*, uint, void*> __m2mep@??_EXE_ILogWriteMessageHandler@@$$FUEAAPEAXI@Z;

	// Token: 0x04000113 RID: 275 RVA: 0x00049890 File Offset: 0x00048890
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_LogWriter<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>\u0020>*, uint, void*> __m2mep@??_G?$XE_LogWriter@V?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@@@$$FQEAAPEAXI@Z;

	// Token: 0x04000114 RID: 276 RVA: 0x000499D0 File Offset: 0x000489D0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint> __m2mep@?GetMaxAffinity@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAIXZ;

	// Token: 0x04000115 RID: 277 RVA: 0x00049920 File Offset: 0x00048920
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, void> __m2mep@??1?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAA@XZ;

	// Token: 0x04000116 RID: 278 RVA: 0x00049930 File Offset: 0x00048930
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_FileWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>.LogWriter<Microsoft::SqlServer::XEvent::Linq::Internal::XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>,Microsoft::SqlServer::XEvent::Linq::Internal::XEventInteropMetadataAdapter>*, void> __m2mep@??1?$LogWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@?$XE_FileWriter@V?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@VXEventInteropMetadataAdapter@23456@@@$$FQEAA@XZ;

	// Token: 0x04000117 RID: 279 RVA: 0x00049770 File Offset: 0x00048770
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_AutoP<XE_Log>*, uint, void*> __m2mep@??_E?$XE_AutoP@VXE_Log@@@@$$FQEAAPEAXI@Z;

	// Token: 0x04000118 RID: 280 RVA: 0x000499E0 File Offset: 0x000489E0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_AutoP<XE_Log>*, void> __m2mep@??_F?$XE_AutoP@VXE_Log@@@@$$FQEAAXXZ;

	// Token: 0x04000119 RID: 281 RVA: 0x00049A10 File Offset: 0x00048A10
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, ulong*> __m2mep@?GetLogMaxSizeInBytes@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAAEB_KXZ;

	// Token: 0x0400011A RID: 282 RVA: 0x00049A00 File Offset: 0x00048A00
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint*> __m2mep@?GetLogMaxRollover@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAAEBIXZ;

	// Token: 0x0400011B RID: 283 RVA: 0x000499A0 File Offset: 0x000489A0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, int> __m2mep@?IsAsyncIo@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAHXZ;

	// Token: 0x0400011C RID: 284 RVA: 0x000499B0 File Offset: 0x000489B0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint, uint> __m2mep@?GetSectorSize@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAII@Z;

	// Token: 0x0400011D RID: 285 RVA: 0x000499F0 File Offset: 0x000489F0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint, uint> __m2mep@?GetCurrentLogCount@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAII@Z;

	// Token: 0x0400011E RID: 286 RVA: 0x00049B30 File Offset: 0x00048B30
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, void> __m2mep@?OnRetry@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAXXZ;

	// Token: 0x0400011F RID: 287 RVA: 0x00049B20 File Offset: 0x00048B20
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, int> __m2mep@?IsRetryNeeded@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHXZ;

	// Token: 0x04000120 RID: 288 RVA: 0x000499C0 File Offset: 0x000489C0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, XE_Log*, XEBufferHeader*, void> __m2mep@?UpdateLogTime@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAXPEAVXE_Log@@QEAUXEBufferHeader@@@Z;

	// Token: 0x04000121 RID: 289 RVA: 0x00049B10 File Offset: 0x00048B10
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, int> __m2mep@?IsAccessAllowed@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAHXZ;

	// Token: 0x04000122 RID: 290 RVA: 0x00049B00 File Offset: 0x00048B00
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, XE_Log*, XEBufferHeader*, int> __m2mep@?IsRolloverNeeded@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAHPEAVXE_Log@@QEAUXEBufferHeader@@@Z;

	// Token: 0x04000123 RID: 291 RVA: 0x00049990 File Offset: 0x00048990
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, ushort, XECustomizableAttribute*, XE_ILogWriteMessageHandler*, bool, int> __m2mep@?Initialize@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHGQEBUXECustomizableAttribute@@PEAVXE_ILogWriteMessageHandler@@_N@Z;

	// Token: 0x04000124 RID: 292 RVA: 0x00049900 File Offset: 0x00048900
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_IBufferWriter*, XE_IBufferWriter*> __m2mep@??0XE_IBufferWriter@@$$FQEAA@XZ;

	// Token: 0x04000125 RID: 293 RVA: 0x00049910 File Offset: 0x00048910
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*> __m2mep@??0?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAA@XZ;

	// Token: 0x04000126 RID: 294 RVA: 0x00049A60 File Offset: 0x00048A60
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, ulong*> __m2mep@?GetLogGrowthSizeInBytes@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAAEB_KXZ;

	// Token: 0x04000127 RID: 295 RVA: 0x00049AA0 File Offset: 0x00048AA0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint, int, int> __m2mep@?CanCreateFile@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHIH@Z;

	// Token: 0x04000128 RID: 296 RVA: 0x00049AB0 File Offset: 0x00048AB0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint, char*, uint, uint*, int> __m2mep@?GetLogPathToDelete@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHIQEA_WIPEAI@Z;

	// Token: 0x04000129 RID: 297 RVA: 0x00049AE0 File Offset: 0x00048AE0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, char*> __m2mep@?GetLogPath@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAPEB_WXZ;

	// Token: 0x0400012A RID: 298 RVA: 0x00049A80 File Offset: 0x00048A80
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, long, int> __m2mep@?OnLogRollover@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAH_J@Z;

	// Token: 0x0400012B RID: 299 RVA: 0x00049AC0 File Offset: 0x00048AC0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, char*, int> __m2mep@?OnLogDelete@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHQEB_W@Z;

	// Token: 0x0400012C RID: 300 RVA: 0x00049AD0 File Offset: 0x00048AD0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint, XE_Log*, void> __m2mep@?OnLogClose@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAXIPEAVXE_Log@@@Z;

	// Token: 0x0400012D RID: 301 RVA: 0x00049A40 File Offset: 0x00048A40
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, int> __m2mep@?IsForcedUnitAccess@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAHXZ;

	// Token: 0x0400012E RID: 302 RVA: 0x00049A20 File Offset: 0x00048A20
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, int> __m2mep@?GetLogFileOverwrite@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAHXZ;

	// Token: 0x0400012F RID: 303 RVA: 0x00049A50 File Offset: 0x00048A50
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint> __m2mep@?GetBlobShareModeOnCreation@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEBAKXZ;

	// Token: 0x04000130 RID: 304 RVA: 0x00049A30 File Offset: 0x00048A30
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, char*, uint, _SYSTEMTIME*, XE_FileSourceType, int> __m2mep@?GetIndexFilePathName@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHQEA_WIAEBU_SYSTEMTIME@@W4XE_FileSourceType@@@Z;

	// Token: 0x04000131 RID: 305 RVA: 0x00049A90 File Offset: 0x00048A90
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint, uint, char*, uint, _SYSTEMTIME*, int> __m2mep@?GetLogPathName@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHIIQEA_WIAEBU_SYSTEMTIME@@@Z;

	// Token: 0x04000132 RID: 306 RVA: 0x00049A70 File Offset: 0x00048A70
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XEventSerializerPolicy<XE_FileWriterDefaultPolicy<1,0>\u0020>*, uint, uint, char*, XE_Log*, int> __m2mep@?OnLogCreate@?$XEventSerializerPolicy@V?$XE_FileWriterDefaultPolicy@$00$0A@@@@Internal@Linq@XEvent@SqlServer@Microsoft@@$$FQEAAHIIQEB_WPEAVXE_Log@@@Z;

	// Token: 0x04000133 RID: 307 RVA: 0x00049970 File Offset: 0x00048970
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_ILogWriter*, XE_ILogWriter*> __m2mep@??0XE_ILogWriter@@$$FQEAA@XZ;

	// Token: 0x04000134 RID: 308 RVA: 0x00049AF0 File Offset: 0x00048AF0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<XE_AutoP<XE_Log>*, uint, void*> __m2mep@??_G?$XE_AutoP@VXE_Log@@@@$$FQEAAPEAXI@Z;

	// Token: 0x04000135 RID: 309 RVA: 0x00021EB0 File Offset: 0x00020EB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000136 RID: 310 RVA: 0x00021EA0 File Offset: 0x00020EA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000137 RID: 311
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000138 RID: 312 RVA: 0x00021460 File Offset: 0x00020460
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000139 RID: 313
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400013A RID: 314 RVA: 0x00021478 File Offset: 0x00020478
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400013B RID: 315 RVA: 0x00021EC0 File Offset: 0x00020EC0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400013C RID: 316 RVA: 0x00021ED0 File Offset: 0x00020ED0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400013D RID: 317
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400013E RID: 318 RVA: 0x0004A8D4 File Offset: 0x000498D4
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'False'.
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400013F RID: 319 RVA: 0x00049BAC File Offset: 0x00048BAC
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000140 RID: 320 RVA: 0x0004A8D7 File Offset: 0x000498D7
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'False'.
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000141 RID: 321 RVA: 0x0004A8D0 File Offset: 0x000498D0
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '0'.
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x04000142 RID: 322
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000143 RID: 323 RVA: 0x0004A8D6 File Offset: 0x000498D6
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'False'.
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000144 RID: 324
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000145 RID: 325
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000146 RID: 326 RVA: 0x0004A8D5 File Offset: 0x000498D5
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'False'.
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000147 RID: 327
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000148 RID: 328 RVA: 0x00049BA8 File Offset: 0x00048BA8
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000149 RID: 329 RVA: 0x000214A0 File Offset: 0x000204A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x0400014A RID: 330 RVA: 0x000214B0 File Offset: 0x000204B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x0400014B RID: 331 RVA: 0x00021480 File Offset: 0x00020480
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400014C RID: 332 RVA: 0x00021450 File Offset: 0x00020450
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x0400014D RID: 333 RVA: 0x00021490 File Offset: 0x00020490
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x0400014E RID: 334 RVA: 0x00021488 File Offset: 0x00020488
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400014F RID: 335 RVA: 0x000214A8 File Offset: 0x000204A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x04000150 RID: 336 RVA: 0x00021458 File Offset: 0x00020458
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000151 RID: 337 RVA: 0x00021498 File Offset: 0x00020498
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x04000152 RID: 338 RVA: 0x00021470 File Offset: 0x00020470
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000153 RID: 339 RVA: 0x00021468 File Offset: 0x00020468
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xba6ca20d.?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000154 RID: 340 RVA: 0x00049BB0 File Offset: 0x00048BB0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000155 RID: 341 RVA: 0x00049BC0 File Offset: 0x00048BC0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000156 RID: 342 RVA: 0x00021EE0 File Offset: 0x00020EE0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000157 RID: 343 RVA: 0x00021EE8 File Offset: 0x00020EE8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000158 RID: 344 RVA: 0x0004AA40 File Offset: 0x00049A40
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0x8e112e2a.__onexitbegin_m;

	// Token: 0x04000159 RID: 345 RVA: 0x0004AA38 File Offset: 0x00049A38
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '0'.
	internal static ulong ?A0x8e112e2a.__exit_list_size;

	// Token: 0x0400015A RID: 346
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitend_app_domain;

	// Token: 0x0400015B RID: 347
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x0400015C RID: 348
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x0400015D RID: 349 RVA: 0x0004AA48 File Offset: 0x00049A48
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0x8e112e2a.__onexitend_m;

	// Token: 0x0400015E RID: 350
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x0400015F RID: 351
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitbegin_app_domain;

	// Token: 0x04000160 RID: 352 RVA: 0x0004AA60 File Offset: 0x00049A60
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEEngineServicesAPI ?sm_ServiceAPI@XE_API@@2UXEEngineServicesAPI@@A;

	// Token: 0x04000161 RID: 353 RVA: 0x00021E60 File Offset: 0x00020E60
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x04000162 RID: 354 RVA: 0x0004ACD8 File Offset: 0x00049CD8
	// Note: this field is marked with 'hasfieldrva'.
	internal static XE_OneTimeInit ?sm_oti@XE_API@@2VXE_OneTimeInit@@A;

	// Token: 0x04000163 RID: 355 RVA: 0x0004ACE0 File Offset: 0x00049CE0
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEEngineClientAPI ?sm_ClientAPI@XE_API@@2UXEEngineClientAPI@@A;

	// Token: 0x04000164 RID: 356 RVA: 0x0004AF20 File Offset: 0x00049F20
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEEngineRegisterAPI ?sm_RegistrationAPI@XE_API@@2UXEEngineRegisterAPI@@A;

	// Token: 0x04000165 RID: 357 RVA: 0x0004B618 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor ?sm_NULLADD@XE_VersionConfig@@2UXEActionDataDescriptor@@B;

	// Token: 0x04000166 RID: 358 RVA: 0x00022378 File Offset: 0x00021378
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_SortOptions@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x04000167 RID: 359 RVA: 0x000223A8 File Offset: 0x000213A8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_MetadataFileName@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x04000168 RID: 360 RVA: 0x000223B0 File Offset: 0x000213B0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* XE_File_PartitionSeparator;

	// Token: 0x04000169 RID: 361 RVA: 0x00022348 File Offset: 0x00021348
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_LogFileName@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x0400016A RID: 362 RVA: 0x0004B610 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static XEActionDataDescriptor_v0 ?sm_NULLADD@XE_VersionConfig_v0@@2UXEActionDataDescriptor_v0@@B;

	// Token: 0x0400016B RID: 363 RVA: 0x00022350 File Offset: 0x00021350
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_LogMaxFileSizeInMBytes@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x0400016C RID: 364 RVA: 0x00022358 File Offset: 0x00021358
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_DatabaseID@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x0400016D RID: 365 RVA: 0x00022368 File Offset: 0x00021368
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_LogMaxRolloverFiles@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x0400016E RID: 366 RVA: 0x00022370 File Offset: 0x00021370
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_AddAppName@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x0400016F RID: 367 RVA: 0x00022380 File Offset: 0x00021380
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* DefaultIndexFileExtension;

	// Token: 0x04000170 RID: 368 RVA: 0x00022390 File Offset: 0x00021390
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_IsAppendBlob@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x04000171 RID: 369 RVA: 0x000223B8 File Offset: 0x000213B8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static char* ?XE_File_LogIncrementSizeInMBytes@XE_FileTargetParams@@2QEB_WEB;

	// Token: 0x04000172 RID: 370 RVA: 0x00021428 File Offset: 0x00020428
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000173 RID: 371 RVA: 0x0004A2F0 File Offset: 0x000492F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x04000174 RID: 372 RVA: 0x0004A2F8 File Offset: 0x000492F8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x04000175 RID: 373 RVA: 0x000213E8 File Offset: 0x000203E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000176 RID: 374 RVA: 0x00021420 File Offset: 0x00020420
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x04000177 RID: 375 RVA: 0x00049B84 File Offset: 0x00048B84
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '4294967295'.
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x04000178 RID: 376 RVA: 0x00021418 File Offset: 0x00020418
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
