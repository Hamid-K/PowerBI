using System;
using System.Runtime.InteropServices;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002A0 RID: 672
	public static class ExtendedThread
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x000400AC File Offset: 0x0003E2AC
		public static int GetCurrentThreadId()
		{
			return ExtendedThread.NativeMethods.GetCurrentThreadId();
		}

		// Token: 0x02000773 RID: 1907
		private static class NativeMethods
		{
			// Token: 0x0600306B RID: 12395
			[DllImport("kernel32.dll")]
			internal static extern int GetCurrentThreadId();
		}
	}
}
