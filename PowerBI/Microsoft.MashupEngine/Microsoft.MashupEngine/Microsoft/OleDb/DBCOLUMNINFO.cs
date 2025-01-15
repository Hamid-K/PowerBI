using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE7 RID: 7911
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBCOLUMNINFO
	{
		// Token: 0x0400641C RID: 25628
		public unsafe char* pwszName;

		// Token: 0x0400641D RID: 25629
		public unsafe void* pTypeInfo;

		// Token: 0x0400641E RID: 25630
		public DBORDINAL iOrdinal;

		// Token: 0x0400641F RID: 25631
		public DBCOLUMNFLAGS dwFlags;

		// Token: 0x04006420 RID: 25632
		public DBLENGTH ulColumnSize;

		// Token: 0x04006421 RID: 25633
		public DBTYPE wType;

		// Token: 0x04006422 RID: 25634
		public byte bPrecision;

		// Token: 0x04006423 RID: 25635
		public byte bScale;

		// Token: 0x04006424 RID: 25636
		public DBID columnid;
	}
}
