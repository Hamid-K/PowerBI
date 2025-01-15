using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A0 RID: 160
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBPROPINFO
	{
		// Token: 0x040002F0 RID: 752
		public unsafe char* Description;

		// Token: 0x040002F1 RID: 753
		public DBPROPID PropertyID;

		// Token: 0x040002F2 RID: 754
		public DBPROPFLAGS Flags;

		// Token: 0x040002F3 RID: 755
		public VARTYPE Type;

		// Token: 0x040002F4 RID: 756
		public VARIANT Values;
	}
}
