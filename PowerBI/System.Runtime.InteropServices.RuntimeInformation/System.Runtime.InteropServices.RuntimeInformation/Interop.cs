using System;
using System.Runtime.InteropServices;

// Token: 0x02000002 RID: 2
internal static class Interop
{
	// Token: 0x02000008 RID: 8
	internal class NtDll
	{
		// Token: 0x0600001D RID: 29
		[DllImport("ntdll.dll")]
		private static extern int RtlGetVersion(out Interop.NtDll.RTL_OSVERSIONINFOEX lpVersionInformation);

		// Token: 0x0600001E RID: 30 RVA: 0x000024A8 File Offset: 0x000006A8
		internal static string RtlGetVersion()
		{
			Interop.NtDll.RTL_OSVERSIONINFOEX rtl_OSVERSIONINFOEX = default(Interop.NtDll.RTL_OSVERSIONINFOEX);
			rtl_OSVERSIONINFOEX.dwOSVersionInfoSize = (uint)Marshal.SizeOf(rtl_OSVERSIONINFOEX);
			if (Interop.NtDll.RtlGetVersion(out rtl_OSVERSIONINFOEX) == 0)
			{
				return string.Format("{0} {1}.{2}.{3} {4}", new object[] { "Microsoft Windows", rtl_OSVERSIONINFOEX.dwMajorVersion, rtl_OSVERSIONINFOEX.dwMinorVersion, rtl_OSVERSIONINFOEX.dwBuildNumber, rtl_OSVERSIONINFOEX.szCSDVersion });
			}
			return "Microsoft Windows";
		}

		// Token: 0x0200000B RID: 11
		internal struct RTL_OSVERSIONINFOEX
		{
			// Token: 0x04000058 RID: 88
			internal uint dwOSVersionInfoSize;

			// Token: 0x04000059 RID: 89
			internal uint dwMajorVersion;

			// Token: 0x0400005A RID: 90
			internal uint dwMinorVersion;

			// Token: 0x0400005B RID: 91
			internal uint dwBuildNumber;

			// Token: 0x0400005C RID: 92
			internal uint dwPlatformId;

			// Token: 0x0400005D RID: 93
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
			internal string szCSDVersion;
		}
	}

	// Token: 0x02000009 RID: 9
	internal static class Libraries
	{
		// Token: 0x04000013 RID: 19
		internal const string Advapi32 = "advapi32.dll";

		// Token: 0x04000014 RID: 20
		internal const string BCrypt = "BCrypt.dll";

		// Token: 0x04000015 RID: 21
		internal const string Combase = "combase.dll";

		// Token: 0x04000016 RID: 22
		internal const string Console_L1 = "api-ms-win-core-console-l1-1-0.dll";

		// Token: 0x04000017 RID: 23
		internal const string Console_L2 = "api-ms-win-core-console-l2-1-0.dll";

		// Token: 0x04000018 RID: 24
		internal const string CoreFile_L1 = "api-ms-win-core-file-l1-1-0.dll";

		// Token: 0x04000019 RID: 25
		internal const string CoreFile_L1_2 = "api-ms-win-core-file-l1-2-0.dll";

		// Token: 0x0400001A RID: 26
		internal const string CoreFile_L2 = "api-ms-win-core-file-l2-1-0.dll";

		// Token: 0x0400001B RID: 27
		internal const string Crypt32 = "crypt32.dll";

		// Token: 0x0400001C RID: 28
		internal const string Debug = "api-ms-win-core-debug-l1-1-0.dll";

		// Token: 0x0400001D RID: 29
		internal const string Error_L1 = "api-ms-win-core-winrt-error-l1-1-0.dll";

		// Token: 0x0400001E RID: 30
		internal const string ErrorHandling = "api-ms-win-core-errorhandling-l1-1-0.dll";

		// Token: 0x0400001F RID: 31
		internal const string Eventing = "api-ms-win-eventing-provider-l1-1-0.dll";

		// Token: 0x04000020 RID: 32
		internal const string Handle = "api-ms-win-core-handle-l1-1-0.dll";

		// Token: 0x04000021 RID: 33
		internal const string Heap = "api-ms-win-core-heap-obsolete-l1-1-0.dll";

		// Token: 0x04000022 RID: 34
		internal const string Heap_L1 = "api-ms-win-core-heap-l1-1-0.dll";

		// Token: 0x04000023 RID: 35
		internal const string IO = "api-ms-win-core-io-l1-1-0.dll";

		// Token: 0x04000024 RID: 36
		internal const string IpHlpApi = "iphlpapi.dll";

