using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Data.Common;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Data.SqlTypes
{
	// Token: 0x0200001C RID: 28
	[SuppressUnmanagedCodeSecurity]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x0600066C RID: 1644
		[DllImport("NtDll.dll", CharSet = CharSet.Unicode)]
		internal static extern uint NtCreateFile(out SafeFileHandle fileHandle, int desiredAccess, ref UnsafeNativeMethods.OBJECT_ATTRIBUTES objectAttributes, out UnsafeNativeMethods.IO_STATUS_BLOCK ioStatusBlock, ref long allocationSize, uint fileAttributes, FileShare shareAccess, uint createDisposition, uint createOptions, SafeHandle eaBuffer, uint eaLength);

		// Token: 0x0600066D RID: 1645
		[DllImport("Kernel32.dll", SetLastError = true)]
		internal static extern UnsafeNativeMethods.FileType GetFileType(SafeFileHandle hFile);

		// Token: 0x0600066E RID: 1646
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int GetFullPathName(string path, int numBufferChars, StringBuilder buffer, IntPtr lpFilePartOrNull);

		// Token: 0x0600066F RID: 1647 RVA: 0x0000C458 File Offset: 0x0000A658
		internal static string SafeGetFullPathName(string path)
		{
			StringBuilder stringBuilder = new StringBuilder(path.Length + 1);
			int num = UnsafeNativeMethods.GetFullPathName(path, stringBuilder.Capacity, stringBuilder, IntPtr.Zero);
			if (num > stringBuilder.Capacity)
			{
				stringBuilder.Capacity = num;
				num = UnsafeNativeMethods.GetFullPathName(path, stringBuilder.Capacity, stringBuilder, IntPtr.Zero);
			}
			if (num != 0)
			{
				return stringBuilder.ToString();
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 0)
			{
				throw ADP.Argument(StringsHelper.GetString(Strings.SqlFileStream_InvalidPath, Array.Empty<object>()), "path");
			}
			Win32Exception ex = new Win32Exception(lastWin32Error);
			ADP.TraceExceptionAsReturnValue(ex);
			throw ex;
		}

		// Token: 0x06000670 RID: 1648
		[DllImport("Kernel32.dll", ExactSpelling = true)]
		private static extern uint SetErrorMode(uint mode);

		// Token: 0x06000671 RID: 1649
		[DllImport("Kernel32.dll", ExactSpelling = true, SetLastError = true)]
		private static extern bool SetThreadErrorMode(uint newMode, out uint oldMode);

		// Token: 0x06000672 RID: 1650 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		internal static void SetErrorModeWrapper(uint mode, out uint oldMode)
		{
			if (Environment.OSVersion.Version >= UnsafeNativeMethods.ThreadErrorModeMinOsVersion)
			{
				if (!UnsafeNativeMethods.SetThreadErrorMode(mode, out oldMode))
				{
					throw new Win32Exception();
				}
			}
			else
			{
				oldMode = UnsafeNativeMethods.SetErrorMode(mode);
			}
		}

		// Token: 0x06000673 RID: 1651
		[DllImport("Kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool DeviceIoControl(SafeFileHandle fileHandle, uint ioControlCode, IntPtr inBuffer, uint cbInBuffer, IntPtr outBuffer, uint cbOutBuffer, out uint cbBytesReturned, IntPtr overlapped);

		// Token: 0x06000674 RID: 1652
		[DllImport("NtDll.dll")]
		internal static extern uint RtlNtStatusToDosError(uint status);

		// Token: 0x06000675 RID: 1653 RVA: 0x0000C513 File Offset: 0x0000A713
		internal static uint CTL_CODE(ushort deviceType, ushort function, byte method, byte access)
		{
			if (function > 4095)
			{
				throw ADP.ArgumentOutOfRange("function");
			}
			return (uint)(((int)deviceType << 16) | ((int)access << 14) | ((int)function << 2) | (int)method);
		}

		// Token: 0x04000048 RID: 72
		private static readonly Version ThreadErrorModeMinOsVersion = new Version(6, 1, 7600);

		// Token: 0x04000049 RID: 73
		internal const ushort FILE_DEVICE_FILE_SYSTEM = 9;

		// Token: 0x0400004A RID: 74
		internal const int ERROR_INVALID_HANDLE = 6;

		// Token: 0x0400004B RID: 75
		internal const int ERROR_MR_MID_NOT_FOUND = 317;

		// Token: 0x0400004C RID: 76
		internal const uint STATUS_INVALID_PARAMETER = 3221225485U;

		// Token: 0x0400004D RID: 77
		internal const uint STATUS_SHARING_VIOLATION = 3221225539U;

		// Token: 0x0400004E RID: 78
		internal const uint STATUS_OBJECT_NAME_NOT_FOUND = 3221225524U;

		// Token: 0x0400004F RID: 79
		internal const uint SEM_FAILCRITICALERRORS = 1U;

		// Token: 0x04000050 RID: 80
		internal const int FILE_READ_DATA = 1;

		// Token: 0x04000051 RID: 81
		internal const int FILE_WRITE_DATA = 2;

		// Token: 0x04000052 RID: 82
		internal const int FILE_READ_ATTRIBUTES = 128;

		// Token: 0x04000053 RID: 83
		internal const int SYNCHRONIZE = 1048576;

		// Token: 0x02000194 RID: 404
		internal enum Method
		{
			// Token: 0x04001249 RID: 4681
			METHOD_BUFFERED,
			// Token: 0x0400124A RID: 4682
			METHOD_IN_DIRECT,
			// Token: 0x0400124B RID: 4683
			METHOD_OUT_DIRECT,
			// Token: 0x0400124C RID: 4684
			METHOD_NEITHER
		}

		// Token: 0x02000195 RID: 405
		internal enum Access
		{
			// Token: 0x0400124E RID: 4686
			FILE_ANY_ACCESS,
			// Token: 0x0400124F RID: 4687
			FILE_READ_ACCESS,
			// Token: 0x04001250 RID: 4688
			FILE_WRITE_ACCESS
		}

		// Token: 0x02000196 RID: 406
		internal enum FileType : uint
		{
			// Token: 0x04001252 RID: 4690
			Unknown,
			// Token: 0x04001253 RID: 4691
			Disk,
			// Token: 0x04001254 RID: 4692
			Char,
			// Token: 0x04001255 RID: 4693
			Pipe,
			// Token: 0x04001256 RID: 4694
			Remote = 32768U
		}

		// Token: 0x02000197 RID: 407
		internal struct OBJECT_ATTRIBUTES
		{
			// Token: 0x04001257 RID: 4695
			internal int length;

			// Token: 0x04001258 RID: 4696
			internal IntPtr rootDirectory;

			// Token: 0x04001259 RID: 4697
			internal SafeHandle objectName;

			// Token: 0x0400125A RID: 4698
			internal int attributes;

			// Token: 0x0400125B RID: 4699
			internal IntPtr securityDescriptor;

			// Token: 0x0400125C RID: 4700
			internal SafeHandle securityQualityOfService;
		}

		// Token: 0x02000198 RID: 408
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct UNICODE_STRING
		{
			// Token: 0x0400125D RID: 4701
			internal ushort length;

			// Token: 0x0400125E RID: 4702
			internal ushort maximumLength;

			// Token: 0x0400125F RID: 4703
			internal string buffer;
		}

		// Token: 0x02000199 RID: 409
		internal enum SecurityImpersonationLevel
		{
			// Token: 0x04001261 RID: 4705
			SecurityAnonymous,
			// Token: 0x04001262 RID: 4706
			SecurityIdentification,
			// Token: 0x04001263 RID: 4707
			SecurityImpersonation,
			// Token: 0x04001264 RID: 4708
			SecurityDelegation
		}

		// Token: 0x0200019A RID: 410
		internal struct SECURITY_QUALITY_OF_SERVICE
		{
			// Token: 0x04001265 RID: 4709
			internal uint length;

			// Token: 0x04001266 RID: 4710
			[MarshalAs(UnmanagedType.I4)]
			internal int impersonationLevel;

			// Token: 0x04001267 RID: 4711
			internal byte contextDynamicTrackingMode;

			// Token: 0x04001268 RID: 4712
			internal byte effectiveOnly;
		}

		// Token: 0x0200019B RID: 411
		internal struct IO_STATUS_BLOCK
		{
			// Token: 0x04001269 RID: 4713
			internal uint status;

			// Token: 0x0400126A RID: 4714
			internal IntPtr information;
		}

		// Token: 0x0200019C RID: 412
		internal struct FILE_FULL_EA_INFORMATION
		{
			// Token: 0x0400126B RID: 4715
			internal uint nextEntryOffset;

			// Token: 0x0400126C RID: 4716
			internal byte flags;

			// Token: 0x0400126D RID: 4717
			internal byte EaNameLength;

			// Token: 0x0400126E RID: 4718
			internal ushort EaValueLength;

			// Token: 0x0400126F RID: 4719
			internal byte EaName;
		}

		// Token: 0x0200019D RID: 413
		[Flags]
		internal enum CreateOption : uint
		{
			// Token: 0x04001271 RID: 4721
			FILE_WRITE_THROUGH = 2U,
			// Token: 0x04001272 RID: 4722
			FILE_SEQUENTIAL_ONLY = 4U,
			// Token: 0x04001273 RID: 4723
			FILE_NO_INTERMEDIATE_BUFFERING = 8U,
			// Token: 0x04001274 RID: 4724
			FILE_SYNCHRONOUS_IO_NONALERT = 32U,
			// Token: 0x04001275 RID: 4725
			FILE_RANDOM_ACCESS = 2048U
		}

		// Token: 0x0200019E RID: 414
		internal enum CreationDisposition : uint
		{
			// Token: 0x04001277 RID: 4727
			FILE_SUPERSEDE,
			// Token: 0x04001278 RID: 4728
			FILE_OPEN,
			// Token: 0x04001279 RID: 4729
			FILE_CREATE,
			// Token: 0x0400127A RID: 4730
			FILE_OPEN_IF,
			// Token: 0x0400127B RID: 4731
			FILE_OVERWRITE,
			// Token: 0x0400127C RID: 4732
			FILE_OVERWRITE_IF
		}

		// Token: 0x0200019F RID: 415
		[Flags]
		internal enum Attributes : uint
		{
			// Token: 0x0400127E RID: 4734
			Inherit = 2U,
			// Token: 0x0400127F RID: 4735
			Permanent = 16U,
			// Token: 0x04001280 RID: 4736
			Exclusive = 32U,
			// Token: 0x04001281 RID: 4737
			CaseInsensitive = 64U,
			// Token: 0x04001282 RID: 4738
			OpenIf = 128U,
			// Token: 0x04001283 RID: 4739
			OpenLink = 256U,
			// Token: 0x04001284 RID: 4740
			KernelHandle = 512U,
			// Token: 0x04001285 RID: 4741
			ForceAccessCheck = 1024U,
			// Token: 0x04001286 RID: 4742
			ValidAttributes = 2034U
		}
	}
}
