using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000A3 RID: 163
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct AuthenticationData
	{
		// Token: 0x0400016F RID: 367
		[Nullable(1)]
		[MarshalAs(UnmanagedType.LPWStr)]
		public string AccessToken;

		// Token: 0x04000170 RID: 368
		[MarshalAs(UnmanagedType.I8)]
		public long UnixExpiresOn;
	}
}
