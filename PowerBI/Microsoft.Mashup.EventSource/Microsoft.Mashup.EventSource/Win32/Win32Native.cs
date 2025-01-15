using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Diagnostics.Tracing.Internal;

namespace Microsoft.Win32
{
	// Token: 0x02000006 RID: 6
	internal static class Win32Native
	{
		// Token: 0x06000008 RID: 8
		[SecurityCritical]
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Unicode)]
		internal static extern int FormatMessageW(int dwFlags, IntPtr lpSource, int dwMessageId, int dwLanguageId, [Out] StringBuilder lpBuffer, int nSize, IntPtr va_list_arguments);

		// Token: 0x06000009 RID: 9 RVA: 0x00002160 File Offset: 0x00000360
		[SecuritySafeCritical]
		internal static string GetMessage(int errorCode)
		{
			StringBuilder stringBuilder = new StringBuilder(512);
			if (Win32Native.FormatMessageW(12800, IntPtr.Zero, errorCode, 0, stringBuilder, stringBuilder.Capacity, IntPtr.Zero) != 0)
			{
				return stringBuilder.ToString();
			}
			return Microsoft.Diagnostics.Tracing.Internal.Environment.GetRuntimeResourceString("UnknownError_Num", new object[] { errorCode });
		}

		// Token: 0x0600000A RID: 10
		[SecurityCritical]
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern uint GetCurrentProcessId();

		// Token: 0x04000006 RID: 6
		private const string CoreProcessThreadsApiSet = "kernel32.dll";

		// Token: 0x04000007 RID: 7
		private const string CoreLocalizationApiSet = "kernel32.dll";

		// Token: 0x04000008 RID: 8
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x04000009 RID: 9
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x0400000A RID: 10
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
	}
}
