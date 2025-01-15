using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Extensions.Msal
{
	// Token: 0x0200001D RID: 29
	internal static class LinuxNativeMethods
	{
		// Token: 0x06000075 RID: 117
		[DllImport("libc")]
		public static extern int getuid();

		// Token: 0x0400006E RID: 110
		public const int RootUserId = 0;
	}
}
