using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200000A RID: 10
	public static class RuntimeInformation
	{
		// Token: 0x06000013 RID: 19 RVA: 0x000021E8 File Offset: 0x000003E8
		public static bool IsOSPlatform(OSPlatform osPlatform)
		{
			return osPlatform == OSPlatform.Windows;
		}
	}
}
