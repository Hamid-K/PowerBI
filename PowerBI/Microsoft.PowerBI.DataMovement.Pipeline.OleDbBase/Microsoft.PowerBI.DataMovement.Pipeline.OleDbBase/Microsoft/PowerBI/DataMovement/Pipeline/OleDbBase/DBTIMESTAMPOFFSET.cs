using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000AA RID: 170
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBTIMESTAMPOFFSET
	{
		// Token: 0x04000326 RID: 806
		public short Year;

		// Token: 0x04000327 RID: 807
		public ushort Month;

		// Token: 0x04000328 RID: 808
		public ushort Day;

		// Token: 0x04000329 RID: 809
		public ushort Hour;

		// Token: 0x0400032A RID: 810
		public ushort Minute;

		// Token: 0x0400032B RID: 811
		public ushort Second;

		// Token: 0x0400032C RID: 812
		public uint Fraction;

		// Token: 0x0400032D RID: 813
		public short Timezone_Hour;

		// Token: 0x0400032E RID: 814
		public short Timezone_Minute;
	}
}
