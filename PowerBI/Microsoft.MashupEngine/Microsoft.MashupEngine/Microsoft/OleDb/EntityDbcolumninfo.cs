using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EAC RID: 7852
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct EntityDbcolumninfo
	{
		// Token: 0x040061B3 RID: 25011
		public DBCOLUMNINFO dbcolumninfo;

		// Token: 0x040061B4 RID: 25012
		public EntityPropertyFormat format;

		// Token: 0x040061B5 RID: 25013
		public EntityPropertyFlags flags;
	}
}
