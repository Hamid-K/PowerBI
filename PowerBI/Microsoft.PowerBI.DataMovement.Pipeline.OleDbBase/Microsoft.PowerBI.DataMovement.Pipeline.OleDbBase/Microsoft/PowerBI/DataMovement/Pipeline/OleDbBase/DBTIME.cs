using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000096 RID: 150
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBTIME
	{
		// Token: 0x040002B2 RID: 690
		public ushort Hour;

		// Token: 0x040002B3 RID: 691
		public ushort Minute;

		// Token: 0x040002B4 RID: 692
		public ushort Second;
	}
}
