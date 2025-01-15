using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Mashup.Security.Cryptography
{
	// Token: 0x02001FFA RID: 8186
	internal static class Win32Native
	{
		// Token: 0x0600C78F RID: 51087 RVA: 0x0027B5C4 File Offset: 0x002797C4
		[SecurityCritical]
		[SecurityTreatAsSafe]
		internal static string FormatMessageFromLibrary(int message, string library)
		{
			string text;
			using (SafeLibraryHandle safeLibraryHandle = Win32Native.UnsafeNativeMethods.LoadLibrary(library))
			{
				IntPtr zero = IntPtr.Zero;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					if (Win32Native.UnsafeNativeMethods.FormatMessage(Win32Native.FormatMessageFlags.AllocateBuffer | Win32Native.FormatMessageFlags.FromModule | Win32Native.FormatMessageFlags.FromSystem, safeLibraryHandle, message, 0, ref zero, 0, IntPtr.Zero) == 0)
					{
						Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
					}
					text = Marshal.PtrToStringUni(zero);
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						Win32Native.UnsafeNativeMethods.LocalFree(zero);
					}
				}
			}
			return text;
		}

		// Token: 0x0600C790 RID: 51088 RVA: 0x0027B64C File Offset: 0x0027984C
		internal static string GetNTStatusMessage(int ntstatus)
		{
			return Win32Native.FormatMessageFromLibrary(ntstatus, "ntdll.dll");
		}

		// Token: 0x02001FFB RID: 8187
		[Flags]
		internal enum FormatMessageFlags
		{
			// Token: 0x040065E7 RID: 26087
			None = 0,
			// Token: 0x040065E8 RID: 26088
			AllocateBuffer = 256,
			// Token: 0x040065E9 RID: 26089
			FromModule = 2048,
			// Token: 0x040065EA RID: 26090
			FromSystem = 4096
		}

		// Token: 0x02001FFC RID: 8188
		internal struct SYSTEMTIME
		{
			// Token: 0x0600C791 RID: 51089 RVA: 0x0027B65C File Offset: 0x0027985C
			internal SYSTEMTIME(DateTime time)
			{
				this.wYear = (ushort)time.Year;
				this.wMonth = (ushort)time.Month;
				this.wDayOfWeek = (ushort)time.DayOfWeek;
				this.wDay = (ushort)time.Day;
				this.wHour = (ushort)time.Hour;
				this.wMinute = (ushort)time.Minute;
				this.wSecond = (ushort)time.Second;
				this.wMilliseconds = (ushort)time.Millisecond;
			}

			// Token: 0x040065EB RID: 26091
			internal ushort wYear;

			// Token: 0x040065EC RID: 26092
			internal ushort wMonth;

			// Token: 0x040065ED RID: 26093
			internal ushort wDayOfWeek;

			// Token: 0x040065EE RID: 26094
			internal ushort wDay;

			// Token: 0x040065EF RID: 26095
			internal ushort wHour;

			// Token: 0x040065F0 RID: 26096
			internal ushort wMinute;

			// Token: 0x040065F1 RID: 26097
			internal ushort wSecond;

			// Token: 0x040065F2 RID: 26098
			internal ushort wMilliseconds;
		}

		// Token: 0x02001FFD RID: 8189
		[SecurityCritical(SecurityCriticalScope.Everything)]
		[SuppressUnmanagedCodeSecurity]
		private static class UnsafeNativeMethods
		{
			// Token: 0x0600C792 RID: 51090
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			internal static extern SafeLibraryHandle LoadLibrary(string lpFileName);

			// Token: 0x0600C793 RID: 51091
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			[DllImport("kernel32.dll", SetLastError = true)]
			internal static extern IntPtr LocalFree(IntPtr hMem);

			// Token: 0x0600C794 RID: 51092
			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
			internal static extern int FormatMessage(Win32Native.FormatMessageFlags dwFlags, SafeLibraryHandle lpSource, int dwMessageId, int dwLanguageId, [In] [Out] ref IntPtr lpBuffer, int nSize, IntPtr pArguments);
		}
	}
}
