using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000097 RID: 151
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBTIMESTAMP
	{
		// Token: 0x040002B5 RID: 693
		public short Year;

		// Token: 0x040002B6 RID: 694
		public ushort Month;

		// Token: 0x040002B7 RID: 695
		public ushort Day;

		// Token: 0x040002B8 RID: 696
		public ushort Hour;

		// Token: 0x040002B9 RID: 697
		public ushort Minute;

		// Token: 0x040002BA RID: 698
		public ushort Second;

		// Token: 0x040002BB RID: 699
		public uint Fraction;
	}
}
