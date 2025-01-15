using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A9 RID: 169
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBTIME2
	{
		// Token: 0x04000322 RID: 802
		public ushort Hour;

		// Token: 0x04000323 RID: 803
		public ushort Minute;

		// Token: 0x04000324 RID: 804
		public ushort Second;

		// Token: 0x04000325 RID: 805
		public uint Fraction;
	}
}
