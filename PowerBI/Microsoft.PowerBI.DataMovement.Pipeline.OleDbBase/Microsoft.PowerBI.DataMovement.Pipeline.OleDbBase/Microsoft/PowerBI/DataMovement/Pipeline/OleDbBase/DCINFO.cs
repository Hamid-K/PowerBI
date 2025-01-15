using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A8 RID: 168
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DCINFO
	{
		// Token: 0x04000320 RID: 800
		public DCINFOTYPE InfoType;

		// Token: 0x04000321 RID: 801
		public VARIANT Data;
	}
}
