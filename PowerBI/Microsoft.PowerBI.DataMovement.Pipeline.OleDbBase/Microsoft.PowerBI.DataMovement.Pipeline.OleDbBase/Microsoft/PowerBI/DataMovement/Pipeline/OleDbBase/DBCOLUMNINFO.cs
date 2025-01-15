using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000A1 RID: 161
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBCOLUMNINFO
	{
		// Token: 0x040002F5 RID: 757
		public unsafe char* Name;

		// Token: 0x040002F6 RID: 758
		public unsafe void* TypeInfo;

		// Token: 0x040002F7 RID: 759
		public DBORDINAL Ordinal;

		// Token: 0x040002F8 RID: 760
		public DBCOLUMNFLAGS Flags;

		// Token: 0x040002F9 RID: 761
		public DBLENGTH ColumnSize;

		// Token: 0x040002FA RID: 762
		public DBTYPE Type;

		// Token: 0x040002FB RID: 763
		public byte Precision;

		// Token: 0x040002FC RID: 764
		public byte Scale;

		// Token: 0x040002FD RID: 765
		public DBID ColumnId;
	}
}
