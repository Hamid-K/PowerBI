using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EDE RID: 7902
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBTIME
	{
		// Token: 0x040063E7 RID: 25575
		public ushort hour;

		// Token: 0x040063E8 RID: 25576
		public ushort minute;

		// Token: 0x040063E9 RID: 25577
		public ushort second;
	}
}
