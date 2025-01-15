using System;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000D4 RID: 212
	internal static class NativeMethods
	{
		// Token: 0x06000601 RID: 1537
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern int SetConsoleCtrlHandler(NativeMethods.ConsoleCtrlDelegate HandlerRoutine, int Add);

		// Token: 0x04000215 RID: 533
		internal const int TRUE = 1;

		// Token: 0x04000216 RID: 534
		internal const int FALSE = 0;

		// Token: 0x020005D6 RID: 1494
		// (Invoke) Token: 0x06002BBA RID: 11194
		internal delegate int ConsoleCtrlDelegate(NativeMethods.CtrlTypes CtrlType);

		// Token: 0x020005D7 RID: 1495
		internal enum CtrlTypes : uint
		{
			// Token: 0x04000FE6 RID: 4070
			CTRL_C_EVENT,
			// Token: 0x04000FE7 RID: 4071
			CTRL_BREAK_EVENT,
			// Token: 0x04000FE8 RID: 4072
			CTRL_CLOSE_EVENT,
			// Token: 0x04000FE9 RID: 4073
			CTRL_LOGOFF_EVENT = 5U,
			// Token: 0x04000FEA RID: 4074
			CTRL_SHUTDOWN_EVENT
		}
	}
}
