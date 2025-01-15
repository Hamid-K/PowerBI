using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A2 RID: 162
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MDAXISINFO
	{
		// Token: 0x040002FE RID: 766
		public DBLENGTH Size;

		// Token: 0x040002FF RID: 767
		public DBCOUNTITEM AxisIndex;

		// Token: 0x04000300 RID: 768
		public DBCOUNTITEM DimensionsCount;

		// Token: 0x04000301 RID: 769
		public DBCOUNTITEM CoordinatesCount;

		// Token: 0x04000302 RID: 770
		public unsafe DBORDINAL* DimensionColumnCounts;

		// Token: 0x04000303 RID: 771
		public unsafe char** DimensionNames;
	}
}