		// Token: 0x04000025 RID: 37
		internal const string Kernel32 = "kernel32.dll";

		// Token: 0x04000026 RID: 38
		internal const string Kernel32_L1 = "api-ms-win-core-kernel32-legacy-l1-1-1.dll";

		// Token: 0x04000027 RID: 39
		internal const string Kernel32_L2 = "api-ms-win-core-kernel32-legacy-l1-1-0.dll";

		// Token: 0x04000028 RID: 40
		internal const string Keyboard = "ext-ms-win-ntuser-keyboard-l1-2-1.dll";

		// Token: 0x04000029 RID: 41
		internal const string LibraryLoader = "api-ms-win-core-libraryloader-l1-1-0.dll";

		// Token: 0x0400002A RID: 42
		internal const string Localization = "api-ms-win-core-localization-l1-2-0.dll";

		// Token: 0x0400002B RID: 43
		internal const string Memory_L1_0 = "api-ms-win-core-memory-l1-1-0.dll";

		// Token: 0x0400002C RID: 44
		internal const string Memory_L1_1 = "api-ms-win-core-memory-l1-1-1.dll";

		// Token: 0x0400002D RID: 45
		internal const string Memory_L1_2 = "api-ms-win-core-memory-l1-1-2.dll";

		// Token: 0x0400002E RID: 46
		internal const string Memory_L1_3 = "api-ms-win-core-memory-l1-1-3.dll";

		// Token: 0x0400002F RID: 47
		internal const string NCrypt = "ncrypt.dll";

		// Token: 0x04000030 RID: 48
		internal const string NtDll = "ntdll.dll";

		// Token: 0x04000031 RID: 49
		internal const string OleAut32 = "oleaut32.dll";

		// Token: 0x04000032 RID: 50
		internal const string Pipe = "api-ms-win-core-namedpipe-l1-1-0.dll";

		// Token: 0x04000033 RID: 51
		internal const string Pipe_L2 = "api-ms-win-core-namedpipe-l1-2-1.dll";

		// Token: 0x04000034 RID: 52
		internal const string ProcessEnvironment = "api-ms-win-core-processenvironment-l1-1-0.dll";

		// Token: 0x04000035 RID: 53
		internal const string ProcessThread_L1 = "api-ms-win-core-processthreads-l1-1-0.dll";

		// Token: 0x04000036 RID: 54
		internal const string ProcessThread_L1_1 = "api-ms-win-core-processthreads-l1-1-1.dll";

		// Token: 0x04000037 RID: 55
		internal const string ProcessThread_L1_2 = "api-ms-win-core-processthreads-l1-1-2.dll";

		// Token: 0x04000038 RID: 56
		internal const string ProcessTopology = "api-ms-win-core-processtopology-obsolete-l1-1-0.dll";

		// Token: 0x04000039 RID: 57
		internal const string Profile = "api-ms-win-core-profile-l1-1-0.dll";

		// Token: 0x0400003A RID: 58
		internal const string Psapi = "api-ms-win-core-psapi-l1-1-0.dll";

		// Token: 0x0400003B RID: 59
		internal const string Psapi_Obsolete = "api-ms-win-core-psapi-obsolete-l1-1-0.dll";

		// Token: 0x0400003C RID: 60
		internal const string Registry_L1 = "api-ms-win-core-registry-l1-1-0.dll";

		// Token: 0x0400003D RID: 61
		internal const string Registry_L2 = "api-ms-win-core-registry-l2-1-0.dll";

		// Token: 0x0400003E RID: 62
		internal const string RoBuffer = "api-ms-win-core-winrt-robuffer-l1-1-0.dll";

		// Token: 0x0400003F RID: 63
		internal const string SecurityBase = "api-ms-win-security-base-l1-1-0.dll";

		// Token: 0x04000040 RID: 64
		internal const string SecurityCpwl = "api-ms-win-security-cpwl-l1-1-0.dll";

		// Token: 0x04000041 RID: 65
		internal const string SecurityCryptoApi = "api-ms-win-security-cryptoapi-l1-1-0.dll";

		// Token: 0x04000042 RID: 66
		internal const string SecurityLsa = "api-ms-win-security-lsalookup-l2-1-0.dll";

		// Token: 0x04000043 RID: 67
		internal const string SecurityLsaPolicy = "api-ms-win-security-lsapolicy-l1-1-0.dll";

		// Token: 0x04000044 RID: 68
		internal const string SecurityProvider = "api-ms-win-security-provider-l1-1-0.dll";

		// Token: 0x04000045 RID: 69
		internal const string SecuritySddl = "api-ms-win-security-sddl-l1-1-0.dll";

