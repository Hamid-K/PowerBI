using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000009 RID: 9
	internal static class RuntimeInformation
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002098 File Offset: 0x00000298
		public static bool IsOSPlatform(OSPlatform osPlatform)
		{
			return osPlatform == OSPlatform.Windows;
		}
	}
}
