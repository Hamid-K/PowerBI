using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EB3 RID: 7859
	[StructLayout(LayoutKind.Explicit)]
	public struct DecimalBlittable
	{
		// Token: 0x040061C4 RID: 25028
		[FieldOffset(0)]
		public uint flags;

		// Token: 0x040061C5 RID: 25029
		[FieldOffset(4)]
		public uint hi;

		// Token: 0x040061C6 RID: 25030
		[FieldOffset(8)]
		public uint lo;

		// Token: 0x040061C7 RID: 25031
		[FieldOffset(12)]
		public uint mid;
	}
}
