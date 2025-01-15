using System;
using System.Runtime.InteropServices;

namespace Microsoft.OleDb
{
	// Token: 0x02001EE8 RID: 7912
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DBOBJECT
	{
		// Token: 0x04006425 RID: 25637
		public uint dwFlags;

		// Token: 0x04006426 RID: 25638
		public Guid iid;
	}
}
