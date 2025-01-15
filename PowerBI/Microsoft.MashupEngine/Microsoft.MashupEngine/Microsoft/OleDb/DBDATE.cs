using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EDD RID: 7901
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBDATE
	{
		// Token: 0x040063E4 RID: 25572
		public short year;

		// Token: 0x040063E5 RID: 25573
		public ushort month;

		// Token: 0x040063E6 RID: 25574
		public ushort day;
	}
}
