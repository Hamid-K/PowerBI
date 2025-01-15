using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EEF RID: 7919
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBTIMESTAMPOFFSET
	{
		// Token: 0x04006447 RID: 25671
		public short year;

		// Token: 0x04006448 RID: 25672
		public ushort month;

		// Token: 0x04006449 RID: 25673
		public ushort day;

		// Token: 0x0400644A RID: 25674
		public ushort hour;

		// Token: 0x0400644B RID: 25675
		public ushort minute;

		// Token: 0x0400644C RID: 25676
		public ushort second;

		// Token: 0x0400644D RID: 25677
		public uint fraction;

		// Token: 0x0400644E RID: 25678
		public short timezone_hour;

		// Token: 0x0400644F RID: 25679
		public short timezone_minute;
	}
}
