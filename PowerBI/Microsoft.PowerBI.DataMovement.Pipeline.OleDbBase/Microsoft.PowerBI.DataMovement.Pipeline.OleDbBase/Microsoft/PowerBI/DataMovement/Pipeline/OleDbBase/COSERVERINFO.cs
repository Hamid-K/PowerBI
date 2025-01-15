using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000AF RID: 175
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct COSERVERINFO
	{
		// Token: 0x0400033F RID: 831
		public uint Reserved1;

		// Token: 0x04000340 RID: 832
		public unsafe char* Name;

		// Token: 0x04000341 RID: 833
		public uint Reserved2;
	}
}
