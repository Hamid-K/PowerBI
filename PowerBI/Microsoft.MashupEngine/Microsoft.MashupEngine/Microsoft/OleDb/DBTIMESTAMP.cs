using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EDF RID: 7903
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBTIMESTAMP
	{
		// Token: 0x040063EA RID: 25578
		public short year;

		// Token: 0x040063EB RID: 25579
		public ushort month;

		// Token: 0x040063EC RID: 25580
		public ushort day;

		// Token: 0x040063ED RID: 25581
		public ushort hour;

		// Token: 0x040063EE RID: 25582
		public ushort minute;

		// Token: 0x040063EF RID: 25583
		public ushort second;

		// Token: 0x040063F0 RID: 25584
		public uint fraction;
	}
}