		// Token: 0x04000046 RID: 70
		internal const string ServiceCore = "api-ms-win-service-core-l1-1-1.dll";

		// Token: 0x04000047 RID: 71
		internal const string ServiceMgmt_L1 = "api-ms-win-service-management-l1-1-0.dll";

		// Token: 0x04000048 RID: 72
		internal const string ServiceMgmt_L2 = "api-ms-win-service-management-l2-1-0.dll";

		// Token: 0x04000049 RID: 73
		internal const string ServiceWinSvc = "api-ms-win-service-winsvc-l1-1-0.dll";

		// Token: 0x0400004A RID: 74
		internal const string Sspi = "sspicli.dll";

		// Token: 0x0400004B RID: 75
		internal const string String_L1 = "api-ms-win-core-string-l1-1-0.dll";

		// Token: 0x0400004C RID: 76
		internal const string Synch = "api-ms-win-core-synch-l1-1-0.dll";

		// Token: 0x0400004D RID: 77
		internal const string SystemInfo_L1_1 = "api-ms-win-core-sysinfo-l1-1-0.dll";

		// Token: 0x0400004E RID: 78
		internal const string SystemInfo_L1_2 = "api-ms-win-core-sysinfo-l1-2-0.dll";

		// Token: 0x0400004F RID: 79
		internal const string ThreadPool = "api-ms-win-core-threadpool-l1-2-0.dll";

		// Token: 0x04000050 RID: 80
		internal const string User32 = "user32.dll";

		// Token: 0x04000051 RID: 81
		internal const string Util = "api-ms-win-core-util-l1-1-0.dll";

		// Token: 0x04000052 RID: 82
		internal const string Version = "api-ms-win-core-version-l1-1-0.dll";

		// Token: 0x04000053 RID: 83
		internal const string WinHttp = "winhttp.dll";

		// Token: 0x04000054 RID: 84
		internal const string Winsock = "Ws2_32.dll";

		// Token: 0x04000055 RID: 85
		internal const string Wow64 = "api-ms-win-core-wow64-l1-1-0.dll";

		// Token: 0x04000056 RID: 86
		internal const string Ws2_32 = "ws2_32.dll";

		// Token: 0x04000057 RID: 87
		internal const string Zlib = "clrcompression.dll";
	}

	// Token: 0x0200000A RID: 10
	internal class mincore
	{
		// Token: 0x06000020 RID: 32
		[DllImport("api-ms-win-core-sysinfo-l1-2-0.dll")]
		internal static extern void GetNativeSystemInfo(out Interop.mincore.SYSTEM_INFO lpSystemInfo);

		// Token: 0x06000021 RID: 33
		[DllImport("api-ms-win-core-sysinfo-l1-1-0.dll")]
		internal static extern void GetSystemInfo(out Interop.mincore.SYSTEM_INFO lpSystemInfo);

		// Token: 0x0200000C RID: 12
		internal struct SYSTEM_INFO
		{
			// Token: 0x0400005E RID: 94
			internal ushort wProcessorArchitecture;

			// Token: 0x0400005F RID: 95
			internal ushort wReserved;

			// Token: 0x04000060 RID: 96
			internal int dwPageSize;

			// Token: 0x04000061 RID: 97
			internal IntPtr lpMinimumApplicationAddress;

			// Token: 0x04000062 RID: 98
			internal IntPtr lpMaximumApplicationAddress;

			// Token: 0x04000063 RID: 99
			internal IntPtr dwActiveProcessorMask;

			// Token: 0x04000064 RID: 100
			internal int dwNumberOfProcessors;

			// Token: 0x04000065 RID: 101
			internal int dwProcessorType;

			// Token: 0x04000066 RID: 102
			internal int dwAllocationGranularity;

			// Token: 0x04000067 RID: 103
			internal short wProcessorLevel;

			// Token: 0x04000068 RID: 104
			internal short wProcessorRevision;
		}

		// Token: 0x0200000D RID: 13
		internal enum ProcessorArchitecture : ushort
		{
			// Token: 0x0400006A RID: 106
			Processor_Architecture_INTEL,
			// Token: 0x0400006B RID: 107
			Processor_Architecture_ARM = 5,
			// Token: 0x0400006C RID: 108
			Processor_Architecture_IA64,
			// Token: 0x0400006D RID: 109
			Processor_Architecture_AMD64 = 9,
			// Token: 0x0400006E RID: 110
			Processor_Architecture_ARM64 = 12,
			// Token: 0x0400006F RID: 111
			Processor_Architecture_UNKNOWN = 65535
		}
	}
}
