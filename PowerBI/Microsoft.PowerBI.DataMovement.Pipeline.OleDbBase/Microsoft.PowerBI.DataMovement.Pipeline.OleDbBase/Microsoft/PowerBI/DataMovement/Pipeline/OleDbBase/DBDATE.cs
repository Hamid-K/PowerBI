using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000095 RID: 149
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBDATE
	{
		// Token: 0x040002AF RID: 687
		public short Year;

		// Token: 0x040002B0 RID: 688
		public ushort Month;

		// Token: 0x040002B1 RID: 689
		public ushort Day;
	}
}
