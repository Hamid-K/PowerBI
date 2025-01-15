using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ParquetSharp.IO
{
	// Token: 0x020000A2 RID: 162
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct AuthenticationContext
	{
		// Token: 0x0400016E RID: 366
		[Nullable(2)]
		public GetStorageToken GetStorageTokenCallback;
	}
}
