using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ULARGE_INTEGER
	{
		// Token: 0x04000330 RID: 816
		public ulong QuadPart;
	}
}
